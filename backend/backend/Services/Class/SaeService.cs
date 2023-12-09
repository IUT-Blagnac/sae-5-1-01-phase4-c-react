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
        private readonly IUserTeamService _userTeamService;
        
        public SaeService(EntityContext context, IUserTeamService userTeamService)
        {
            _context = context;
            _userTeamService = userTeamService;
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
                total_nb_groups = _context.SaeGroups.Where(sg => sg.id_sae == sae.id).Count(),
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
                             total_nb_groups = group_sae.Count(),
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
                             total_nb_student = character_sae.Count()
                         }).ToList();

            return query;
        }

        public SaeAdminResponse GetSaeNbStudent(Guid saeId)
        {
            // var query = from s in _context.Saes
            //             join c in _context.Characters on s.id equals c.id_sae
            //             where s.id == saeId
            //             group c by new
            //             {
            //                 s.id,
            //                 s.name,
            //                 s.description,
            //                 s.max_student_per_group,
            //                 s.max_group_per_subject,
            //                 s.min_group_per_subject,
            //                 s.min_student_per_group,
            //                 s.state
            //             }
            //     into character_sae
            //             select new SaeAdminResponse()
            //             {
            //                 id = character_sae.Key.id,
            //                 name = character_sae.Key.name,
            //                 description = character_sae.Key.description,
            //                 min_student_per_group = character_sae.Key.min_student_per_group,
            //                 max_student_per_group = character_sae.Key.max_student_per_group,
            //                 min_group_per_subject = character_sae.Key.min_group_per_subject,
            //                 max_group_per_subject = character_sae.Key.max_group_per_subject,
            //                 total_nb_student = character_sae.Count()
            // //             };
            
            var query = from s in _context.Saes
                join sg in _context.SaeGroups on s.id equals sg.id_sae
                join g in _context.Groups on sg.id_group equals g.id
                join u in _context.Users on g.id equals u.id_group
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
                into student_sae
                select new SaeAdminResponse()
                {
                    id = student_sae.Key.id,
                    name = student_sae.Key.name,
                    description = student_sae.Key.description,
                    min_student_per_group = student_sae.Key.min_student_per_group,
                    max_student_per_group = student_sae.Key.max_student_per_group,
                    min_group_per_subject = student_sae.Key.min_group_per_subject,
                    max_group_per_subject = student_sae.Key.max_group_per_subject,
                    state = student_sae.Key.state,
                    total_nb_student = student_sae.Count()
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
                            state = group_sae.Key.state,
                            total_nb_groups = group_sae.Count()
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

        public Sae? GetSae(Guid saeId)
        {
            var query = from s in _context.Saes
                where s.id == saeId
                select s;
            //check if SAE found in the good state
            return query.FirstOrDefault();
        }

        public SaeAdminResponse SetSaeToPendingWishes(Guid saeId)
        {
            var sae = GetSae(saeId);
            if (sae is not { state: State.PENDING_USERS })
            {
                throw new HttpRequestException("Sae in the required state not found", null, HttpStatusCode.NotFound);
            }

            //try to generate groups
            // find all users assigned to the SAE

            if (sae.min_student_per_group == null)
            {
                throw new HttpRequestException("This selected Sae hasn't got any minimum students per group defined", null, HttpStatusCode.Forbidden);
            }

            if (sae.max_student_per_group == null)
            {
                throw new HttpRequestException("This selected Sae hasn't got any maximum students per group defined", null, HttpStatusCode.Forbidden);
            }

            var groups = _context.SaeGroups.Where(sg => sg.id_sae == saeId).Select(sg => sg.id_group).ToList();

            var userTeams = new List<UserTeam>();
            var nbTeams = 0;
            
            foreach (var groupid in groups)
            {
                var nbTeam = (sae.min_student_per_group + sae.max_student_per_group) / 2;

                var teams = new List<Team>();
                var colors = new List<string>() { "red", "blue", "green", "yellow", "purple", "orange", "pink", "brown", "black", "white" };
                
                for (int i = 0; i < nbTeam; i++)
                {
                    teams.Add(new Team
                    {
                        id = Guid.NewGuid(),
                        name = "Team " + i,
                        color = colors[i],
                        id_sae = saeId
                    });
                }
                
                foreach (var team in teams)
                {
                    _context.Teams.Add(team);
                    _context.SaveChanges();
                }
                
                // _context.SaveChanges();
                
                var users = from u in _context.Users
                            where u.id_group == groupid
                            select u.id;
                
                nbTeams += teams.Count;
                
                var userTeam = _userTeamService.GenTeams(users.ToList(), teams.Select(t => t.id).ToList());
                
                userTeams.AddRange(userTeam);
            }
            
            //if group generation successful pass sae to next stat
            sae.state = State.PENDING_WISHES;

            _context.SaveChanges();

            return new SaeAdminResponse(sae)
            {
                total_nb_teams = nbTeams,
                total_nb_student = userTeams.Count

            };
        }

        public SaeAdminResponse SetSaeToLaunched(Guid saeId)
        {
            
            //get sae info and check if good state
            var sae = GetSae(saeId);
            if (sae is not { state: State.PENDING_WISHES })
            {
                throw new HttpRequestException("Sae in the required state not found", null, HttpStatusCode.NotFound);
            }
            
            //get teams assigned to sae

            var query1 = from t in _context.Teams
                where t.id_sae == saeId
                select t;

            var teams = query1.ToList();
            
            //get subjects

            var query2 = from s in _context.Subjects
                where s.id_sae == saeId
                select s;
            
            var subjects = query2.ToList();
            
            var teamWishes = _context.TeamWishes.Where(wish => subjects.Contains(wish.subject)).ToList();

            List<TeamSubject> assignments = AssignTeamsToSubjects(teams, subjects, teamWishes);

            // Display the assignments
            foreach (TeamSubject assignment in assignments)
            {
                _context.TeamSubjects.Add(assignment);
            }
            sae.state = State.LAUNCHED;
            //remove wishes
            _context.TeamWishes.RemoveRange(teamWishes);
            _context.SaveChanges();
            //add the routes

            return new SaeAdminResponse(sae);
        }

        public static List<TeamSubject> AssignTeamsToSubjects(List<Team?> teams, List<Subject> subjects, List<TeamWish> teamWishes)
        {
            List<TeamSubject> assignments = new List<TeamSubject>();
            Dictionary<Guid, bool> assignedTeams = new Dictionary<Guid, bool>();

            // Assign teams based on wished subjects and preferences
            foreach (TeamWish teamWish in teamWishes)
            {
                Team? team = teams.FirstOrDefault(t => t.id == teamWish.id_team);

                if (team != null && !assignedTeams.ContainsKey(team.id))
                {
                    assignments.Add(new TeamSubject { id_team = team.id, id_subject = teamWish.id_subject });
                    assignedTeams[team.id] = true;
                }
            }

            // Assign remaining teams to subjects without preferences
            foreach (Subject subject in subjects)
            {
                int? teamsForSubject = subject.sae.max_group_per_subject - assignments.Count(a => a.id_subject == subject.id);

                List<Team?> remainingTeamsForSubject = teams
                    .Where(t => !assignedTeams.ContainsKey(t.id))
                    .ToList();

                for (int i = 0; i < teamsForSubject && i < remainingTeamsForSubject.Count; i++)
                {
                    Team? team = remainingTeamsForSubject[i];
                    if (team != null)
                    {
                        assignments.Add(new TeamSubject { id_team = team.id, id_subject = subject.id });
                        assignedTeams[team.id] = true;
                    }
                }
            }

            return assignments;
        }


    }
}
