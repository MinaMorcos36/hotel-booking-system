using HotelBooking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Web.Pages.Rooms
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext context;
        public DeleteModel(AppDbContext context)
        {
            this.context = context;
        }
        public Room Room { get; set; } = new Room();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var room = await context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            Room.Number = room.Number;
            Room.Floor = room.Floor;
            Room.CategoryId = room.CategoryId;
            Room.IsAvailable = room.IsAvailable;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var room = await context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            context.Rooms.Remove(room);
            try
            {
                await context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                Room = room;
                ModelState.AddModelError(string.Empty, "Unable to delete room. It may be in use.");
                return Page();
            }
            return RedirectToPage("/Rooms/Index");
        }
    }
}