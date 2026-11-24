using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Web.ViewModels
{
    public class RoomCategoryInput
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 1 and 50 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1, 20, ErrorMessage = "Capacity must be between 1 and 20")]
        public int Capacity { get; set; }
        [Required]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000")]
        public decimal Price { get; set; }
    }
}
