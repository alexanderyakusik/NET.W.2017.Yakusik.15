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
                Id = account.AccountNumber,
                IsClosed = account.IsClosed
            };
        }

        public static AccountOwner ToOrmOwner(this AccountOwnerDto accountOwnerDto)
        {
            return new AccountOwner
            {
                Id = accountOwnerDto.Id,
                FirstName = accountOwnerDto.FirstName,
                LastName = accountOwnerDto.LastName
            };
        }

        public static AccountOwnerDto ToDto(this AccountOwner accountOwner)
        {
            return new AccountOwnerDto
            {
                Id = accountOwner.Id,
                FirstName = accountOwner.FirstName,
                LastName = accountOwner.LastName
            };
        }
    }
}
