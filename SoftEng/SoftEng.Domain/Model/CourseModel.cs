namespace SoftEng.Domain.Model
{
    public class CourseModel
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int Enrolled { get; set; }
        public string Schedules { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
