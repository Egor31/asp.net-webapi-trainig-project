using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Teacher : BasePerson
    {
        [InverseProperty(nameof(Subject.TeacherNavigation))]
        public IEnumerable<Subject> Subjects { get; set; } = new List<Subject>();
    }
}