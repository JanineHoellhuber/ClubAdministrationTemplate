using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClubAdministration.Web.Pages.Members
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public MemberDto MemberDto { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var member = await _unitOfWork.MemberRepository.GetByIdAsync(id.Value);

            if(member == null)
            {
                return NotFound();
            }
            MemberDto = new MemberDto
            {
                FirstName = member.FirstName,
                LastName = member.LastName,
                Id = member.Id,

            };
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Member dbMember = await _unitOfWork.MemberRepository.GetByIdAsync(MemberDto.Id);

            if (dbMember == null)
            {
                return NotFound();
            }
            dbMember.FirstName = MemberDto.FirstName;
            dbMember.LastName = MemberDto.LastName;

            try
            {
                await _unitOfWork.SaveChangesAsync();
                return RedirectToPage("../Index");
            }
            catch (ValidationException validationException)
            {
                ValidationResult valResult = validationException.ValidationResult;
                ModelState.AddModelError("", valResult.ErrorMessage);
            }
            return Page();
        }
    }
}
