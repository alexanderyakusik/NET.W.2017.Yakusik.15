namespace BLL.Interface.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Entities;

    public interface IBankAccountService
    {
        /// <summary>
        /// Opens a bank account of specified <paramref name="accountType"/> with zero balance and bonus points.
        /// </summary>
        /// <param name="firstName">Owners first name.</param>
        /// <param name="lastName">Owners last name.</param>
        /// <param name="accountType">Type of the bank account.</param>
        /// <exception cref="ArgumentNullException"><paramref name="firstName"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="lastName"/> is null.</exception>
        void OpenAccount(string firstName, string lastName, AccountType accountType);

        /// <summary>
        /// Closes the bank account with the specified <paramref name="accountId"/> and disables all the money operations.
        /// </summary>
        /// <param name="accountId">Account id.</param>
        /// <exception cref="ArgumentNullException"><paramref name="accountId"/> is null.</exception>
        void CloseAccount(string accountId);

        /// <summary>
        /// Adds the amount of money equal to <paramref name="value"/> to the bank account with specified <paramref name="accountId"/>.
        /// </summary>
        /// <param name="accountId">Account id.</param>
        /// <param name="value">Amount of money to deposit.</param>
        /// <exception cref="ArgumentNullException"><paramref name="accountId"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than zero.</exception>
        void Deposit(string accountId, decimal value);

        /// <summary>
        /// Withdraws the amount of money equal to <paramref name="value"/> to the bank account with specified <paramref name="accountId"/>.
        /// </summary>
        /// <param name="accountId">Account id.</param>
        /// <param name="value">Amount of money to withdraw.</param>
        /// <exception cref="ArgumentNullException"><paramref name="accountId"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than zero.</exception>
        void Withdraw(string accountId, decimal value);

        /// <summary>
        /// Gets all the bank accounts.
        /// </summary>
        /// <returns>Enumeration of all the bank accounts.</returns>
        IEnumerable<BankAccount> GetAll();

        /// <summary>
        /// Gets the account with the specified <paramref name="accountId"/>. 
        /// </summary>
        /// <param name="accountId">Account id.</param>
        /// <returns>Specified bank account. If no such account found, returns null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="accountId"/> is null.</exception>
        BankAccount GetAccountById(string accountId);
    }
}
