using System;

namespace Loterias.Application.Utils.Exceptions
{
    public class ErrorEnteringDataException : Exception
    {
        public ErrorEnteringDataException(string message) : base(message)
        {

        }
    }
}
