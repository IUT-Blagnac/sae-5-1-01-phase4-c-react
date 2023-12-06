using backend.Data.Models;

namespace backend.Services.Interfaces;

public interface ITeamWishService
{
    public List<TeamWish> GetWishes();
    public List<TeamWish> GetWishesByTeamId(Guid team_id);
    public List<TeamWish> GetWishesBySubjectId(Guid subject_id);
    public List<TeamWish> GetWish(Guid team_id, Guid subject_id);
    public TeamWish AddWish(Guid team_id, Guid subject_id);
    public void RemoveWish(Guid team_id, Guid subject_id);

}