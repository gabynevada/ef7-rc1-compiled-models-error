namespace Ef7Rc1Error.Models;

public class Patient
{
  public Guid PatientId { get; set; }

  // Model relationships
  public ICollection<User> User { get; set; } = new HashSet<User>();
}
