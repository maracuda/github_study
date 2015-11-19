namespace StudyProjectOne.Models
{
    /// <summary>
    ///     Модель префикса для передачи во View
    /// </summary>
    public class PrefixViewModel
    {
        public string Id { get; set; }
        public string PrefixString { get; set; }

        /// <summary>
        ///     Конструктор для преобразования из JSON
        /// </summary>
        public PrefixViewModel()
        {
        }

        public PrefixViewModel(string id, string prefix_string)
        {
            Id = id;
            PrefixString = prefix_string;
        }
    }
}