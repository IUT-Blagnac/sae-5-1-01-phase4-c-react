using backend.Data.Models;

namespace backend.FormModels;

public class SaeAdminResponse : Sae
{
    public int total_group { get; set; }
    public int total_student { get; set; }
}