using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

    }
}