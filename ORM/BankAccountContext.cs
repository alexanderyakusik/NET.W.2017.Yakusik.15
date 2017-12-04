using ORM.Entities;

namespace ORM
{
    using System.Data.Entity;

    public class BankAccountContext : DbContext
    {
        public BankAccountContext()
            : base("name=BankAccounts")
        {
        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; }

        public virtual DbSet<AccountOwner> AccountOwners { get; set; }

        public virtual DbSet<AccountType> AccountTypes { get; set; }
    }
}