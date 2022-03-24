using System.Collections.Generic;
using BusinessLogic.ModelsDto;

namespace BusinessLogic.ServiceInterfaces
{
    public interface IRollBookService
    {
        void CheckInStudent(int studentId, int subjectId, bool isStudentPresent);
        IEnumerable<RollBookRecordDto> GetRollBookRecordsByStudentId(int studentId);
        IEnumerable<RollBookRecordDto> GetRollBookRecordsBySubjectId(int subjectId);
    }
}