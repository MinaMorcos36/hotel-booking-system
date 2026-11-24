using HotelBooking.Web.Models;
using HotelBooking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace HotelBooking.Web.Pages.RoomCategories
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext context;
        public EditModel(AppDbContext context)
        {
            this.context = context;
        }
        [BindProperty]
        public RoomCategoryInput Input { get; set; } = new RoomCategoryInput();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var roomcategory = await context.RoomCategories.FindAsync(id);
            if(roomcategory == null)
            {
                return NotFound();
            }
            Input.Name = roomcategory.Name;
            Input.Capacity = roomcategory.Capacity;
            Input.Price = roomcategory.Price;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            var categoryInDb = await context.RoomCategories.FindAsync(id);
            if(categoryInDb == null)
            {
                return NotFound();
            }
            categoryInDb.Name = Input.Name;
            categoryInDb.Capacity = Input.Capacity;
            categoryInDb.Price = Input.Price;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Input.Name", "A category with this name already exists.");
                return Page();
            }
            return RedirectToPage("/RoomCategories/Index");
        }
    }
}
