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

            foreach(SubjectForm subject in saeForm.subjects)
            {
                Guid subjectId = Guid.NewGuid();

                _context.Subjects.Add(new Subject
                {
                    id = subjectId,
                    name = subject.name,
                    description = subject.description,
                    id_sae = idSae
                });

                foreach (Guid categoryId in subject.categoriesId)
                {
                    _context.SubjectCategories.Add(new SubjectCategory
                    {
                        id_category = categoryId,
                        id_subject = subjectId
                    });
                }
            }

            _context.SaveChanges();
        }

        public List<Sae> GetSaeByUserId(Guid id)
        {
            var query = (from u in _context.Users
                join g in _context.Groups on u.id_group equals g.id
                join sg in _context.SaeGroups on g.id equals sg.id_group
                join s in _context.Saes on sg.id_sae equals s.id
                where u.id == id
                select new Sae()
                {
                    id = s.id,
                    name = s.name,
                    description = s.description,
                    min_student_per_group = s.min_student_per_group,
                    max_student_per_group = s.max_student_per_group,
                    min_group_per_subject = s.min_group_per_subject,
                    max_group_per_subject = s.max_group_per_subject,
                    state = s.state
                }).ToList();
        
            return query;
        }
    }
}
