using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
  public class MemberSectionRepository : IMemberSectionRepository
  {
    private readonly ApplicationDbContext _dbContext;
            
    public MemberSectionRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<MemberSection> memberSections) 
      => await _dbContext.AddRangeAsync(memberSections);


        public async Task<IEnumerable<MemberDto>> GetBySectionWithMemberAsync(int sectionId)
        {
            var members = await _dbContext.MemberSections
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
            members.ForEach(m => m.CountSections = _dbContext.MemberSections.Count(ms => ms.MemberId == m.Id));
            return members;
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await _dbContext.Members
                .SingleAsync(s => s.Id == id);
        }

        public void Update(Member memeberInDb)
        {
             _dbContext.Members
                .Update(memeberInDb);
        }
    }
}