namespace BLL.Interface.Entities
{
    public class GoldAccount : BankAccount
    {
        #region Private fields

        private const long DefaultBalanceValue = 5;
        private const long DefaultDepositValue = 3;

        #endregion

        #region Ctors

        /// <inheritdoc />
        public GoldAccount(string accountId, string firstName, string lastName) : base(accountId, firstName, lastName)
        {
        }

        /// <inheritdoc />
        public GoldAccount(string accountId, string firstName, string lastName, decimal balance, long bonusPoints) :
            base(accountId, firstName, lastName, balance, bonusPoints)
        {
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override long BalanceValue
        {
            get => DefaultBalanceValue;
        }

        /// <inheritdoc />
        public override long DepositValue
        {
            get => DefaultDepositValue;
        }

        #endregion
    }
}
