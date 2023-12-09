using backend.Data;
using backend.Data.Models;
using backend.FormModels;
using backend.Services.Interfaces;
using System.Net;

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

            foreach (Guid id_group in saeForm.id_groups)
            {
                _context.SaeGroups.Add(new SaeGroup
                {
                    id_group = id_group,
                    id_sae = idSae
                });
            }

            foreach (Guid id_coach in saeForm.id_coachs)
            {
                _context.SaeCoaches.Add(new SaeCoach
                {
                    id_coach = id_coach,
                    id_sae = idSae
                });
            }

            foreach (SubjectForm subject in saeForm.subjects)
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
                         select s);

            return query.ToList();
        }

        public List<SaeAdminResponse> GetSaes()
        {
            return _context.Saes.Select(sae => new SaeAdminResponse()
            {
                id = sae.id,
                name = sae.name,
                description = sae.description,
                min_student_per_group = sae.min_student_per_group,
                max_student_per_group = sae.max_student_per_group,
                min_group_per_subject = sae.min_group_per_subject,
                max_group_per_subject = sae.max_group_per_subject,
                total_group = _context.SaeGroups.Where(sg => sg.id_sae == sae.id).Count(),
                state = sae.state
            }).ToList();
        }

        public List<SaeAdminResponse> GetSaeAdminNbGroupByUserId(Guid id)
        {
            var query = (from u in _context.Users
                         join sc in _context.SaeCoaches on u.id equals sc.id_coach
                         join s in _context.Saes on sc.id_sae equals s.id
                         join sg in _context.SaeGroups on s.id equals sg.id_sae
                         where u.id == id
                         where s.state != State.CLOSED
                         group new { s.id, s.name, s.description, s.max_student_per_group, s.max_group_per_subject, s.min_group_per_subject, s.min_student_per_group, sg.id_group, s.state } by new { s.id, s.name, s.description, s.max_student_per_group, s.max_group_per_subject, s.min_group_per_subject, s.min_student_per_group, s.state } into group_sae
                         select new SaeAdminResponse()
                         {
                             id = group_sae.Key.id,
                             name = group_sae.Key.name,
                             description = group_sae.Key.description,
                             min_student_per_group = group_sae.Key.min_student_per_group,
                             max_student_per_group = group_sae.Key.max_student_per_group,
                             min_group_per_subject = group_sae.Key.min_group_per_subject,
                             max_group_per_subject = group_sae.Key.max_group_per_subject,
                             total_group = group_sae.Count(),
                             state = group_sae.Key.state
                         }).ToList();

            return query;
        }

        public List<SaeAdminResponse> GetSaeAdminNbStudentByUserId(Guid id)
        {
            var query = (from u in _context.Users
                         join sc in _context.SaeCoaches on u.id equals sc.id_coach
                         join s in _context.Saes on sc.id_sae equals s.id
                         join c in _context.Characters on s.id equals c.id_sae
                         where u.id == id
                         group c by new { s.id, s.name, s.description, s.max_student_per_group, s.max_group_per_subject, s.min_group_per_subject, s.min_student_per_group, } into character_sae
                         select new SaeAdminResponse()
                         {
                             id = character_sae.Key.id,
                             name = character_sae.Key.name,
                             description = character_sae.Key.description,
                             min_student_per_group = character_sae.Key.min_student_per_group,
                             max_student_per_group = character_sae.Key.max_student_per_group,
                             min_group_per_subject = character_sae.Key.min_group_per_subject,
                             max_group_per_subject = character_sae.Key.max_group_per_subject,
                             total_student = character_sae.Count()
                         }).ToList();

            return query;
        }

        public SaeAdminResponse GetSaeNbStudent(Guid saeId)
        {
            var query = from s in _context.Saes
                        join c in _context.Characters on s.id equals c.id_sae
                        where s.id == saeId
                        group c by new
                        {
                            s.id,
                            s.name,
                            s.description,
                            s.max_student_per_group,
                            s.max_group_per_subject,
                            s.min_group_per_subject,
                            s.min_student_per_group,
                            s.state
                        }
                into character_sae
                        select new SaeAdminResponse()
                        {
                            id = character_sae.Key.id,
                            name = character_sae.Key.name,
                            description = character_sae.Key.description,
                            min_student_per_group = character_sae.Key.min_student_per_group,
                            max_student_per_group = character_sae.Key.max_student_per_group,
                            min_group_per_subject = character_sae.Key.min_group_per_subject,
                            max_group_per_subject = character_sae.Key.max_group_per_subject,
                            total_student = character_sae.Count()
                        };

            return query.FirstOrDefault();
        }

        public SaeAdminResponse GetSaeNbGroup(Guid saeId)
        {
            var query = from s in _context.Saes
                        join sg in _context.SaeGroups on s.id equals sg.id_sae
                        where s.id == saeId
                        group sg by new
                        {
                            s.id,
                            s.name,
                            s.description,
                            s.max_student_per_group,
                            s.max_group_per_subject,
                            s.min_group_per_subject,
                            s.min_student_per_group,
                            s.state
                        }
                into group_sae
                        select new SaeAdminResponse()
                        {
                            id = group_sae.Key.id,
                            name = group_sae.Key.name,
                            description = group_sae.Key.description,
                            min_student_per_group = group_sae.Key.min_student_per_group,
                            max_student_per_group = group_sae.Key.max_student_per_group,
                            min_group_per_subject = group_sae.Key.min_group_per_subject,
                            max_group_per_subject = group_sae.Key.max_group_per_subject,
                            total_group = group_sae.Count()
                        };

            return query.FirstOrDefault();
        }

        public List<User> GetUsersAssignedToSae(Guid saeId)
        {
            var query = from u in _context.Users
                        join g in _context.Groups on u.id_group equals g.id
                        where g.sae_groups.FirstOrDefault(sg => sg.id_sae == saeId) != null
                        select u;
            return query.ToList();
        }

        public List<User> GetUsersWithCharactersAssignedToSae(Guid saeId)
        {
            var query = from u in _context.Users
                        join c in _context.Characters on u.id equals c.id_user
                        join s in _context.Saes on c.id_sae equals s.id
                        where s.id == saeId
                        select u;

            return query.ToList();


        }

        public SaeAdminResponse SetSaeToPendingWishes(Guid saeId)
        {
            var query = from s in _context.Saes
                        where s.id == saeId && s.state == State.PENDING_USERS
                        select s;
            var sae = query.FirstOrDefault();
            if (sae == null)
            {
                //check if no SAE found in the good state
                throw new HttpRequestException("Sae in the required state not found", null, HttpStatusCode.NotFound);
                return null;
            }

            //try to generate groups
            // find all users assigned to the SAE

            var users = GetUsersWithCharactersAssignedToSae(saeId);

            if (sae.min_student_per_group == null)
            {
                throw new HttpRequestException("This selected Sae hasn't got any minimum students per group defined", null, HttpStatusCode.Forbidden);
            }



            int currentGroup = 0;
            Team currTeam = null;
            int currTeamNbMembers = 0;
            List<Team> teams = new List<Team>();
            Random r = new Random();
            foreach (var user in users.OrderBy(x => r.Next()))
            {
                // foreach random user in users assign to group untill group amount == floor(users/minusers) then start looping the groups and adding the remaining users
                if (currTeam == null || (teams.Count < Math.Floor(users.Count / (decimal)sae.min_student_per_group)))
                {
                    currTeamNbMembers = 0;
                    currTeam = new Team()
                    {
                        id = Guid.NewGuid(),
                        name = "Equipe " + (teams.Count + 1),
                        id_sae = saeId,
                        color = "", //list sae colors,
                        creator_challenge = new List<Challenge>(),
                        sae = sae,
                        target_challenge = new List<Challenge>(),
                        user_team =
                            new List<UserTeam>(), // create UserTeam on additions to the team and add them to this list
                        team_subject = new List<TeamSubject>(),
                        wish = new List<TeamWish>(),

                    };
                    teams.Add(currTeam);
                    _context.Teams.Add(currTeam);
                }

                if (teams.Count > Math.Floor(users.Count / (decimal)sae.min_student_per_group))
                {
                    //loop through groups and add remaining users
                    if (teams.Count > 0)
                    {
                        if (currentGroup == teams.Count)
                        {
                            break;
                        }
                        currTeam = teams[currentGroup];
                        currentGroup++;
                    }

                }
                var userTeam = new UserTeam()
                {
                    id_user = user.id,
                    user = user,
                    role = "membre", //default to membre
                    id_team = currTeam.id,
                    team = currTeam,
                };
                _context.UserTeams.Add(userTeam);
                currTeam.user_team.Add(userTeam);
                //user.user_team.Add(userTeam);
                currTeamNbMembers++;
            }






            //if group generation successful pass sae to next stat
            sae.state = State.PENDING_WISHES;

            _context.SaveChanges();

            return new SaeAdminResponse(sae)
            {
                total_group = teams.Count,
                total_student = users.Count

            };
        }
    }
}
