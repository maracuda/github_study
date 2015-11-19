using System;

namespace StudyProjectOne.Services
{
    /// <summary>
    ///     Класс префикса, хранит адрес сети в числовом виде
    /// </summary>
    public class Prefix
    {
        public Prefix(string id, string prefix_string)
        {
            Id = id;
            PrefixString = prefix_string;
            Network = Dot2LongIP(prefix_string.Split('/')[0]);
            PrefixLength = int.Parse(prefix_string.Split('/')[1]);
        }

        public string Id { get; private set; }
        public long Network { get; private set; }
        public int PrefixLength { get; private set; }
        public string PrefixString { get; private set; }
        public long UniqueNetworkId
        {
            get { return Network + PrefixLength; }
        }

        private static long Dot2LongIP(string dotted_ip)
        {
            var num = 0d;

            var arr_dec = dotted_ip.Split('.');
            for (var i = arr_dec.Length - 1; i >= 0; i--)
            {
                num += ((int.Parse(arr_dec[i])%256)*Math.Pow(256, (3 - i)));
            }

            return (long) num;
        }
    }
}