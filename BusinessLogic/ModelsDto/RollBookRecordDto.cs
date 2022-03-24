using System;

namespace BusinessLogic.ModelsDto
{
    public class RollBookRecordDto : BaseDto
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}