namespace BLL.Interface.Entities
{
    public class PlatinumAccount : BankAccount
    {
        #region Private fields

        private const int DefaultBalanceValue = 10;
        private const int DefaultDepositValue = 5;

        #endregion

        #region Ctors

        /// <inheritdoc />
        public PlatinumAccount(string accountId, string firstName, string lastName) : base(accountId, firstName, lastName)
        {
        }

        /// <inheritdoc />
        public PlatinumAccount(string accountId, string firstName, string lastName, decimal balance, long bonusPoints) :
            base(accountId, firstName, lastName, balance, bonusPoints)
        {
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override int BalanceValue
        {
            get => DefaultBalanceValue;
        }

        /// <inheritdoc />
        public override int DepositValue
        {
            get => DefaultDepositValue;
        }

        #endregion
    }
}
