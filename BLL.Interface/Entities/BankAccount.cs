namespace BLL.Interface.Entities
{
    using System;

    public abstract class BankAccount
    {
        #region Private fields

        private decimal balance;
        private long bonusPoints;

        #endregion

        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class. Creates bank account with zero balance and bonus points.
        /// </summary>
        /// <param name="accountId">Account number.</param>
        /// <param name="firstName">First name of the account's owner.</param>
        /// <param name="lastName">Last name of the account's owner.</param>
        protected BankAccount(string accountId, string firstName, string lastName)
        {
            this.Id = accountId ?? throw new ArgumentNullException($"{nameof(accountId)} cannot be null.");
            this.FirstName = firstName ?? throw new ArgumentNullException($"{nameof(firstName)} cannot be null.");
            this.LastName = lastName ?? throw new ArgumentNullException($"{nameof(lastName)} cannot be null.");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class. Creates bank account with zero balance and bonus points.
        /// </summary>
        /// <param name="accountId">Account number.</param>
        /// <param name="firstName">First name of the account's owner.</param>
        /// <param name="lastName">Last name of the account's owner.</param>
        /// <param name="balance">Account balance.</param>
        /// <param name="bonusPoints">Account bonus points.</param>
        protected BankAccount(string accountId, string firstName, string lastName, decimal balance, long bonusPoints) :
            this(accountId, firstName, lastName)
        {
            this.Balance = balance >= 0
                ? balance
                : throw new ArgumentOutOfRangeException($"{nameof(balance)} cannot be less than zero.");
            this.BonusPoints = bonusPoints >= 0
                ? bonusPoints
                : throw new ArgumentOutOfRangeException($"{nameof(bonusPoints)} cannot be less than zero.");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets account id.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets first name of the account's owner.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Gets last name of the account's owner.
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Gets current balance on the account.
        /// </summary>
        /// <exception cref="InvalidOperationException">Value is less than zero.</exception>
        public decimal Balance
        {
            get => this.balance;
            private set => this.balance = value >= 0
                                          ? value
                                          : throw new InvalidOperationException($"Balance cannot be less than zero.");
        }

        /// <summary>
        /// Gets current bonus points on the account.
        /// </summary>
        public long BonusPoints
        {
            get => this.bonusPoints;
            private set => this.bonusPoints = value >= 0 
                                              ? value 
                                              : 0;
        }

        /// <summary>
        /// Gets a value indicating whether the account is closed or not.
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Gets value of the balance. Depends on the account type.
        /// </summary>
        public abstract long BalanceValue { get; }

        /// <summary>
        /// Gets value of the account deposit. Depends on the account type.
        /// </summary>
        public abstract long DepositValue { get; }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds money on the account.
        /// </summary>
        /// <param name="amount">Amount of money to add.</param>
        /// <param name="depositBonus">Amount of the bonus points to add.</param>
        /// <exception cref="InvalidOperationException">Account is closed.</exception>
        public void Deposit(decimal amount, long depositBonus)
        {
            CheckClosedAccount();

            Balance += amount;
            BonusPoints += depositBonus;
        }

        /// <summary>
        /// Withdraws money from the account.
        /// </summary>
        /// <param name="amount">Amount of money to be withdrawn.</param>
        /// <param name="withdrawPenalty">Amount of bonus points to be withdrawn.</param>
        /// <exception cref="InvalidOperationException">Account is closed.</exception>
        public void Withdraw(decimal amount, long withdrawPenalty)
        {
            CheckClosedAccount();

            Balance -= amount;
            BonusPoints -= withdrawPenalty;
        }

        /// <summary>
        /// Closes account and disables all the money operations.
        /// </summary>
        public void Close()
        {
            IsClosed = true;
        }

        #endregion

        #region Private methods

        private void CheckClosedAccount()
        {
            if (IsClosed)
            {
                throw new InvalidOperationException($"The account is closed.");
            }
        }

        #endregion
    }
}
