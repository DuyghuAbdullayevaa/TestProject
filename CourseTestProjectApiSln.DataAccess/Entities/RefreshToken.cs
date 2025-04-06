using CourseTestProjectApiSln.DataAccess.Entities.Base;


namespace CourseTestProjectApiSln.DataAccess.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
