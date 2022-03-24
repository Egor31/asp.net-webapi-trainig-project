using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Subject : BaseEntity
    {
        public int TeacherId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(TeacherId))]
        public Teacher TeacherNavigation { get; set; }

        public string Title { get; set; }
    }
}