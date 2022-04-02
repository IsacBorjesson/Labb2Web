namespace Labb2Web.Server.DAL.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
