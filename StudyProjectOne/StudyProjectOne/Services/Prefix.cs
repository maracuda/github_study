using System;
using System.Net;

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
            PrefixString = prefix_string.Split('/')[0];
            Network = Dot2LongIP(prefix_string.Split('/')[0]);//BitConverter.ToInt32(IPAddress.Parse(prefix_string.Split('/')[0]).GetAddressBytes(), 0);
            PrefixLength = int.Parse(prefix_string.Split('/')[1]);
        }

        public string Id { get; private set; }
        public long Network { get; private set; }
        public int PrefixLength { get; private set; }

        public string PrefixString { get; private set; }

        public long UniqueNetworkId { get { return Network + PrefixLength; } }
        private long Dot2LongIP(string dotted_ip)
        {
            int i;
            string[] arrDec;
            double num = 0;
            if (dotted_ip == "")
            {
                return 0;
            }
            arrDec = dotted_ip.Split('.');
            for (i = arrDec.Length - 1; i >= 0; i--)
            {
                num += ((int.Parse(arrDec[i])%256)*Math.Pow(256, (3 - i)));
            }
            return (long) num;
        }

        public new string ToString()
        {
            return PrefixString + "/" + PrefixLength;
        }
    }
}