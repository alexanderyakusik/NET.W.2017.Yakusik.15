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

        public int OwnerId { get; set; }

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
