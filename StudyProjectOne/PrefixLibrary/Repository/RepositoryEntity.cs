namespace PrefixLibrary.Repository
{
    /// <summary>
    ///     Сущность репозитория
    /// </summary>
    public class RepositoryEntity
    {
        /// <summary>
        /// Идентификатор префикса(подсети)
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Строковое представление префикса
        /// </summary>
        public string PrefixString { get; set; }

        public RepositoryEntity(string id, string prefix_string)
        {
            Id = id;
            PrefixString = prefix_string;
        }
    }
}