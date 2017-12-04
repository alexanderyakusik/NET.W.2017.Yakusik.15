using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;

namespace DAL.Fake.Repositories
{
    public class FakeRepository : IRepository
    {
        #region Private fields

        private List<BankAccountDto> list = new List<BankAccountDto>();

        #endregion

        #region Interfaces implementation

        /// <inheritdoc />
        public void Add(BankAccountDto bankAccountDto)
        {
            list.Add(bankAccountDto);
        }

        /// <inheritdoc />
        public void Update(BankAccountDto bankAccountDto)
        {
            BankAccountDto account = list.First(item => item.Id == bankAccountDto.Id);

            UpdateProperties(account, bankAccountDto);
        }

        /// <inheritdoc />
        public IEnumerable<BankAccountDto> GetAll()
        {
            return list;
        }

        /// <inheritdoc />
        public BankAccountDto GetAccountById(string accountId)
        {
            return list.FirstOrDefault(account => account.Id == accountId);
        }

        #endregion

        #region Private methods

        private void UpdateProperties(BankAccountDto destinationAccount, BankAccountDto sourceAccount)
        {
            destinationAccount.IsClosed = sourceAccount.IsClosed;
            destinationAccount.Balance = sourceAccount.Balance;
            destinationAccount.BonusPoints = sourceAccount.BonusPoints;
        }

        #endregion
    }
}
