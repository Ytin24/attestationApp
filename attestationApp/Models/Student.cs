using System;
using System.Collections.Generic;

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
        if(obj == null)
            return false;
        if(obj.GetType() != typeof(Student))
            return false;

        var item = obj as Student;
        if (item.FullName != FullName)
            return false;
        else if (item.LastName != LastName)
            return false;
        else if (item.Patronymic != Patronymic)
            return false;
        else if ( item.GenderId!= GenderId)
            return false;
        else if (item.Birthdate!= Birthdate)
            return false;
        else 
            return true;
    }
}
