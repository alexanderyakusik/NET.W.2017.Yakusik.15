using System.Collections.Generic;

namespace ORM.Entities
{
    public class AccountOwner
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
