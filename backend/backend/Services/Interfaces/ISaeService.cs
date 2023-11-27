using backend.Data.Models;
using backend.FormModels;

namespace backend.Services.Interfaces
{
    public interface ISaeService
    {
        public void CreateSae(SaeForm saeForm);
        public List<Sae> GetSaeByUserId(Guid id);
    }
}
