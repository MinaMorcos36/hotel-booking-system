using HotelBooking.Web.Models;
using HotelBooking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data.Common;

namespace HotelBooking.Web.Pages.RoomCategories
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext context;
        public CreateModel(AppDbContext context)
        {
            this.context = context;
        }
        [BindProperty]
        public RoomCategoryInput Input { get; set; } = new RoomCategoryInput();

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var category = new RoomCategory
            {
                Name = Input.Name,
                Capacity = Input.Capacity,
                Price = Input.Price
            };
            context.RoomCategories.Add(category);
            try
            {
                await context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("Input.Name", "A category with this name already exists.");
                return Page();
            }
            return RedirectToPage("/RoomCategories/Index");
        }
    }
}
