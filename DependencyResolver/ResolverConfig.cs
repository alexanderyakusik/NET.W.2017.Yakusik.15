using BLL.Interface.Interfaces;
using BLL.ServiceImplementation;
using DAL.Interface.Interfaces;
using DAL.Repositories;
using Ninject;
using Services.Interface.Interfaces;
using Services.ServicesImplementation;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<IBankAccountService>().To<BankAccountService>();
            kernel.Bind<IRepository>().To<FakeRepository>();
            kernel.Bind<IBonusPointsCalculatorService>().To<BonusPointsCalculatorService>();
            kernel.Bind<IAccountIdGeneratorService>().To<AccountIdGeneratorService>();
        }
    }
}
