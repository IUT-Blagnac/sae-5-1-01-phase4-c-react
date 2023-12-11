using backend.Data;
using backend.Data.Models;

namespace backend.Services.Interfaces;

public class TeamWishService : ITeamWishService
{
    private readonly EntityContext _context;

    public TeamWishService(EntityContext context)
    {
        _context = context;
    }

    public TeamWish AddWish(Guid team_id, Guid subject_id)
    {
        var new_wish = new TeamWish()
        {
            id_team = team_id,
            id_subject = subject_id,
        };

        _context.TeamWishes.Add(new_wish);
        _context.SaveChanges();

        return new_wish;
    }

    public void RemoveWish(Guid team_id, Guid subject_id)
    {
        var wish = _context.TeamWishes.FirstOrDefault(tw => tw.id_subject == subject_id && tw.id_team == team_id);

        _context.Remove(wish);
        _context.SaveChangesAsync();
    }

    private List<TeamWish> GetWishes(Guid? team_id = null, Guid? subject_id = null)
    {
        IEnumerable<TeamWish> wishes;

        if (team_id is not null && subject_id is not null)
        {
            wishes = _context.TeamWishes.Where(tw => tw.id_subject == subject_id && tw.id_team == team_id);
        }
        else if (team_id is not null)
        {
            wishes = _context.TeamWishes.Where(tw => tw.id_team == team_id);
        }
        else if (subject_id is not null)
        {
            wishes = _context.TeamWishes.Where(tw => tw.id_subject == subject_id);
        }
        else
        {
            wishes = _context.TeamWishes;
        }

        return wishes.ToList();
    }

    public List<TeamWish> GetWish(Guid team_id, Guid subject_id)
    {
        return GetWishes(team_id, subject_id);
    }

    public List<TeamWish> GetWishes()
    {
        return GetWishes();
    }

    public List<TeamWish> GetWishesBySubjectId(Guid subject_id)
    {
        return GetWishes(subject_id: subject_id);
    }

    public List<TeamWish> GetWishesByTeamId(Guid team_id)
    {
        return GetWishes(team_id: team_id);
    }
}