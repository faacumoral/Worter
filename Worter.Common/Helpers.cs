using System;
using System.Runtime.CompilerServices;
using Worter.Common.Constants;

namespace Worter.Common
{
    public static class Helpers
    {
        public static Environment GetEnvironment()
        {
            var value = System.Environment.GetEnvironmentVariable(CONSTANTS.Keys.ENVIRONMENT);

            // default value
            if (string.IsNullOrEmpty(value)) return CONSTANTS.Keys.DEFAULT_ENVIRONMENT;

            value = value.ToUpper();

            switch (value)
            {
                case CONSTANTS.Keys.DEVELOPMENT:
                    return Environment.DEVELOPMENT;
                case CONSTANTS.Keys.PRODUCTION:
                    return Environment.PRODUCTION;
            }

            throw new ArgumentException($"'{value}' environment is not a valid one. Check Worter.Common.Environment enum.");
        }

        public static string GetConnectionString()
        {
            var env = GetEnvironment();
            
            switch (env)
            {
                case Environment.DEVELOPMENT:
                    return "Server=.\\SQLExpress;Database=Worter;Trusted_Connection=True;";
                case Environment.PRODUCTION:
                    var password_decode = System.Environment.GetEnvironmentVariable(CONSTANTS.Keys.PASSWORD_ENCRYPT);
                    var encoded_connectionstring = System.Environment.GetEnvironmentVariable(CONSTANTS.Keys.CONNECTION_STRING);
                    return FMCW.Seguridad.Encriptador.Desencriptar(encoded_connectionstring, password_decode);
            }
            throw new ArgumentException($"Not define connection string for '{env.ToString()}' environment.");
        }


    }
}
