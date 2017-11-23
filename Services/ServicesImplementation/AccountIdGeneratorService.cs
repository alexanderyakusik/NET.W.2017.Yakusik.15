﻿using System;
using Services.Interface.Interfaces;

namespace Services.ServicesImplementation
{
    public class AccountIdGeneratorService : IAccountIdGeneratorService
    {
        /// <inheritdoc />
        public string GenerateId(string firstName, string lastName, Type accountType)
        {
            const int randomMinimumId = 1_000_000;
            const int randomMaximumId = 9_999_999;

            Random random = new Random(DateTime.Now.Millisecond);

            return $"{accountType.Name[0]}{firstName[0]}{lastName[0]}{random.Next(randomMinimumId, randomMaximumId)}";
        }
    }
}