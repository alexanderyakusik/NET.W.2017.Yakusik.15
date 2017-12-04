using System;
using BLL.Interface.Entities;
using BLL.ServiceImplementation;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class EntitiesMappers
    {
        public static BankAccountDto ToAccountDto(this BankAccount account, string accountType)
        {
            return new BankAccountDto
            {
                AccountType = accountType,
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
                AccountResolver.GetBankAccountType(accountDto.AccountType),
                accountDto.Id,
                accountDto.FirstName,
                accountDto.LastName,
                accountDto.Balance,
                accountDto.BonusPoints,
                accountDto.IsClosed);
        }
    }
}
