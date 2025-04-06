using CourseTestProjectApiSln.DataAccess.Entities.Base;


namespace CourseTestProjectApiSln.DataAccess.Entities
{
    public class Role : IEntity
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
