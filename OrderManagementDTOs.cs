csharp WebApplication1\DTOs\OrderManagementDTOs.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionProject.DTOs
{
    public static class OrderManagementDTOs
    {
        public class OrderManagementCreateDto
        {
            [Required]
            [StringLength(9, MinimumLength = 9, ErrorMessage = "UserId must be exactly 9 characters.")]
            public string UserId { get; set; } = null!;

            [Required]
            public int GiftId { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "TicketsCount must be at least 1.")]
            public int TicketsCount { get; set; }
        }

        public class OrderManagementUpdateDto
        {
            [StringLength(9, MinimumLength = 9, ErrorMessage = "UserId must be exactly 9 characters.")]
            public string? UserId { get; set; }

            public int? GiftId { get; set; }

            public int? TicketsCount { get; set; }
            public bool IsPaid { get; set; }
        }

        public class OrderManagementReadDto
        {
            public int Id { get; set; } // חשוב לזיהוי
            public string UserId { get; set; } = null!;
            public int GiftId { get; set; }
            public int TicketsCount { get; set; }
            public bool IsPaid { get; set; }
        }
    }
}