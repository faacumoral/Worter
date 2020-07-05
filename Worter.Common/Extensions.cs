using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Worter.Common
{
    public static class Extensions
    {
        public static string GetConnectionString(this IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(Constants.CONSTANTS.Keys.WORTER_CONTEXT);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string cannot be blank.");
            }
            return connectionString;
        }

        public static string GetEncryptPassword(this IConfiguration configuration)
        {
            var encrypt = configuration[Constants.CONSTANTS.Keys.ENCRYPT_PASSWORD];
            if (string.IsNullOrEmpty(encrypt))
            {
                throw new ArgumentException("Encrypt password cannot be blank.");
            }
            return encrypt;
        }
    }
}
