namespace Services.Interface.Interfaces
{
    public interface IAccountIdGeneratorService
    {
        /// <summary>
        /// Generates a unique identifier.
        /// </summary>
        /// <param name="firstName">Owners first name.</param>
        /// <param name="lastName">Owners last name.</param>
        /// <param name="targetObjectType">Type of the target object.</param>
        /// <returns>Unique identifier.</returns>
        string GenerateId(string firstName, string lastName, string targetObjectType);
    }
}
