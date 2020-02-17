using System;
using System.Globalization;
using EcbForex.API.Domain.Models.Exceptions;

namespace EcbForex.API.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// use this method to convert string ("yyyy-MM-dd") to datetime 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str)
        {
            try
            {
                return DateTime.ParseExact(str, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                throw new BadRequestException($"invalid date format provided {str}");
            }
        }
    }
}
