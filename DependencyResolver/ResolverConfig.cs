using System.Data.Entity;
using BLL.Interface.Interfaces;
using BLL.ServiceImplementation;
using DAL.Fake.Repositories;
using DAL.Interface.Interfaces;
using DAL.Repositories;
using Ninject;
using ORM;
using Services.Interface.Interfaces;
using Services.ServicesImplementation;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<IBankAccountService>().To<BankAccountService>();
            kernel.Bind<IRepository>().To<BankAccountRepository>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            kernel.Bind<IBonusPointsCalculatorService>().To<BonusPointsCalculatorService>();
            kernel.Bind<IAccountIdGeneratorService>().To<AccountIdGeneratorService>();
            kernel.Bind<DbContext>().To<BankAccountContext>().InSingletonScope();
        }
    }
}
