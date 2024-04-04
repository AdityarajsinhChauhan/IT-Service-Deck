using System.Collections.Generic;
using System.Linq;
using IT_Service_Deck.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using IT_Service_Deck.Services;

namespace IT_Service_Deck.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger, UserManager<AppUser> userManager, AppDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public List<Requests> UserRequests { get; set; }

        public async Task OnGetAsync()
        {
            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Fetch requests associated with the logged-in user
                UserRequests = await _context.Requests
                    .Where(r => r.E_id == user.Id)
                    .ToListAsync();
            }
        }
    }
}
