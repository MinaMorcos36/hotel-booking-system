using HotelBooking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Web.Pages.RoomCategories
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext context;
        public DeleteModel(AppDbContext context)
        {
            this.context = context;
        }
        public RoomCategory RoomCategory { get; set; } = new RoomCategory();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var roomcategory = await context.RoomCategories.FindAsync(id);
            if (roomcategory == null)
            {
                return NotFound();
            }
            RoomCategory.Name = roomcategory.Name;
            RoomCategory.Capacity = roomcategory.Capacity;
            RoomCategory.Price = roomcategory.Price;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var roomcategory = await context.RoomCategories.FindAsync(id);
            if (roomcategory == null)
            {
                return NotFound();
            }
            context.RoomCategories.Remove(roomcategory);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                RoomCategory = roomcategory;
                ModelState.AddModelError(string.Empty, "Unable to delete category. It may be in use.");
                return Page();
            }
            return RedirectToPage("/RoomCategories/Index");
        }
    }
}
