
using System.ComponentModel.DataAnnotations;

namespace Labb2Web.Shared
{
    public class CreateCourse
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Length { get; set; }
        [Required]
        public CourseEnum.Level Level { get; set; }
        [Required]
        public CourseEnum.Status Status { get; set; }
    }
}
