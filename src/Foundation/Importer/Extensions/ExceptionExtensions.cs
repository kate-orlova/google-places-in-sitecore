using System;
using System.Text;

namespace Importer.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetAllMessages(this Exception exception)
        {
            var innerException = exception;

            var sb = new StringBuilder();

            while (innerException != null)
            {
                sb.AppendLine(innerException.Message);

                innerException = innerException.InnerException;
            }

            return sb.ToString();
        }
    }
}