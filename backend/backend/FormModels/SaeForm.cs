using backend.Data.Models;

namespace backend.FormModels
{
    public class SaeForm
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<Guid> id_groups { get; set; }
        public int? min_students_per_group { get; set; }
        public int? max_students_per_group { get; set; }
        public int? min_groups_per_subject { get; set; }
        public int? max_groups_per_subject { get; set; }
        public List<Guid> id_coachs { get; set; }
        public List<Subject> subjects { get; set; }
    }
}
