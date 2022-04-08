using System;

namespace Loterias.Application.Utils.Exceptions
{
    public class ErrorRetrievingDataException : Exception
    {
        public ErrorRetrievingDataException(string message = "Error retrieving data from csv") : base(message)
        {

        }
    }
}
