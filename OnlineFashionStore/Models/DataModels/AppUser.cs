using Microsoft.AspNetCore.Identity;

namespace OnlineFashionStore.Models.DataModels
{
    public class AppUser:IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Order> Orders { get; set; }
        public List<Review> Review { get; set; }
    }
}
