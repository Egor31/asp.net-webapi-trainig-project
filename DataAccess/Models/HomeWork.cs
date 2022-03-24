using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class HomeWork : BaseEntity
    {
        public int StudentId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(StudentId))]
        public Student StudentNavigation { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject SubjectNavigation { get; set; }

        public int Grade { get; set; }
    }
}