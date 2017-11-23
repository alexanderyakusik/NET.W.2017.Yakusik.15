namespace Services.Interface.Interfaces
{
    public interface IBonusPointsCalculatorService
    {
        long CalculateDepositBonus(int balanceValue, int depositValue);

        long CalculateWithdrawPenalty(int balanceValue, int depositValue);
    }
}
