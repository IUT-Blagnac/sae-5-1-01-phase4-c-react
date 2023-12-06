using backend.Data;
using backend.Data.Models;
using backend.Services.Interfaces;

namespace backend.Services.Class;

public class SubjectService : ISubjectService
{
    private readonly EntityContext _context;

    public SubjectService(EntityContext context)
    {
        _context = context;
    }
    
    public List<Subject> GetSubjectsBySaeId(Guid idSae)
    {
        var query = from s in _context.Saes
            join su in _context.Subjects on s.id equals su.id_sae
            where s.id == idSae
            select su;

        return query.ToList();
    }
}