using Infrastructure.Models;

namespace WebApp.Models
{
    public class CoursesViewModel
    {
        public IEnumerable <Category>? Categories { get; set; }
        public IEnumerable<Course>? Courses { get; set; }
        public Pagination? pagination { get; set; }
       // public CourseRegistration? registration { get; set; }

    }
}
