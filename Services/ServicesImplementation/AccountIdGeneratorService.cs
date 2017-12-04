using System;
using Services.Interface.Interfaces;

namespace Services.ServicesImplementation
{
    public class AccountIdGeneratorService : IAccountIdGeneratorService
    {
        /// <inheritdoc />
        public string GenerateId(string firstName, string lastName, string accountType)
        {
            const int RandomMinimumId = 1_000_000;
            const int RandomMaximumId = 9_999_999;

            Random random = new Random(DateTime.Now.Millisecond);

            return $"{accountType[0]}{firstName[0]}{lastName[0]}{random.Next(RandomMinimumId, RandomMaximumId)}";
        }
    }
}
