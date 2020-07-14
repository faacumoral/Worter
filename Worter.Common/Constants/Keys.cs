using System;
using System.Collections.Generic;
using System.Text;

namespace Worter.Common.Constants
{
    public static partial class CONSTANTS
    {
        public const int WORDS_TO_RETURN = 5;

        public static class Keys
        {
            #region ENVIRONMENT
            public const Environment DEFAULT_ENVIRONMENT = Environment.DEVELOPMENT;

            public const string ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";

            public const string DEVELOPMENT = "Development";
            public const string PRODUCTION = "Production";
            #endregion

            public const string WORTER_CONTEXT = "WORTER_CONTEXT";

            public const string ENCRYPT_PASSWORD = "Encrypt_Password";

            #region JWT
            public const string JWT_SECRETKEY = "JWT_SecretKey";
            public const string JWT_ISSUER = "JWT_Issuer";
            public const string JWT_AUDIENCE = "JWT_Audience";
            #endregion
        }
    }
}
