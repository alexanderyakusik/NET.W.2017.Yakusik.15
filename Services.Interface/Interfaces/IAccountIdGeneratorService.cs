using System;

namespace Services.Interface.Interfaces
{
    public interface IAccountIdGeneratorService
    {
        string GenerateId(string firstName, string lastName, Type accountType);
    }
}
