using System.ComponentModel.DataAnnotations;

namespace room_service.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [Required]

        public int? RoomNumber { get; set; }
        [Required]
        public string? RoomType { get; set; }
        [Required]

        public int ?Occupancy { get; set; }
        [Required]
        public double ?RoomPrice { get; set; }
        [Required]

        public bool ?IsAvailable { get; set; }
    }
}
