using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Web.Pages
{
  public class IndexModel : PageModel
  {
        [BindProperty]
        public int SectionId { get; set; }
        public List<Section> SectionList { get; set; }
        public List<MemberDto> Members { get; set; }


        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> OnGet()
        {
            var sectionList = await _unitOfWork.SectionRepository.GetAllAsync();
            SectionList = sectionList.ToList();
            SectionId = sectionList.First().Id;
            Members = await _unitOfWork.SectionRepository.GetMemebersToSection(SectionId);


            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return await OnGet();
            var sectionList = await _unitOfWork.SectionRepository.GetAllAsync();
            SectionList = sectionList.ToList();
            var members = await _unitOfWork.SectionRepository.GetMemebersToSection(SectionId);
            Members = members;
            return Page();
        }
  }
}
