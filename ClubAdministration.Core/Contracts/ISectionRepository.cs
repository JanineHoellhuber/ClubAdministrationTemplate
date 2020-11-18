using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
  public interface ISectionRepository
  {
        Task<IEnumerable<Section>> GetAllAsync();
        Task<List<MemberDto>> GetMemebersToSection(int sectionId);
        Task<string[]> GetSectionsForMember(string lastname, string firstname);
    }
}
