namespace backend.ApiModels.Output
{
    public class OutputGetTeachers
    {
        public List<TeacherSimplified> Teachers { get; set; } = new();
    }

    public class TeacherSimplified
    {
        public Guid id { get; set; }
        public string fullName { get; set; }
    }
}
