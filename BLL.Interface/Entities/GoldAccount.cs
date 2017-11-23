namespace BLL.Interface.Entities
{
    public class GoldAccount : BankAccount
    {
        #region Private fields

        private const int DefaultBalanceValue = 5;
        private const int DefaultDepositValue = 3;

        #endregion

        #region Ctors

        /// <inheritdoc />
        public GoldAccount(string accountId, string firstName, string lastName) : base(accountId, firstName, lastName)
        {
        }

        /// <inheritdoc />
        public GoldAccount(string accountId, string firstName, string lastName, decimal balance, long bonusPoints, bool isClosed) :
            base(accountId, firstName, lastName, balance, bonusPoints, isClosed)
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
