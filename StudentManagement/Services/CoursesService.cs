using CoursesManagement.Models;
using MongoDB.Driver;

namespace CoursesManagement.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly IMongoCollection<Courses> _courses;
        public CoursesService(ICourseStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _courses = database.GetCollection<Courses>(settings.CoursesCollectionName);

        }
        public Courses Create(Courses course)
        {
            _courses.InsertOne(course);
            return course;
        }

        public List<Courses> Get()
        {
            return _courses.Find(course=>true).ToList();
        }

        public Courses Get(string id)
        {
            return _courses.Find(course => course.Id.Equals(id)).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _courses.DeleteOne(course => course.Id.Equals(id));
        }

        public void Update(string id, Courses course)
        {
            _courses.ReplaceOne(course => course.Id.Equals(id), course);
        }
    }
}
