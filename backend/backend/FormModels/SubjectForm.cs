using backend.Data.Models;

namespace backend.FormModels
{
    public class SubjectForm
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<Guid> categoriesId { get; set; }
    }
}
