namespace backend.Utils
{
    public static class RoleAccesses
    {
        public const string Admin = "Admin";

        public const string Teacher = Admin + ", Teacher";

        public const string Student = Teacher + ", Student";
    }
}
