using System;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class EntitiesMappers
    {
        public static BankAccountDto ToAccountDto(this BankAccount account)
        {
            return new BankAccountDto
            {
                AccountType = account.GetType(),
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                IsClosed = account.IsClosed,
                Balance = account.Balance,
                BonusPoints = account.BonusPoints
            };
        }

        public static BankAccount ToAccount(this BankAccountDto accountDto)
        {
            return (BankAccount)Activator.CreateInstance(
                accountDto.AccountType,
                accountDto.Id,
                accountDto.FirstName,
                accountDto.LastName,
                accountDto.Balance,
                accountDto.BonusPoints);
        }
    }
}
