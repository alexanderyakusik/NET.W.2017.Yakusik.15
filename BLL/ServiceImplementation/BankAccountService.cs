namespace BLL.ServiceImplementation
{
    using System;
    using System.Collections.Generic;
    using DAL.Interface.Interfaces;
    using Interface.Entities;
    using Interface.Interfaces;
    using Services.Interface.Interfaces;

    public class BankAccountService : IBankAccountService
    {
        #region Private fields

        private IRepository repository;
        private IAccountIdGeneratorService accountIdGeneratorService;
        private IBonusPointsCalculatorService bonusPointsCalculatorService;

        #endregion

        #region Ctors

        /// <summary>
        /// Creates a new bank account service.
        /// </summary>
        /// <param name="repository">Repository to store bank accounts.</param>
        /// <param name="accountIdGeneratorService">Generator of account id.</param>
        /// <param name="bonusPointsCalculatorService">Calculator of bonus points.</param>
        public BankAccountService(
            IRepository repository,
            IAccountIdGeneratorService accountIdGeneratorService,
            IBonusPointsCalculatorService bonusPointsCalculatorService)
        {
            this.repository = repository;
            this.accountIdGeneratorService = accountIdGeneratorService;
            this.bonusPointsCalculatorService = bonusPointsCalculatorService;
        }

        #endregion

        #region Interfaces implementations

        /// <inheritdoc />
        public void OpenAccount(string firstName, string lastName, AccountType accountType)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void CloseAccount(string accountId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Deposit(string accountId, decimal value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Withdraw(string accountId, decimal value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public BankAccount GetAccountById(string accountId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<BankAccount> GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
