using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Web.ViewModels
{
    public class RoomInput
    {
        [Required(ErrorMessage = "Room number is required.")]
        [StringLength(10, ErrorMessage = "Room number cannot exceed 10 characters.")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Room number must be a positive number with digits only.")]
        public string Number { get; set; } = string.Empty;
        [Range(1, 5, ErrorMessage = "Floor must be between 1 and 5.")]
        public int Floor { get; set; }
        [Required(ErrorMessage = "Please select a category.")]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
