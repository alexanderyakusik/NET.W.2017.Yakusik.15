using System;
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

        private readonly IAccountRepository repository;
        private readonly IUnitOfWork unitOfWork;
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
            IAccountRepository repository,
            IUnitOfWork unitOfWork,
            IAccountIdGeneratorService accountIdGeneratorService,
            IBonusPointsCalculatorService bonusPointsCalculatorService)
        {
            this.repository = repository 
                ?? throw new ArgumentNullException($"{nameof(repository)} cannot be null.");
            this.unitOfWork = unitOfWork
                ?? throw new ArgumentNullException($"{nameof(unitOfWork)} cannot be null.");
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

            string accountId = accountIdGeneratorService.GenerateId(firstName, lastName, accountType.ToString());

            if (repository.GetAccountById(accountId) != null)
            {
                throw new ArgumentException("Such account already exists.");
            }

            BankAccount account = AccountResolver.CreateAccount(accountType.ToString(), accountId, firstName, lastName);
            
            repository.Add(account.ToAccountDto(accountType.ToString()));
            unitOfWork.Commit();

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

            repository.Update(account.ToAccountDto(AccountResolver.GetBankAccountTypeName(account.GetType())));
            unitOfWork.Commit();
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

            DepositToAccount(account, amount);
            
            repository.Update(account.ToAccountDto(AccountResolver.GetBankAccountTypeName(account.GetType())));
            unitOfWork.Commit();
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

            WithdrawFromAccount(account, amount);

            repository.Update(account.ToAccountDto(AccountResolver.GetBankAccountTypeName(account.GetType())));
            unitOfWork.Commit();
        }

        /// <inheritdoc />
        public void Transfer(string destinationAccountId, string sourceAccountId, decimal amount)
        {
            destinationAccountId = destinationAccountId
                                   ?? throw new ArgumentNullException($"{nameof(destinationAccountId)} cannot be null.");

            sourceAccountId = sourceAccountId
                              ?? throw new ArgumentNullException($"{nameof(sourceAccountId)} cannot be null.");

            amount = amount <= 0
                ? throw new ArgumentOutOfRangeException($"{nameof(amount)} cannot be less than zero")
                : amount;

            var destinationAccount = repository.GetAccountById(destinationAccountId)?.ToAccount()
                ?? throw new ArgumentException($"Account with {destinationAccountId} doesn't exist");

            var sourceAccount = repository.GetAccountById(sourceAccountId)?.ToAccount()
                ?? throw new ArgumentException($"Account with {sourceAccountId} doesn't exist");

            try
            {
                WithdrawFromAccount(sourceAccount, amount);
                DepositToAccount(destinationAccount, amount);

                repository.Update(sourceAccount.ToAccountDto(
                    AccountResolver.GetBankAccountTypeName(sourceAccount.GetType())));
                repository.Update(destinationAccount.ToAccountDto(
                    AccountResolver.GetBankAccountTypeName(destinationAccount.GetType())));

                unitOfWork.Commit();
            }
            catch (InvalidOperationException)
            {
                // How to restore?
                throw;
            }
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

        private void WithdrawFromAccount(BankAccount account, decimal amount)
        {
            long withdrawPenalty =
                bonusPointsCalculatorService.CalculateWithdrawPenalty(account.BalanceValue, account.DepositValue);

            account.Withdraw(amount, withdrawPenalty);
        }

        private void DepositToAccount(BankAccount account, decimal amount)
        {
            long depositBonus =
                bonusPointsCalculatorService.CalculateDepositBonus(account.BalanceValue, account.DepositValue);

            account.Deposit(amount, depositBonus);
        }
    }  
}
