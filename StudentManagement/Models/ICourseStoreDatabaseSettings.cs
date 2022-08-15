namespace CoursesManagement.Models
{
    public interface ICourseStoreDatabaseSettings
    {
        string CoursesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
