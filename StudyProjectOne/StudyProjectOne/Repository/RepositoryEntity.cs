namespace StudyProjectOne.Repository
{
    /// <summary>
    ///     Сущность репозитория
    /// </summary>
    public class RepositoryEntity
    {
        public string Id { get; set; }
        public string PrefixString { get; set; }

        public RepositoryEntity(string id, string prefix_string)
        {
            Id = id;
            PrefixString = prefix_string;
        }
    }
}