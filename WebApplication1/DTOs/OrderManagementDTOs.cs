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
            public string UserId { get; set; }

            [Required]
            [MaxLength(10)]
            public int GiftId { get; set; }

            [Required]
            public int TicketsCount { get; set; }
        }

        public class OrderManagmentUpdateDto
        {
            [MaxLength(9, ErrorMessage = "Remove digits, ID number must be exactly 9 characters long")]
            [MinLength(9, ErrorMessage = "Add digits, ID number must be exactly 9 characters long")]
            public string UserId { get; set; }

            [MaxLength(10)]
            public int GiftId { get; set; }

            public int TicketsCount { get; set; }

            public bool IsPaid { get; set; }
        }

        public class OrderManagmentReadDto
        {

            public int GiftId { get; set; }
            public int TicketsCount { get; set; }
            public bool IsPaid { get; set; }
        }


    }
}
