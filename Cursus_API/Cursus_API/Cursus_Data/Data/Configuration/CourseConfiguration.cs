using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cursus_Data.Data.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course
                {
                    CourseId = "CS0001",
                    CategoryId = "CT0001",
                    Title = "C# Programming Basics",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0002",
                    CategoryId = "CT0002",
                    Title = "Java Fundamentals",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0003",
                    CategoryId = "CT0003",
                    Title = "Python for Beginners",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0004",
                    CategoryId = "CT0004",
                    Title = "Advanced C++ Programming",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0005",
                    CategoryId = "CT0001",
                    Title = "Advanced C# Topics",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0006",
                    CategoryId = "CT0002",
                    Title = "Java Spring Framework",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0007",
                    CategoryId = "CT0003",
                    Title = "Intermediate Python",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0008",
                    CategoryId = "CT0004",
                    Title = "Object-Oriented Design in C++",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0009",
                    CategoryId = "CT0005",
                    Title = "Introduction to C Programming",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0010",
                    CategoryId = "CT0006",
                    Title = "Concurrency in Go",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0011",
                    CategoryId = "CT0007",
                    Title = "Spring Boot Essentials",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0012",
                    CategoryId = "CT0008",
                    Title = "Thymeleaf Framework",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0013",
                    CategoryId = "CT0009",
                    Title = "Node.js Basics",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0014",
                    CategoryId = "CT0010",
                    Title = "React.js Fundamentals",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0015",
                    CategoryId = "CT0011",
                    Title = "React Native Development",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0016",
                    CategoryId = "CT0012",
                    Title = "Advanced React Native Topics",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0017",
                    CategoryId = "CT0001",
                    Title = "Intermediate C# Programming",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0018",
                    CategoryId = "CT0002",
                    Title = "Java EE Development",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0019",
                    CategoryId = "CT0003",
                    Title = "Python Data Analysis",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0020",
                    CategoryId = "CT0004",
                    Title = "Advanced C++ Techniques",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0021",
                    CategoryId = "CT0005",
                    Title = "C Programming for Embedded Systems",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0022",
                    CategoryId = "CT0006",
                    Title = "Go Web Development",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0023",
                    CategoryId = "CT0007",
                    Title = "Spring Boot Microservices",
                    InstructorId = "INS00000002",
                },
                new Course
                {
                    CourseId = "CS0024",
                    CategoryId = "CT0008",
                    Title = "Thymeleaf for Web Development",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0025",
                    CategoryId = "CT0009",
                    Title = "RESTful APIs with Node.js",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0026",
                    CategoryId = "CT0010",
                    Title = "React.js State Management",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0027",
                    CategoryId = "CT0011",
                    Title = "Advanced React Native UI Design",
                    InstructorId = "INS00000001",
                },
                new Course
                {
                    CourseId = "CS0028",
                    CategoryId = "CT0012",
                    Title = "React Native App Deployment",
                    InstructorId = "INS00000002",
                }
            );
        }
    }
}