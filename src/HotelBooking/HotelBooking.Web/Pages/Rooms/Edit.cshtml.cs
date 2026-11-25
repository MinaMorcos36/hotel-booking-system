using HotelBooking.Web.Models;
using HotelBooking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Web.Pages.Rooms
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext context;
        public EditModel(AppDbContext context)
        {
            this.context = context;
        }
        [BindProperty]
        public RoomInput Input { get; set; } = new RoomInput();
        public SelectList RoomCategories { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var room = await context.Rooms.FindAsync(id);
            if(room == null)
            {
                return NotFound();
            }
            Input.Number = room.Number;
            Input.Floor = room.Floor;
            Input.CategoryId = room.CategoryId;
            Input.IsAvailable = room.IsAvailable;
            await LoadCategories();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategories();
                return Page();
            }
            var roomInDb = await context.Rooms.FindAsync(id);
            if (roomInDb == null)
            {
                return NotFound();
            }
            roomInDb.Number = Input.Number;
            roomInDb.Floor = Input.Floor;
            roomInDb.CategoryId = Input.CategoryId!.Value;
            roomInDb.IsAvailable = Input.IsAvailable;   
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Input.Number", "A room with this number already exists.");
                await LoadCategories();
                return Page();
            }
            return RedirectToPage("Index");
        }
        public async Task LoadCategories()
        {
            var categories = await context.RoomCategories
                .AsNoTracking()
                .ToListAsync();
            RoomCategories = new SelectList(categories, "Id", "Name", Input.CategoryId);
        }
    }
}
