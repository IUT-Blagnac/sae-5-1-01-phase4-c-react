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
    
    public List<Group> getGroups()
    {
        return _context.Groups.ToList();
    }
}
}
