﻿namespace BLL.Interface.Entities
{
    public class BaseAccount : BankAccount
    {
        #region Private fields

        private const long DefaultBalanceValue = 2;
        private const long DefaultDepositValue = 1;

        #endregion

        #region Ctors

        /// <inheritdoc />
        public BaseAccount(string accountId, string firstName, string lastName) : base(accountId, firstName, lastName)
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