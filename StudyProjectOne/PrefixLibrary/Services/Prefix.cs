using System;
using System.Collections.Generic;

namespace PrefixLibrary.Services
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

        /// <summary>
        ///     Идентификатор префикса из входного набора
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        ///     Адрес сети
        /// </summary>
        public long Network { get; private set; }

        /// <summary>
        ///     Длина маски
        /// </summary>
        public int PrefixLength { get; private set; }

        /// <summary>
        ///     Строковое представление подсети
        /// </summary>
        public string PrefixString { get; private set; }

        /// <summary>
        ///     Идентификатор для алгоритма построения покрывающего набора: адрес сети + длина маски
        /// </summary>
        public long UniqueNetworkId
        {
            get { return Network + PrefixLength; }
        }

        /// <summary>
        ///     Метод перевода точечной нотации в long
        /// </summary>
        /// <param name="dotted_ip"></param>
        /// <returns></returns>
        public static long Dot2LongIP(string dotted_ip)
        {
            var num = 0d;

            var arr_dec = dotted_ip.Split('.');
            for (var i = arr_dec.Length - 1; i >= 0; i--)
            {
                num += ((int.Parse(arr_dec[i])%256)*Math.Pow(256, (3 - i)));
            }

            return (long) num;
        }

        /// <summary>
        ///     Реализация абстрактного класса Comparer для сортировки
        /// </summary>
        public class PrefixComparer : Comparer<Prefix>
        {
            public override int Compare(Prefix x, Prefix y)
            {
                if (Equals(x, y)) return 0;
                return x.Network == y.Network
                    ? x.PrefixLength.CompareTo(y.PrefixLength)
                    : x.Network.CompareTo(y.Network);
            }
        }
    }
}