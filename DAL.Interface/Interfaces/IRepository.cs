namespace DAL.Interface.Interfaces
{
    using System;
    using System.Collections.Generic;
    using DTO;

    public interface IRepository
    {
        /// <summary>
        /// Adds the specified <paramref name="bankAccountDto"/> to the repository.
        /// </summary>
        /// <param name="bankAccountDto">Bank account.</param>
        void Add(BankAccountDto bankAccountDto);

        /// <summary>
        /// Updates the specified <paramref name="bankAccountDto"/> in the repository.
        /// </summary>
        /// <param name="bankAccountDto">Bank account.</param>
        void Update(BankAccountDto bankAccountDto);

        /// <summary>
        /// Gets all the bank accounts.
        /// </summary>
        /// <returns>Enumeration of all the bank accounts.</returns>
        IEnumerable<BankAccountDto> GetAll();

        /// <summary>
        /// Gets the account with the specified <paramref name="accountId"/>. 
        /// </summary>
        /// <param name="accountId">Account id.</param>
        /// <returns>Specified bank account. If no such account found, returns null.</returns>
        BankAccountDto GetAccountById(string accountId);
    }
}
