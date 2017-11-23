namespace BLL.Interface.Entities
{
    public class PlatinumAccount : BankAccount
    {
        #region Private fields

        private const long DefaultBalanceValue = 10;
        private const long DefaultDepositValue = 5;

        #endregion

        #region Ctors

        /// <inheritdoc />
        public PlatinumAccount(string accountId, string firstName, string lastName) : base(accountId, firstName, lastName)
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
