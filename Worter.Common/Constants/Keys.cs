using System;
using System.Collections.Generic;
using System.Text;

namespace Worter.Common.Constants
{
    public static partial class CONSTANTS
    {
        public static class Keys
        {
            public const Environment DEFAULT_ENVIRONMENT = Environment.DEVELOPMENT;
            
            public const string ENVIRONMENT = "ENVIRONMENT";
            public const string DEVELOPMENT = "DEVELOPMENT";
            public const string PRODUCTION = "PRODUCTION";

            public const string CONNECTION_STRING = "CONNECTION_STRING";
            public static string PASSWORD_ENCRYPT = "PASSWORD_ENCRYPT";
        }
    }
}
