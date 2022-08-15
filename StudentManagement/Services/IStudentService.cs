using StudentManagement.Models;

namespace StudentManagement.Services
{
    public interface IStudentService
    {
        List<Student> Get();
        Student Get(string id);
        Student Create(Student student);
        List<Student> Get(string name, int age);
        void Update(string id, Student student);
        void Remove(string id);

    }
}
