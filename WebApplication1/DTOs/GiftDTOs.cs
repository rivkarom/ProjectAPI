using ChineseAuctionProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionProject.DTOs
{
    public class GiftDTOs
    {
        public class GiftCreateDTO
        {
            [Required]
            [MaxLength(100)]
            public required string Name { get; set; }

            [Required]
            public int CategoryId { get; set; }

            [MaxLength(500)]
            public string Description { get; set; } = string.Empty;

            public int WinnersCount { get; set; } = 0;

            [Required]
            public int TicketPrice { get; set; }

            public string? DonorId { get; set; }
        }

        public class GiftUpdateDTO
        {
            public required string Name { get; set; }

            [Required]
            public int CategoryId { get; set; }

            [MaxLength(500)]
            public string Description { get; set; } = string.Empty;

            public int WinnersCount { get; set; }

            [Required]
            public int TicketPrice { get; set; }

            public string? DonorId { get; set; }
        }

        public class GiftReadDTO
        {
            public int Id { get; set; }
            public required string Name { get; set; }
            public int CategoryId { get; set; }
            public string? CategoryName { get; set; }
            public required string Description { get; set; }
            public int WinnersCount { get; set; }
            public int TicketPrice { get; set; }
            public string? DonorId { get; set; }
        }
    }
}