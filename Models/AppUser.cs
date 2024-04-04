using Microsoft.AspNetCore.Identity;
namespace IT_Service_Deck.Models
{
    public class AppUser : IdentityUser
    {
        public string EmployeeId { get; set; } = "";

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public string Department { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }
}
