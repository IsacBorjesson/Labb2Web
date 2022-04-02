namespace Labb2Web.Server.DAL.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public CourseEnum.Level Level { get; set; }
        public CourseEnum.Status Status { get; set; }
        public ICollection<User> Users { get; set; }
    }

    
}
