using System;
using System.Collections.Generic;
using System.Text;

namespace EZLogger.Test.Utility
{
    public class TestExceptionGenerator
    {
        public static Exception GetValidException(string message, Exception innerException = null)
        {
            try
            {
                throw new Exception(message, innerException);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
