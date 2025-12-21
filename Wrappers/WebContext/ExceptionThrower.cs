using System;
using System.Web;

namespace Wrappers.WebContext
{
    public class ExceptionThrower
    {
        private readonly bool _isUnitTest;

        public ExceptionThrower()
        {
            _isUnitTest = false;
        }

        public ExceptionThrower(bool isUnitTest)
        {
            _isUnitTest = isUnitTest;
        }

        /// <summary>
        /// This is for unit testing.
        /// </summary>
        /// <param name="httpCode"></param>
        /// <param name="message"></param>
        public void ThrowHttpException(int httpCode, string message)
        {
            if(_isUnitTest)
            {
                throw new InvalidOperationException(string.Format("Http error: {0} {1}",httpCode,message));
            }

            throw new HttpException(httpCode, message);
        }
    }
}
