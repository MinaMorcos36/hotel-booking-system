using HotelBooking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Web.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext context;
        public IndexModel(AppDbContext context)
        {
            this.context = context;
        }
        public List<Room> Rooms { get; set; } = new List<Room>();
        public async Task OnGetAsync()
        {
            Rooms = await context.Rooms
                .Include(r => r.Category)
                .ToListAsync();
        }
    }
}
