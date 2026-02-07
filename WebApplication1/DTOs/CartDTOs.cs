using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionProject.DTOs
{
    public class CartDTOs
    {
        public class CartCreateDTO
        {
            [Required]
            public string UserId { get; set; } = string.Empty;
            [Required]
            public int GiftId { get; set; }
            [Required]
            [Range(1, int.MaxValue)]
            public int TicketsCount { get; set; }
        }

        public class CartUpdateDTO
        {
            [Range(1, int.MaxValue)]
            public int? TicketsCount { get; set; }
        }

        public class CartReadDTO
        {
            public int Id { get; set; }
            public int GiftId { get; set; }
            public string GiftName { get; set; } = string.Empty;
            public int TicketsCount { get; set; }
            public bool IsConfirmed { get; set; }
        }
    }
}