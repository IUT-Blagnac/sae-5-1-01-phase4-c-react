using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;

namespace backend.Services.Class
{
    public class SaeService : ISaeService
    {

        private readonly EntityContext _context;

        public SaeService(EntityContext context)
        {
            _context = context;
        }

        public void CreateSae(SaeForm saeForm)
        {
            Guid idSae = Guid.NewGuid();

            Sae newSae = new()
            {
                id = idSae,
                name = saeForm.name,
                description = saeForm.description,
                min_student_per_group = saeForm.min_students_per_group,
                max_student_per_group = saeForm.max_students_per_group,
                min_group_per_subject = saeForm.min_groups_per_subject,
                max_group_per_subject = saeForm.max_groups_per_subject,
                state = State.PENDING_USERS,
            };

            _context.Saes.Add(newSae);
            _context.SaveChanges();

            foreach(Guid id_group in saeForm.id_groups)
            {
                _context.SaeGroups.Add(new SaeGroup
                {
                    id_group = id_group,
                    id_sae = idSae
                });
            }

            foreach(Guid id_coach in saeForm.id_coachs)
            {
                _context.SaeCoaches.Add(new SaeCoach
                {
                    id_coach = id_coach,
                    id_sae = idSae
                });
            }

            _context.SaveChanges();
        }
    }
}
