using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Student : BasePerson
    {
        [InverseProperty(nameof(HomeWork.StudentNavigation))]
        public IEnumerable<HomeWork> HomeWorks { get; set; } = new List<HomeWork>();
    }
}