namespace DAL.Interface.DTO
{
    public class AccountOwnerDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets first name of the account's owner.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets last name of the account's owner.
        /// </summary>
        public string LastName { get; set; }
    }
}
