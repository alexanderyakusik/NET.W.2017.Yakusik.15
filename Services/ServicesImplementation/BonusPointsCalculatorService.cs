using Services.Interface.Interfaces;

namespace Services.ServicesImplementation
{
    public class BonusPointsCalculatorService : IBonusPointsCalculatorService
    {
        /// <inheritdoc />
        public long CalculateDepositBonus(int balanceValue, int depositValue)
        {
            return balanceValue * depositValue;
        }

        /// <inheritdoc />
        public long CalculateWithdrawPenalty(int balanceValue, int depositValue)
        {
            return balanceValue + depositValue;
        }
    }
}
