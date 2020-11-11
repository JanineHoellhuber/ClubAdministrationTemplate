using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using System.Collections.Generic;

namespace ClubAdministration.Core.Contracts
{
  public interface IMemberRepository
    {

        bool CheckDuplicateMember(Member memeber);
    }
}
