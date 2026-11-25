using HotelBooking.Web.Models;
using HotelBooking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Web.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext context;
        public CreateModel(AppDbContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public RoomInput Input { get; set; } = new RoomInput();
        public SelectList CategoryList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            await LoadCategoriesAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync();
                return Page();
            }
            var room = new Room
            {
                Number = Input.Number,
                Floor = Input.Floor,
                CategoryId = Input.CategoryId!.Value,
                IsAvailable = Input.IsAvailable
            };
            context.Rooms.Add(room);
            try
            {
                await context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Could not save the room. maybe a room with this number already exists.");
                await LoadCategoriesAsync();
                return Page();
            }   
            return RedirectToPage("Index");
        }
        private async Task LoadCategoriesAsync()
        {
            var categories = await context.RoomCategories
                .AsNoTracking()
                .ToListAsync();
            CategoryList = new SelectList(categories, "Id", "Name", Input.CategoryId);
        }
    }
}