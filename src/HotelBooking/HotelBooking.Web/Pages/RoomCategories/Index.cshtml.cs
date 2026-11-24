using HotelBooking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Web.Pages.RoomCategories
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext context;
        public IndexModel(AppDbContext context)
        {
            this.context = context;
        }
        public List<RoomCategory> RoomCategories { get; set; } = new List<RoomCategory>();
        public async Task OnGetAsync()
        {
            RoomCategories = await context.RoomCategories.ToListAsync();
        }
    }
}
