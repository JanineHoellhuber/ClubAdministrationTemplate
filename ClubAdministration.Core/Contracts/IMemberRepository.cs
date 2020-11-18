using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
  public interface IMemberRepository
    {

        bool CheckDuplicateMember(Member memeber);
        Task<Member> GetByIdAsync(int value);
        Task<string[]> GetMemberNamesAsync();
    }
}
