using backend.Data;
using backend.Data.Models;
using backend.Services.Interfaces;

namespace backend.Services.Class
{
    public class GroupService : IGroupService
    {
        private readonly EntityContext _context;

        public GroupService(EntityContext context)
        {
            _context = context;
        }
    
        public List<Group> GetGroups()
        {
            return _context.Groups.ToList();
        }

        public Group CreateGroup(string name, bool is_apprenticeship)
        {
            Group? group = _context.Groups.FirstOrDefault(x => x.name == name);

            if (group is not null)
                throw new Exception("Group already exits");

            var new_group = new Group
            {
                id = Guid.NewGuid(),
                name = name,
                is_apprenticeship = is_apprenticeship
            };

            _context.Groups.Add(new_group);

            _context.SaveChanges();

            return new_group;   
        }
    }
}
