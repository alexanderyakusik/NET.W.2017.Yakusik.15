using System.Collections.Generic;

namespace ORM.Entities
{
    public class AccountType
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
