namespace StudyProjectOne.Repository
{
    /// <summary>
    ///     Сущность репозитория
    /// </summary>
    public class RepositoryEntity
    {
        public RepositoryEntity(string id, string prefix_string)
        {
            Id = id;
            PrefixString = prefix_string;
        }

        public string Id { get; set; }
        public string PrefixString { get; set; }
    }
}