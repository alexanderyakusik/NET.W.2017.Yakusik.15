using System;
using BLL.Interface.Entities;
using BLL.ServiceImplementation;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using Moq;
using NUnit.Framework;
using Services.Interface.Interfaces;

namespace BLL.Tests
{
    [TestFixture]
    public class BankAccountServiceTests
    {
        [Test]
        public void CreateAccount_CorrectValuesPassed_AddsAnAccountToTheRepository()
        {
            var repositoryMock = new Mock<IRepository>();
            var accountIdGeneratorMock = new Mock<IAccountIdGeneratorService>();
            var bonusPointsCalculatorMock = Mock.Of<IBonusPointsCalculatorService>();
            repositoryMock.Setup(repository => repository.Add(It.Is<BankAccountDto>(dto => dto.FirstName == "1" && dto.LastName == "2" && dto.Id == "")));
            accountIdGeneratorMock.Setup(generatorService => generatorService.GenerateId(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Type>()))
                                  .Returns("");

            var service = new BankAccountService(repositoryMock.Object, accountIdGeneratorMock.Object, bonusPointsCalculatorMock);
            service.OpenAccount("1", "2", AccountType.Platinum);

            repositoryMock.Verify();
        }

        [Test]
        public void CloseAccount_CorrectValuesPassed_ClosesCorrectly()
        {
            var repositoryMock = new Mock<IRepository>();
            var accountIdGeneratorMock = Mock.Of<IAccountIdGeneratorService>();
            var bonusPointsCalculatorMock = Mock.Of<IBonusPointsCalculatorService>();
            repositoryMock.Setup(repository => repository.Update(It.Is<BankAccountDto>(dto => dto.IsClosed)));
            repositoryMock.Setup(repository => repository.GetAccountById(It.IsAny<string>()))
                          .Returns(new BankAccountDto { FirstName = "", LastName = "", Id = "", AccountType = typeof(PlatinumAccount) });

            var service = new BankAccountService(repositoryMock.Object, accountIdGeneratorMock, bonusPointsCalculatorMock);
            service.CloseAccount("");

            repositoryMock.Verify();
        }

        [Test]
        public void Deposit_CorrectValuesPassed_AddsMoneyCorrectly()
        {
            var repositoryMock = new Mock<IRepository>();
            var accountIdGeneratorMock = Mock.Of<IAccountIdGeneratorService>();
            var bonusPointsCalculatorMock = Mock.Of<IBonusPointsCalculatorService>();
            repositoryMock.Setup(repository => repository.Update(It.Is<BankAccountDto>(dto => dto.Balance == 1000)));
            repositoryMock.Setup(repository => repository.GetAccountById(It.IsAny<string>()))
                .Returns(new BankAccountDto { FirstName = "", LastName = "", Id = "", AccountType = typeof(PlatinumAccount) });

            var service = new BankAccountService(repositoryMock.Object, accountIdGeneratorMock, bonusPointsCalculatorMock);
            service.Deposit("", 1000);

            repositoryMock.Verify();
        }

        [Test]
        public void Withdraw_CorrectValuesPassed_SubtractsMoneyCorrectly()
        {
            var repositoryMock = new Mock<IRepository>();
            var accountIdGeneratorMock = Mock.Of<IAccountIdGeneratorService>();
            var bonusPointsCalculatorMock = Mock.Of<IBonusPointsCalculatorService>();
            repositoryMock.Setup(repository => repository.Update(It.Is<BankAccountDto>(dto => dto.Balance == 100)));
            repositoryMock.Setup(repository => repository.GetAccountById(It.IsAny<string>()))
                .Returns(new BankAccountDto { FirstName = "", LastName = "", Id = "", AccountType = typeof(PlatinumAccount), Balance = 2000 });

            var service = new BankAccountService(repositoryMock.Object, accountIdGeneratorMock, bonusPointsCalculatorMock);
            service.Withdraw("", 1900);

            repositoryMock.Verify();
        }
    }
}
