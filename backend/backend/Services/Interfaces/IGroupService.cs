using backend.Data.Models;

namespace backend.Services.Interfaces;

public interface IGroupService
{
        public List<Group> GetGroups();
        public Group CreateGroup(string name, bool is_apprenticeship);
}