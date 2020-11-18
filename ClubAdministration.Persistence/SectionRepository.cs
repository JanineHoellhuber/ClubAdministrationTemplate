using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
  public class SectionRepository : ISectionRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public SectionRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

        public async Task<IEnumerable<Section>> GetAllAsync()
        {
            return await _dbContext.Sections
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<List<MemberDto>> GetMemebersToSection(int sectionId)
        {
           var members =  await _dbContext.MemberSections
                .Include(ms => ms.Member)
                .Include(ms => ms.Section)
                .Where(ms => ms.SectionId == sectionId)
                .Select(ms => new MemberDto
                {
                    FirstName = ms.Member.FirstName,
                    LastName = ms.Member.LastName,
                    Id = ms.MemberId

                })
                .OrderBy(m => m.LastName)
                .ThenBy(m => m.FirstName)
                .ToListAsync();
            members.ForEach(m => 
                    m.CountSections = _dbContext.MemberSections.Count(ms => ms.MemberId == m.Id)
                );
            return members;
        }

    }
}