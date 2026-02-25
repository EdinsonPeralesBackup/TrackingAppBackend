namespace Tracking.Application.Common.Interface
{
    public interface ICurrentUser
    {
        string Id { get; set; }
        string Name { get; set; }
        string LastName { get; set; }
        string Birthday { get; set; }
        string Phone { get; set; }
        string FullName { get; set; }
        int IdRol { get; set; }
        string RolNombre { get; set; }
    }
}
