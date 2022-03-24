using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class RollBookRecord : BaseEntity
    {
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student StudentNavigation { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject SubjectNavigation { get; set; }

        public DateTime Date { get; set; }

        public bool IsPresent { get; set; }
    }
}