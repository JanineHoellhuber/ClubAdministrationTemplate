using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
  public class MemberRepository : IMemberRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public MemberRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }


       public bool CheckDuplicateMember(Member member)
        {

            var duplicateMember = _dbContext.Members
                 .Where(m => m.LastName == member.LastName && m.FirstName == member.LastName);

            if(duplicateMember != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<Member> GetByIdAsync(int value)
        {
            return await _dbContext.Members.FirstAsync(m => m.Id == value);
        }

        public async Task<string[]> GetMemberNamesAsync()
        {
            return await _dbContext.Members
                .OrderBy(l => l.LastName)
                .Select(m => $"{m.LastName} {m.FirstName}")
                .ToArrayAsync();
        }

    }
}