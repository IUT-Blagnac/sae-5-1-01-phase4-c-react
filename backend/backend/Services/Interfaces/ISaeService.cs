using backend.Data.Models;
using backend.FormModels;

namespace backend.Services.Interfaces
{
    public interface ISaeService
    {
        public void CreateSae(SaeForm saeForm);
        public List<Sae> GetSaeByUserId(Guid id);
        public List<SaeAdminResponse> GetSaeAdminNbGroupByUserId(Guid id);
        public List<SaeAdminResponse> GetSaeAdminNbStudentByUserId(Guid id);
        public SaeAdminResponse GetSaeNbGroup(Guid saeId);
        public SaeAdminResponse GetSaeNbStudent(Guid saeId);
        public SaeAdminResponse SetSaeToPendingWishes(Guid saeId);
    }
}
