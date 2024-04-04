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
    public class ApproverPageModel : PageModel
    {
        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        public string Message { get; set; }

        public ApproverPageModel(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<RequestHistoryViewModel> ApprovalHistory { get; set; }

        public class RequestHistoryViewModel
        {
            public string RequesterName { get; set; }
            public string RequesterDepartment { get; set; }
            public string RequesterEmail { get; set; }
            public string RequestType { get; set; }
            public DateTime RequestedAt { get; set; }
            public string Decision { get; set; }
            public DateTime DecisionAt { get; set; }
        }

        public IList<Requests> PendingRequests { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                Message = "User not found.";
                return NotFound("User not found.");
                
            }

            // Retrieve the role of the current user (approver)
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            // Retrieve the ID of the current user (approver)
            var userId = currentUser.Id;

            if (userRoles.Contains("approver"))
            {
                // If the current user is the first approver
                if (userId == "57b6deec-f45d-4d70-92e6-61dc57dc60fc")//Modify userId if different user is approver
                {
                    // Retrieve pending requests for the first approver
                    PendingRequests = await _context.Requests
                        .Include(r => r.Employee)
                        .Where(r => r.Status == "Pending")
                        .OrderBy(r => r.RequestedAt)
                        .ToListAsync();
                }
                // If the current user is the second approver
                else if (userId == "291ba253-18da-45ec-b511-d32c17496a70")//Modify userId if different user is approver
                {
                    // Retrieve requests with status "OneApproved" for the second approver
                    PendingRequests = await _context.Requests
                        .Include(r => r.Employee)
                        .Where(r => r.Status == "OneApproved")
                        .OrderBy(r => r.RequestedAt)
                        .ToListAsync();
                }
                else
                {
                    Message = "Invalid user.";
                    return NotFound("Invalid user.");
                }

                ApprovalHistory = await _context.RequestHistory
                    .Include(h => h.Requests)
                    .ThenInclude(r => r.Employee)
                    .Where(h => h.ApproverId == userId)
                    .OrderByDescending(h => h.DecisionAt)
                    .Select(h => new RequestHistoryViewModel
                    {
                        RequesterName = h.Requests.Employee.Name,
                        RequesterDepartment = h.Requests.Employee.Department,
                        RequesterEmail = h.Requests.Employee.Email,
                        RequestType = h.Requests.RequestType,
                        RequestedAt = h.Requests.RequestedAt,
                        Decision = h.Decision,
                        DecisionAt = h.DecisionAt
                    })
                    .ToListAsync();

                return Page();
            }
            Message = "User is not an approver.";

            return NotFound("User is not an approver.");
        }
    }
}
