namespace DAL.Interface.DTO
{
    public class BankAccountDto
    {
        /// <summary>
        /// Account type.
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// Gets account id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets first name of the account's owner.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets last name of the account's owner.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets current balance on the account.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets current bonus points on the account.
        /// </summary>
        public long BonusPoints { get; set; }

        /// <summary>
        /// Gets a value indicating whether the account is closed or not.
        /// </summary>
        public bool IsClosed { get; set; }
    }
}
