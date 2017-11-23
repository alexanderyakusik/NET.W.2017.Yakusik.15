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
        /// <exception cref="ArgumentNullException"><paramref name="bankAccountDto"/> is null.</exception>
        void Add(BankAccountDto bankAccountDto);

        /// <summary>
        /// Deletes the specified <paramref name="bankAccountDto"/> from the repository.
        /// </summary>
        /// <param name="bankAccountDto">Bank account.</param>
        /// <exception cref="ArgumentNullException"><paramref name="bankAccountDto"/> is null.</exception>
        void Delete(BankAccountDto bankAccountDto);

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
        /// <exception cref="ArgumentNullException"><paramref name="accountId"/> is null.</exception>
        BankAccountDto GetAccountById(string accountId);
    }
}
