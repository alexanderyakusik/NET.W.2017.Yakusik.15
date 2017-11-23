using System;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using DependencyResolver;
using Ninject;

namespace ConsolePL
{
    public class Program
    {
        public static readonly IKernel resolver;

        static Program()
        {
            resolver = new StandardKernel();
            resolver.ConfigurateResolver();
        }

        public static void Main(string[] args)
        {
            IBankAccountService service = resolver.Get<IBankAccountService>();

            string firstId = service.OpenAccount("Alexander", "Yakusik", AccountType.Platinum);
            string secondId = service.OpenAccount("Daniil", "Gasyul", AccountType.Gold);

            BankAccount first = service.GetAccountById(firstId);
            BankAccount second = service.GetAccountById(secondId);

            Console.WriteLine($"First account: {first.Id} {first.FirstName} {first.LastName} {first.GetType().Name}");
            Console.WriteLine($"\nSecond account: {second.Id} {second.FirstName} {second.LastName} {second.GetType().Name}");

            Console.WriteLine("\nReplenishing each account with 2000.");
            service.Deposit(first.Id, 2000);
            service.Deposit(second.Id, 2000);

            Console.WriteLine($"\nFirst account bonus points: {service.GetAccountById(firstId).BonusPoints}");
            Console.WriteLine($"Second account bonus points: {service.GetAccountById(secondId).BonusPoints}");

            Console.WriteLine($"\nTrying to withdraw more money than there actually is.");

            try
            {
                service.Withdraw(first.Id, 3000);
                Console.WriteLine("Successfully withdrawn 3000.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Unable to withdraw 3000.");
            }

            Console.WriteLine("\nWithdrawing 1500 from each account.");
            service.Withdraw(first.Id, 1500);
            service.Withdraw(second.Id, 1500);

            Console.WriteLine($"\nFirst account bonus points: {service.GetAccountById(firstId).BonusPoints}");
            Console.WriteLine($"Second account bonus points: {service.GetAccountById(secondId).BonusPoints}");

            Console.WriteLine("\nClosing first account");
            service.CloseAccount(first.Id);

            Console.WriteLine("\nTrying to replenish closed account with 100.");
            try
            {
                service.Deposit(first.Id, 100);
                Console.WriteLine("Replenished account successfully.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Couldn't replenish closed account.");
            }

            Console.ReadLine();
        }
    }
}
