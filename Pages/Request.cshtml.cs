using IT_Service_Deck.Models;
using IT_Service_Deck.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IT_Service_Deck.Pages
{
    [Authorize]
    public class RequestModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        private readonly UserManager<AppUser> _userManager;

        private readonly AppDbContext _dbContext;
        [BindProperty]
        public string RequestType { get; set; }
        [BindProperty]
        public string? RequestDuration { get; set; }

        public AppUser? appUser;

        public RequestModel(UserManager<AppUser> userManager,AppDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;


        }
        public void OnGet()
        {

            var task = _userManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = "Error occurred while submitting request.";
                return Page();
            }
            try
            {
                // Retrieve the current user
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    Message = "User not found.";
                    return Page();
                }

                // Create a new Requests object and populate it with form data and user details
                var request = new Requests
                {
                    E_id = user.Id,
                    RequestType = RequestType,
                    RequestDuration = RequestDuration,
                    Status = "Pending",
                    RequestedAt = DateTime.Now
                };

                // Save the request to the database
                _dbContext.Requests.Add(request);
                await _dbContext.SaveChangesAsync();

                var requestId = request.Id;

                var requestRouting = new RequestRouting
                {
                    RequestId = requestId,
                    FirstApprover = "57b6deec-f45d-4d70-92e6-61dc57dc60fc",///Need to modify this user Id if different User is Approver
                    SecondApprover = "291ba253-18da-45ec-b511-d32c17496a70"///Need to modify this user Id if different User is Approver
                };

                _dbContext.RequestRouting.Add(requestRouting);
                await _dbContext.SaveChangesAsync();

                Message = "Request submitted successfully.";
            }
            catch (Exception ex)
            {
                // Log the error
                Message = "Error occurred while submitting request: " + ex.Message;
            }

            return RedirectToPage();
        }
     }
}
