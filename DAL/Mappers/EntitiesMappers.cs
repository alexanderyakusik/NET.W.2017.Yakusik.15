using DAL.Interface.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class EntitiesMappers
    {
        public static BankAccount ToOrmBankAccount(this BankAccountDto dto)
        {
            return new BankAccount
            {
                AccountNumber = dto.Id,
                AccountOwner = new AccountOwner
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                },
                AccountType = new AccountType
                {
                    Name = dto.AccountType
                },
                Balance = dto.Balance,
                BonusPoints = dto.BonusPoints,
                IsClosed = dto.IsClosed
            };
        }

        public static BankAccountDto ToDto(this BankAccount account)
        {
            return new BankAccountDto
            {
                AccountType = account.AccountType.Name,
                Balance = account.Balance,
                BonusPoints = account.BonusPoints,
                FirstName = account.AccountOwner.FirstName,
                Id = account.AccountNumber,
                IsClosed = account.IsClosed,
                LastName = account.AccountOwner.LastName
            };
        }
    }
}
