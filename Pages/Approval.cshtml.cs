using Azure.Core;
using IT_Service_Deck.Models;
using IT_Service_Deck.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IT_Service_Deck.Pages
{
    [Authorize(Roles = "approver")]
    public class ApprovalModel : PageModel
    {
        private readonly AppDbContext _context;

        UserManager<AppUser> _userManager;

        public Requests Request { get; set; }

        public AppUser Employee { get; set; }

        [BindProperty]
        public int RequestId { get; set; }

        [BindProperty]
        public string Decision { get; set; }

        [BindProperty]
        public string RequesterId {  get; set; }


        public ApprovalModel(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? RequestId)
        {
            if (RequestId == null)
            {
                return NotFound();
            }

            Request = await _context.Requests
                .Include(r => r.Employee)
                .FirstOrDefaultAsync(r => r.Id == RequestId);


            if (Request == null)
            {
                return NotFound();
            }

            Employee = await _context.Users.FirstOrDefaultAsync(u => u.Id == Request.E_id);


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == RequestId);
            if (request == null)
            {
                return NotFound();
            }

            // Update status based on decision
            if (Decision == "approve")
            {
                if (currentUser.Id == "57b6deec-f45d-4d70-92e6-61dc57dc60fc")
                {
                    request.Status = "OneApproved";
                }
                else if (currentUser.Id == "291ba253-18da-45ec-b511-d32c17496a70")
                {
                    request.Status = "TwoApproved";
                }
            }
            else if (Decision == "reject")
            {
                request.Status = "Rejected";
            }

            _context.Requests.Update(request);
            await _context.SaveChangesAsync();

            // Add data into RequestHistory table
            var requestHistory = new RequestHistory
            {
                RequestId = RequestId,
                ApproverId = currentUser.Id,
                Decision = Decision == "approve" ? "Approved" : "Rejected",
                DecisionAt = DateTime.Now
            };

            _context.RequestHistory.Add(requestHistory);
            await _context.SaveChangesAsync();

            // Redirect to another page after processing the request
            return RedirectToPage("/ApproverPage");
        }

    }
}
