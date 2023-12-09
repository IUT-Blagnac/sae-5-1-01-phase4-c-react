using backend.Data.Models;

namespace backend.Services.Interfaces
{
    public interface ISubjectService
    {
        public List<Subject> GetSubjectsBySaeId(Guid idSae);
    }
}
