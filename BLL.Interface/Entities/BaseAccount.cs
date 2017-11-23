namespace BLL.Interface.Entities
{
    public class BaseAccount : BankAccount
    {
        #region Private fields

        private const int DefaultBalanceValue = 2;
        private const int DefaultDepositValue = 1;

        #endregion

        #region Ctors

        /// <inheritdoc />
        public BaseAccount(string accountId, string firstName, string lastName) : 
            base(accountId, firstName, lastName)
        {
        }

        /// <inheritdoc />
        public BaseAccount(string accountId, string firstName, string lastName, decimal balance, long bonusPoints) :
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
