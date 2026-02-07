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

            public int CategoryId { get; set; } = 2;  // ברירת מחדל, לא חובה

            [MaxLength(500)]
            public string Description { get; set; } = string.Empty;

            public int WinnersCount { get; set; } = 0;

            [Required]
            public int TicketPrice { get; set; }

            public string? DonorId { get; set; } = "329084172";  // ברירת מחדל
            [MaxLength(500)]
            public string ImageUrl { get; set; } = string.Empty;
        }

        public class GiftUpdateDTO
        {
            public required string Name { get; set; }

            
            public int CategoryId { get; set; }

            [MaxLength(500)]
            public string Description { get; set; } = string.Empty;

            public int WinnersCount { get; set; }

            [Required]
            public int TicketPrice { get; set; }

            public string? DonorId { get; set; }
            [MaxLength(500)]
            public string ImageUrl { get; set; } = string.Empty;
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
            public string ImageUrl { get; set; } = string.Empty;
            public bool IsRaffled { get; set; }
            public DateTime? RaffleDate { get; set; }
            public List<WinnerDTOs.WinnerReadDTO> Winners { get; set; } = new();
        }
    }
}