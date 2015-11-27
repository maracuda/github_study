namespace PrefixLibrary.Models
{
    /// <summary>
    ///     Сущность репозитория
    /// </summary>
    public class PrefixView
    {
        public PrefixView()
        {
        }

        public PrefixView(string id, string prefix_string)
        {
            Id = id;
            PrefixString = prefix_string;
        }

        /// <summary>
        ///     Идентификатор префикса(подсети)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Строковое представление префикса
        /// </summary>
        public string PrefixString { get; set; }

        public override string ToString()
        {
            return Id + ',' + PrefixString;
        }
    }
}