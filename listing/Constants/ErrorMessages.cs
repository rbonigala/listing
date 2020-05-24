using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listing.Constants
{
    public static class ErrorMessages
    {
        public static readonly string CONFIG_READING = "Error reading the configuration";
        internal static readonly string GLOBAL_EXCEPTION_MESSAGE = "Something went wrong {globalex}";
        internal static readonly string INTERNAL_SERVER_ERROR = "Internal Server Error";
    }
}
