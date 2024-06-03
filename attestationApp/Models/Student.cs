using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace attestationApp.Models;

public partial class Student
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateTime Birthdate { get; set; }

    public int? GenderId { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<Direction> Directions { get; set; } = new List<Direction>();
    public override bool Equals(object? obj)
    {
        

        if (obj == null || obj.GetType() != typeof(Student))
            return false;

        var item = obj as Student;
        Debug.WriteLine($"Expected: {item.FullName}, Actual: {FullName}");
        Debug.WriteLine($"Expected: {item.LastName}, Actual: {LastName}");
        Debug.WriteLine($"Expected: {item.Patronymic}, Actual: {Patronymic}");
        Debug.WriteLine($"Expected: {item.Birthdate}, Actual: {Birthdate}");
        Debug.WriteLine($"Expected: {item.GenderId}, Actual: {GenderId}");
        return FullName == item.FullName &&
               LastName == item.LastName &&
               Patronymic == item.Patronymic &&
               GenderId == item.GenderId &&
               Birthdate == item.Birthdate;
    }

}
