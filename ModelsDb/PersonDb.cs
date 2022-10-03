using System.ComponentModel.DataAnnotations;

namespace Models;

public abstract class PersonDb
{
    [Key] public Guid Id { get; set; }
    public int Bonus { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public int PassportId { get; set; }
    public DateTime DateBirth { get; set; }
}