

namespace Labb2Web.Shared
{
    public class CourseForList
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public CourseEnum.Level Level { get; set; }
        public CourseEnum.Status Status { get; set; }
    }
}
