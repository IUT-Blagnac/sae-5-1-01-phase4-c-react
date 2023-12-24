using backend.Data.Models;

namespace backend.FormModels;

public class SaeAdminResponse : Sae
{
    public SaeAdminResponse(Sae sae)
    {
        id = sae.id;
        name = sae.name;
        description = sae.description;
        min_student_per_group = sae.min_student_per_group;
        max_student_per_group = sae.max_student_per_group;
        min_group_per_subject = sae.min_group_per_subject;
        max_group_per_subject = sae.max_group_per_subject;
        state = sae.state;
    }

    public SaeAdminResponse(){}

    public int total_nb_groups { get; set; }
    public int total_nb_teams { get; set; }
    public int total_nb_student { get; set; }
}