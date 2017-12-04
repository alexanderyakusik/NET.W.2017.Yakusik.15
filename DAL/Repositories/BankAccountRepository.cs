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
        #region Interfaces implementations

        #region IRepository

        /// <inheritdoc />
        public void Add(BankAccountDto bankAccountDto)
        {
            var account = bankAccountDto.ToOrmBankAccount();

            using (var context = new BankAccountContext())
            {
                SetAccountForeignKeys(context, account);

                context.Entry(account).State = EntityState.Added;

                context.SaveChanges();
            }
        }

        /// <inheritdoc />
        public void Update(BankAccountDto bankAccountDto)
        {
            using (var context = new BankAccountContext())
            {
                var account = GetAccountByNumber(context, bankAccountDto.Id);

                UpdateProperties(account, bankAccountDto);

                context.SaveChanges();
            }
        }

        /// <inheritdoc />
        public IEnumerable<BankAccountDto> GetAll()
        {
            using (var context = new BankAccountContext())
            {
                return context.BankAccounts
                    .Include(account => account.AccountOwner)
                    .Include(account => account.AccountType)
                    .ToList()
                    .Select(account => account.ToDto());
            }
        }

        /// <inheritdoc />
        public BankAccountDto GetAccountById(string accountId)
        {
            using (var context = new BankAccountContext())
            {
                return context.BankAccounts
                    .Include(account => account.AccountOwner)
                    .Include(account => account.AccountType)
                    .FirstOrDefault(account => account.AccountNumber == accountId)
                    ?.ToDto();
            }
        }

        #endregion

        #endregion

        #region Private methods

        private AccountOwner GetAccountOwnerByName(BankAccountContext context, string firstName, string lastName)
        {
            return context.AccountOwners.FirstOrDefault(owner =>
                owner.FirstName == firstName && owner.LastName == lastName);
        }

        private AccountType GetAccountTypeByName(BankAccountContext context, string accountTypeName)
        {
            return context.AccountTypes.FirstOrDefault(accountType => accountType.Name == accountTypeName);
        }

        private BankAccount GetAccountByNumber(BankAccountContext context, string accountNumber)
        {
            return context.BankAccounts.FirstOrDefault(account => account.AccountNumber == accountNumber);
        }

        private void SetAccountForeignKeys(BankAccountContext context, BankAccount account)
        {
            var accountOwner = GetAccountOwnerByName(
                context,
                account.AccountOwner.FirstName,
                account.AccountOwner.LastName);

            if (accountOwner != null)
            {
                account.AccountOwner = null;
                account.AccountOwnerId = accountOwner.Id;
            }

            var accountType = GetAccountTypeByName(context, account.AccountType.Name);

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
