using System;
using JetBrains.Annotations;

namespace ArmutLocalStackSample.Core
{
    public static class Contract
    {
        [AssertionMethod]
        public static void Requires<TException>([AssertionCondition(AssertionConditionType.IS_TRUE)]bool condition) where TException : Exception
        {
            if (!condition)
            {
                TException exception = Activator.CreateInstance<TException>();
                throw exception;
            }
        }

        [AssertionMethod]
        public static void Requires<TException>([AssertionCondition(AssertionConditionType.IS_TRUE)]bool condition, string userMessage) where TException : Exception
        {
            if (!condition)
            {
                TException exception = (TException)Activator.CreateInstance(typeof(TException), userMessage);
                throw exception;
            }
        }
    }
}