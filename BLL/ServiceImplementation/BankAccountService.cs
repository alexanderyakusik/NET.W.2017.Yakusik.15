﻿using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using BLL.Mappers;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using Services.Interface.Interfaces;

namespace BLL.ServiceImplementation
{
    public class BankAccountService : IBankAccountService
    {
        #region Private fields

        private readonly IRepository repository;
        private readonly IAccountIdGeneratorService accountIdGeneratorService;
        private readonly IBonusPointsCalculatorService bonusPointsCalculatorService;

        #endregion

        #region Ctors

        /// <summary>
        /// Creates a new bank account service.
        /// </summary>
        /// <param name="repository">Repository to store bank accounts.</param>
        /// <param name="accountIdGeneratorService">Generator of account id.</param>
        /// <param name="bonusPointsCalculatorService">Calculator of bonus points.</param>
        /// <exception cref="ArgumentNullException"><paramref name="repository"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="accountIdGeneratorService"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="bonusPointsCalculatorService"/> is null.</exception>
        public BankAccountService(
            IRepository repository,
            IAccountIdGeneratorService accountIdGeneratorService,
            IBonusPointsCalculatorService bonusPointsCalculatorService)
        {
            this.repository = repository 
                ?? throw new ArgumentNullException($"{nameof(repository)} cannot be null.");
            this.accountIdGeneratorService = accountIdGeneratorService 
                ?? throw new ArgumentNullException($"{nameof(accountIdGeneratorService)} cannot be null.");
            this.bonusPointsCalculatorService = bonusPointsCalculatorService 
                ?? throw new ArgumentNullException($"{nameof(bonusPointsCalculatorService)} cannot be null.");
        }

        #endregion

        #region Interfaces implementations

        /// <inheritdoc />
        public string OpenAccount(string firstName, string lastName, AccountType accountType)
        {
            firstName = firstName ?? throw new ArgumentNullException($"{nameof(firstName)} cannot be null.");
            lastName = lastName ?? throw new ArgumentNullException($"{nameof(lastName)} cannot be null.");

            Type bankAccountType = Type.GetType($"{accountType}Account");
            string accountId = accountIdGeneratorService.GenerateId(firstName, lastName, bankAccountType);

            if (repository.GetAccountById(accountId) != null)
            {
                throw new ArgumentException("Such account already exists.");
            }

            BankAccount account = CreateAccount(bankAccountType, accountId, firstName, lastName);
            
            repository.Add(account.ToAccountDto());

            return accountId;
        }

        /// <inheritdoc />
        public void CloseAccount(string accountId)
        {
            accountId = accountId ?? throw new ArgumentNullException($"{nameof(accountId)} cannot be null.");

            BankAccountDto accountDto = repository.GetAccountById(accountId) ?? 
                                        throw new ArgumentException($"Such account doesn't exist.");

            if (accountDto.IsClosed)
            {
                throw new InvalidOperationException($"Specified account is already closed.");
            }

            BankAccount account = accountDto.ToAccount();
            account.Close();

            repository.Update(account.ToAccountDto());
        }

        /// <inheritdoc />
        public void Deposit(string accountId, decimal amount)
        {
            accountId = accountId ?? throw new ArgumentNullException($"{nameof(accountId)} cannot be null.");
            amount = amount >= 0
                    ? amount
                    : throw new ArgumentOutOfRangeException($"{nameof(amount)} cannot be less than zero.");

            BankAccountDto bankAccountDto = repository.GetAccountById(accountId) ?? 
                                            throw new ArgumentException($"Such account doesn't exist.");

            BankAccount account = bankAccountDto.ToAccount();

            long depositBonus =
                bonusPointsCalculatorService.CalculateDepositBonus(account.BalanceValue, account.DepositValue);

            account.Deposit(amount, depositBonus);
            
            repository.Update(account.ToAccountDto());
        }

        /// <inheritdoc />
        public void Withdraw(string accountId, decimal amount)
        {
            accountId = accountId ?? throw new ArgumentNullException($"{nameof(accountId)} cannot be null.");
            amount = amount >= 0
                    ? amount
                    : throw new ArgumentOutOfRangeException($"{nameof(amount)} cannot be less than zero.");

            BankAccountDto bankAccountDto = repository.GetAccountById(accountId) ??
                                            throw new ArgumentException($"Such account doesn't exist.");

            BankAccount account = bankAccountDto.ToAccount();

            long withdrawPenalty =
                bonusPointsCalculatorService.CalculateWithdrawPenalty(account.BalanceValue, account.DepositValue);

            account.Withdraw(amount, withdrawPenalty);

            repository.Update(account.ToAccountDto());
        }

        /// <inheritdoc />
        public BankAccount GetAccountById(string accountId)
        {
            accountId = accountId ?? throw new ArgumentNullException($"{nameof(accountId)} cannot be null.");

            BankAccountDto bankAccountDto = repository.GetAccountById(accountId);

            return bankAccountDto?.ToAccount();
        }

        /// <inheritdoc />
        public IEnumerable<BankAccount> GetAll()
        {
            return repository.GetAll().Select(accountDto => accountDto.ToAccount());
        }

        #endregion

        #region Private methods

        private BankAccount CreateAccount(Type accountType, string accountId, string firstName, string lastName)
        {
            return (BankAccount)Activator.CreateInstance(accountType, accountId, firstName, lastName);
        }

        #endregion
    }
}