using System;
using System.Net;

namespace StudyProjectOne.Models
{
    /// <summary>
    /// Класс префикса, хранит адрес сети в числовом виде
    /// </summary>
    public class Prefix
    {
        public Prefix(string id, string prefix_string)
        {
            Id = id;
            Network = BitConverter.ToInt32(IPAddress.Parse(prefix_string.Split('/')[0]).GetAddressBytes(), 0);
            PrefixLength = int.Parse(prefix_string.Split('/')[1]);

        }
        public string Id { get; private set; }
        public int Network { get; private set; }
        public int PrefixLength { get; private set; }

        new public string ToString()
        {
            return new IPAddress(BitConverter.GetBytes(Network)) + "/" + PrefixLength;
        }
    }
}