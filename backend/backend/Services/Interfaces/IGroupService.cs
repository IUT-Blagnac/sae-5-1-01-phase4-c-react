using backend.Data.Models;

namespace backend.Services.Interfaces;

public interface IGroupService
{
    public List<Group> getGroups();
}