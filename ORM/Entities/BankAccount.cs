namespace ORM.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public long BonusPoints { get; set; }

        public bool IsClosed { get; set; }

        public int AccountOwnerId { get; set; }

        public int AccountTypeId { get; set; }

        public virtual AccountOwner AccountOwner { get; set; }

        public virtual AccountType AccountType { get; set; }
    }
}
