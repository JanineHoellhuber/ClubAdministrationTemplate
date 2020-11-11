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

    

    }
}