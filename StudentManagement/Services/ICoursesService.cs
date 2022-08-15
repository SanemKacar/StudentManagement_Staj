using CoursesManagement.Models;
namespace CoursesManagement.Services
{
    public interface ICoursesService
    {
        List<Courses> Get();
        Courses Get(string id);
        Courses Create(Courses course);
        void Update(string id, Courses course);
        void Remove(string id);
    }
}
