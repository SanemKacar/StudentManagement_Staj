namespace CoursesManagement.Models
{
    public class CoursesStoreDatabaseSettings : ICourseStoreDatabaseSettings
    {
        public string CoursesCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
