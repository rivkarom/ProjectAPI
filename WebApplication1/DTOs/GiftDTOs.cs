using ChineseAuctionProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionProject.DTOs
{
    public class GiftDTOs
    {
        public class GiftCreateDTO
        {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

            [Required]
            public CategoriesEnum Category { get; set; } = CategoriesEnum.Unknown;

            [MaxLength(500)]
            public string Description { get; set; }
            public int WinnersCount { get; set; } = 0;

            [Required]
            public int TicketPrice { get; set; }

            // optional: supply donor Id when creating a gift
            public string? DonorId { get; set; }
        }

        public class GiftUpdateDTO
        {
            public string Name { get; set; }
            [Required]
            public CategoriesEnum Category { get; set; } = CategoriesEnum.Unknown;

            public string Description { get; set; }
            public int WinnersCount { get; set; }

            [Required]
            public int TicketPrice { get; set; }

            // allow changing donor association
            public string? DonorId { get; set; }
        }

        public class GiftReadDTO
        {
            public int Id { get; set; }
            public required string Name { get; set; }
            public CategoriesEnum Category { get; set; }
            public required string Description { get; set; }
            public int WinnersCount { get; set; }
            public int TicketPrice { get; set; }
            public required List<User> WinnersList { get; set; } = new();

            // include donor id in read DTO
            public string? DonorId { get; set; }
        }
    }
}