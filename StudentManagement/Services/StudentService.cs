using StudentManagement.Models;
using MongoDB.Driver;
using System.Diagnostics;
using CoursesManagement.Models;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        public readonly IMongoCollection<Student> _students;
        private readonly IMongoCollection<Courses> _courses;

        public StudentService(IStudentStoreDatabaseSettings settings, ICourseStoreDatabaseSettings courses, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);

            _students = database.GetCollection<Student>(settings.StudentCoursesCollectionName);
            _courses = database.GetCollection<Courses>(courses.CoursesCollectionName);

        }
        public Student Create(Student student)
        {
            //Student student1 = new Student { Age = 10 ,Gender = "K"};
            //_students.InsertOne(student1);
           
            _students.InsertOne(student);
            return student;

        }
        public List<Student> Create(List<Student> studentL)
        {
            //Student student1 = new Student { Age = 10, Gender = "K" };
            //_students.InsertOne(student1);
            _students.InsertMany(studentL);
            return studentL;

        }

        public List<Courses> GetCourses()
        {

            return _courses.Find(courses => true).ToList();
        }
        public List<Student> Get()
        {
            IMongoQueryable<Student> results0 = 
                from student in _students.AsQueryable()
                where student.Age > 21 && student.Age < 27
                select student;
            foreach (Student student in results0)
            {
                Debug.WriteLine("{0}: {1}",student.Name,student.Age);
            }

            var results1 =
                from student in _students.AsQueryable()
                where student.Age > 21 && student.Age < 27
                select new { student.Name, student.Age };
            foreach (var student in results1)
            {
                Debug.WriteLine("{0}: {1}", student.Name, student.Age);
            }

            List<Student> students1 = new List<Student>();
            students1=_students.Find(student => student.Age == 24).SortBy(n => n.Name).ToList();
            foreach (var item in students1)
            {
                Debug.WriteLine(item.Name);
            }
            /*Debug.WriteLine("StudentCount: "+_students.Find(student => true).Count());
            List<Student> students = new List<Student>();
            students=_students.Find(student => true).ToList();
            foreach (Student student in students)
            {
                Debug.WriteLine("Student Name: " + student.Name + " - Course count: " + student.Courses.Count()+ " - Course 1:" +student.Courses.First().Name);
            }*/
            return _students.Find(student => true).ToList();
        }

        public Student Get(string id)
        {
            var filter = Builders<Student>.Filter.Eq("_id", new ObjectId(id));
            return _students.Find(filter).FirstOrDefault();
        }

        public List<Student> Get(string name, int age)
        {
            List<Student> students = new List<Student>();
            students = _students.Find(student => student.Name == name && student.Age == age).ToList().OrderByDescending(x => x.Age).ToList();
            foreach (var student in students.OrderByDescending(x=> x.Age))
            {
                //if (student.Name == "Sanem")
                //{
                //    student.Age = 23;
                //    //update
                //    _students.ReplaceOne(x => x.Id.ToString() == student.Name,student);

                //}
                List<Courses> courseL = new List<Courses>();
                
                foreach (var item in student.Courses)
                {
                    string course_name = "";
                    if (item.Name.Equals(""))
                    {
                        _courses.Find(_ => true).ToList().ForEach(vault =>
                        {
                            if (item.Id == vault.Id)
                            {
                                course_name = vault.Name;
                            }
                        });
                        courseL.Add(new Courses
                        {
                            Name = course_name,
                            Id = item.Id
                        });
                    }
                    else
                    {
                        courseL.Add(new Courses
                        {
                            Name = item.Name,
                            Id = item.Id
                        });
                    }
                }
                    students.Add(new Student
                    {
                        Id = student.Id,
                        Name = student.Name,
                        Age = student.Age,
                        Courses = courseL,
                        Gender = student.Gender,
                    });
                //var pResults = _students.Aggregate()
                //    .Match(new BsonDocument { { "name", "BOĞAÇ" } })
                //    .Project(new BsonDocument
                //    {
                //        { "_id",1},
                //        {"name",1 },
                //        {
                //            "courses", new BsonDocument{
                //                {
                //                    "$map", new BsonDocument{
                //                        {"input", "$courses"},
                //                        {"as","item" },
                //                        {
                //                            "in", new BsonDocument{
                //                                {
                //                                    "$convert", new BsonDocument{
                //                                        {"input","$$item" },
                //                                        {"to","objectId" }
                //                                    }
                //                                }
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    })
                //    .Lookup("courses","courses","_id","_courses")
                //    .Unwind("Courses")
                //    .Group(new BsonDocument
                //    {
                //        {"_id","$_id" },
                //        {
                //            "name", new BsonDocument
                //            {
                //                {"$first","$name" },
                //            }
                //        },
                //        {
                //            "Courses", new BsonDocument
                //            {
                //                {"$addToSet","$Courses"}
                //            } 
                //        }
                //    }).ToList();
                //Debug.WriteLine("HELLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLOOOOOOOOOOOOOOOOO");
                //Debug.WriteLine("dddddddddddddd: "+pResults.Count());
                //foreach (var pResult in pResults)
                //{
                //    Debug.WriteLine(pResult);
                //}
                //Debug.WriteLine("HELLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLOOOOOOOOOOOOOOOOO");



                /*
                var result = _students.Aggregate().Lookup<Student, Courses, RegisteredStudentsOnCourses>(_courses,
                    x => x.Courses,
                    z => z.Id,
                    y => y.Students
                    ).ToListAsync() ;
                Debug.WriteLine("***************************");
                Debug.WriteLine(result.ToString());
                Debug.WriteLine("***************************");
                Debug.WriteLine(result);
                Debug.WriteLine("***************************");
                */
                //Debug.WriteLine("Name: "+students.Last().Courses.Last().Name);
                foreach( var item in courseL)
                {
                    Debug.WriteLine(item.Id+" - "+item.Name);
                }

            }
            Debug.WriteLine("Sayı: "+students.Count());
            return _students.Find(student => student.Name == name && student.Age > age).ToList().OrderByDescending(x=> x.Age).ToList();

        }

        public void Remove(string id)
        {
             _students.DeleteOne(student => student.Id.Equals(id));
        }

        public void Update(string id, Student student)
        {
            _students.ReplaceOne(student => student.Id == id, student);
        }
    }
}
