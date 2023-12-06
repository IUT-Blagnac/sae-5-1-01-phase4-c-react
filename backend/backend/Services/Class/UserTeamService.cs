using backend.Data;
using backend.Data.Models;
using backend.Services.Interfaces;

namespace backend.Services.Class;

public class UserTeamService: IUserTeamService
{
    private readonly EntityContext _context;

    public UserTeamService(EntityContext context)
    {
        _context = context;
    }


    public UserTeam AddUserTeam(Guid id_user, Guid id_team, string role)
    {
        var userTeam = new UserTeam
        {
            id_user = id_user,
            id_team = id_team,
            role = role
        };

        _context.UserTeams.Add(userTeam);
        _context.SaveChangesAsync();

        return userTeam;
    }

    public void RemoveUserTeam(Guid id_user, Guid id_team)
    {
        var userTeam = _context.UserTeams.FirstOrDefault(x => x.id_team == id_team && x.id_user == id_user);

        _context.UserTeams.Remove(userTeam);
        _context.SaveChangesAsync();
    }

    public List<UserTeam> GenTeams(List<Guid> users, List<Guid> teams)
    {
        List<Player> players = new List<Player>();
        
        foreach (var u in users)
        {
            players.Add(new Player(u, globalCharacterNote(u)));
        }

        List<List<Player>> groups = SimulatedAnnealing(players, teams.Count);

        List<UserTeam> userTeams = new List<UserTeam>();
        
        for (int i = 0; i < teams.Count; i++)
        {
            var group = groups[i];

            foreach (var player in group)
            {
                var ut = new UserTeam
                {
                    id_user = player.userId,
                    id_team = teams[i],
                    role = "Member"
                };
                
                userTeams.Add(ut);
                
                _context.UserTeams.Add(ut);
            }
        }

        _context.SaveChanges();

        return userTeams;
    }

    private int globalCharacterNote(Guid userId)
    {
        var charService = new CharacterService(_context);

        var charSkills = charService.getCharacterByUserId(userId);

        var sum = 0;

        foreach (var cs in charSkills)
        {
            sum += cs.confidence_level;
        }

        return sum;
    }
    
    // StandardDeviation = Ecart type
    private double ComputeStandardDeviationAverage(List<List<Player>> groups)
    {
        List<double> avgs = groups.Select(group =>
            group.Count > 0 ? group.Select(p => (double)p.skillsLevels).Sum() / group.Count : 0).ToList();

        double globalAvg = avgs.Sum() / avgs.Count();

        double standardDeviation = Math.Sqrt(avgs.Select(avg => Math.Pow(avg - globalAvg, 2)).Sum() / avgs.Count());

        return standardDeviation;
    }
    
    // Simulated annealing = Recuit simule
    private List<List<Player>> SimulatedAnnealing(List<Player> players, int nbGroups, double initalTemp = 1.0,
        double refreshFactor = 0.95, int nbIteration = 1000)
    {
        int sizeGroup = players.Count / nbGroups;
        int remainingPlayers = players.Count % nbGroups; // Calculate the remaining players

        List<List<Player>> groups = new List<List<Player>>(Enumerable.Range(0, nbGroups)
            .Select(i => players.GetRange(i * sizeGroup, sizeGroup).ToList()));

        double bestStandardDivision = ComputeStandardDeviationAverage(groups);

        double temp = initalTemp;
        Random random = new Random();

        for (int iteration = 0; iteration < nbIteration; iteration++)
        {
            List<Player> group1 = groups[random.Next(groups.Count)];
            List<Player> group2 = groups[random.Next(groups.Count)];
            
            while (group1 == group2)
            {
                group2 = groups[random.Next(groups.Count)];
            }
            
            Player player1 = group1[random.Next(group1.Count)];
            Player player2 = group2[random.Next(group2.Count)];

            int index1 = groups.FindIndex(g => g.Contains(player1));
            int index2 = groups.FindIndex(g => g.Contains(player2));
    
            groups[index1].Remove(player1);
            groups[index2].Remove(player2);
            groups[index1].Add(player2);
            groups[index2].Add(player1);
    
            double newStandardDeviation = ComputeStandardDeviationAverage(groups);

            if (newStandardDeviation < bestStandardDivision ||
                random.NextDouble() < Math.Exp(bestStandardDivision - newStandardDeviation) / temp)
            {
                bestStandardDivision = newStandardDeviation;
            }
            else
            {
                groups[index1].Remove(player2);
                groups[index2].Remove(player1);
                groups[index1].Add(player1);
                groups[index2].Add(player2);
            }

            var size = 0;
            foreach (var g in groups)
            {
                size += g.Count;
            }

            Console.WriteLine("size : " + size);

            temp *= refreshFactor;
        }

        // Distribute remaining players
        for (int i = 0; i < remainingPlayers; i++)
        {
            groups[i].Add(players[sizeGroup * nbGroups + i]);
        }

        return groups;
    }


    private class Player
    {
        public Guid userId;
        public int skillsLevels;

        public Player(Guid uId, int i)
        {
            userId = uId;
            skillsLevels = i;
        }
    }
}