using System;
using System.Net;
using System.Text.RegularExpressions;

namespace PrefixLibrary.Services
{
    public static class PrefixValidator
    {
        public static string ValidatePrefixString(string prefix_string)
        {
            var regex = new Regex(".+/.+");

            if (!regex.IsMatch(prefix_string)) return "Неверный формат, используйте x.x.x.x/y";

            var network = prefix_string.Split('/')[0];
            int prefix_length;

            try
            {
                IPAddress.Parse(network);
                prefix_length = int.Parse(prefix_string.Split('/')[1]);
            }
            catch (Exception)
            {
                return "Неверный формат, используйте x.x.x.x/y";
            }

            if (prefix_length < 1 || prefix_length > 32)
            {
                return "Неверная длина префикса";
            }

            var network_digit = EditablePrefix.Dot2LongIP(network);
            var mask_digit = ((long) 1 << 32) - (1 << 32 - prefix_length);

            return network_digit != (network_digit & mask_digit)
                ? "Такой подсети не существует"
                : "";
        }

    }
}