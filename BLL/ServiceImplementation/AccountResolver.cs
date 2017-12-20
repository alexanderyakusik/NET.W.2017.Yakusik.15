using System;
using BLL.Interface.Entities;

namespace BLL.ServiceImplementation
{
    internal static class AccountResolver
    {
        public static Type GetBankAccountType(AccountType accountType)
        {
            string fullTypeName = typeof(BankAccount).AssemblyQualifiedName;
            fullTypeName = fullTypeName.Replace("BankAccount", $"{accountType}Account");

            return Type.GetType(fullTypeName);
        }

        public static Type GetBankAccountType(string accountType)
        {
            return GetBankAccountType(GetEnumAccountType(accountType));
        }

        public static string GetBankAccountTypeName(Type accountType)
        {
            return accountType.Name.Replace("Account", string.Empty);
        }

        public static BankAccount CreateAccount(string accountType, string accountId, string firstName, string lastName)
        {
            Type type = GetBankAccountType(GetEnumAccountType(accountType));

            return (BankAccount)Activator.CreateInstance(type, accountId, firstName, lastName);
        }

        private static AccountType GetEnumAccountType(string accountType)
        {
            Enum.TryParse(accountType, out AccountType result);

            return result;
        }
    }
}
