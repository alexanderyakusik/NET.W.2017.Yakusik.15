using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using DAL.Mappers;
using ORM;
using ORM.Entities;

namespace DAL.Repositories
{
    public class BankAccountRepository : IRepository
    {
        #region Private fields

        private readonly DbContext context;

        #endregion

        #region Ctors

        public BankAccountRepository(DbContext context)
        {
            this.context = context;
        }

        #endregion

        #region Interfaces implementations

        #region IRepository

        /// <inheritdoc />
        public void Add(BankAccountDto bankAccountDto)
        {
            var account = bankAccountDto.ToOrmBankAccount();

            SetAccountForeignKeys(account);

            context.Entry(account).State = EntityState.Added;
        }

        /// <inheritdoc />
        public void Update(BankAccountDto bankAccountDto)
        {
            var account = GetAccountByNumber(bankAccountDto.Id);

            UpdateProperties(account, bankAccountDto);
        }

        /// <inheritdoc />
        public IEnumerable<BankAccountDto> GetAll()
        {
                return context.Set<BankAccount>()
                    .Include(account => account.AccountOwner)
                    .Include(account => account.AccountType)
                    .ToList()
                    .Select(account => account.ToDto());
        }

        /// <inheritdoc />
        public BankAccountDto GetAccountById(string accountId)
        {
            return context.Set<BankAccount>()
                .Include(account => account.AccountOwner)
                .Include(account => account.AccountType)
                .FirstOrDefault(account => account.AccountNumber == accountId)
                ?.ToDto();
        }

        #endregion

        #endregion

        #region Private methods

        private AccountOwner GetAccountOwnerByName(string firstName, string lastName)
        {
            return context.Set<AccountOwner>().FirstOrDefault(owner =>
                owner.FirstName == firstName && owner.LastName == lastName);
        }

        private AccountType GetAccountTypeByName(string accountTypeName)
        {
            return context.Set<AccountType>().FirstOrDefault(accountType => accountType.Name == accountTypeName);
        }

        private BankAccount GetAccountByNumber(string accountNumber)
        {
            return context.Set<BankAccount>().FirstOrDefault(account => account.AccountNumber == accountNumber);
        }

        private void SetAccountForeignKeys(BankAccount account)
        {
            var accountOwner = GetAccountOwnerByName(
                account.AccountOwner.FirstName,
                account.AccountOwner.LastName);

            if (accountOwner != null)
            {
                account.AccountOwner = null;
                account.AccountOwnerId = accountOwner.Id;
            }

            var accountType = GetAccountTypeByName(account.AccountType.Name);

            if (accountType != null)
            {
                account.AccountType = null;
                account.AccountTypeId = accountType.Id;
            }
        }

        private void UpdateProperties(BankAccount destination, BankAccountDto source)
        {
            destination.Balance = source.Balance;
            destination.BonusPoints = source.BonusPoints;
            destination.IsClosed = source.IsClosed;
        }

        #endregion
    }
}
