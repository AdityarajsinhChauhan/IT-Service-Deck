using IT_Service_Deck.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IT_Service_Deck.Pages
{
    [Authorize]

    public class ProfileModel : PageModel
    {
        private readonly UserManager<AppUser> usreManager;

        public AppUser? appUser;
        public ProfileModel(UserManager<AppUser> userManager)
        {
            usreManager = userManager;


        }
        public async void OnGet()
        {
            var task = usreManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;
        }
    }
}
