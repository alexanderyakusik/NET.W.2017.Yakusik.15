namespace Services.Interface.Interfaces
{
    public interface IBonusPointsCalculatorService
    {
        /// <summary>
        /// Calculates the deposit bonus.
        /// </summary>
        /// <param name="balanceValue">Value of the balance.</param>
        /// <param name="depositValue">Value of the deposit.</param>
        /// <returns>Deposit bonus.</returns>
        long CalculateDepositBonus(int balanceValue, int depositValue);

        /// <summary>
        /// Calculates the withdraw penalty.
        /// </summary>
        /// <param name="balanceValue">Value of the balance.</param>
        /// <param name="depositValue">Value of the deposit.</param>
        /// <returns>Withdraw penalty.</returns>
        long CalculateWithdrawPenalty(int balanceValue, int depositValue);
    }
}
