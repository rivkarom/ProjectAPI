using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionProject.DTOs
{
    public class OrderManagementDTOs
    {
        public class OrderManagmentCreateDto
        {
            [Required]
            [MaxLength(9, ErrorMessage = "Remove digits, ID number must be exactly 9 characters long")]
            [MinLength(9, ErrorMessage = "Add digits, ID number must be exactly 9 characters long")]
            public required string UserId { get; set; }

            [Required]
            public int GiftId { get; set; }

            [Required]
            public int TicketsCount { get; set; }
        }

        public class OrderManagmentUpdateDto
        {
            [MaxLength(9, ErrorMessage = "Remove digits, ID number must be exactly 9 characters long")]
            [MinLength(9, ErrorMessage = "Add digits, ID number must be exactly 9 characters long")]
            public string? UserId { get; set; }

            public int? GiftId { get; set; }
            public int? TicketsCount { get; set; }
            public bool IsPaid { get; set; }
        }

        public class OrderManagmentReadDto
        {
            public int GiftId { get; set; }
            public int TicketsCount { get; set; }
            public bool IsPaid { get; set; }
            public string GiftName { get; set; } = string.Empty;
            public decimal TicketPrice { get; set; }
            public string UserName { get; set; } = string.Empty;
        }
    }
}