using ChineseAuctionProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ChineseAuctionProject.DTOs
{
    public class GiftDTOs
    {
        public class GiftCreateDTO
        {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

<<<<<<< HEAD
            [Required]
            public CategoriesEnum Category { get; set; } = CategoriesEnum.Unknown;
=======
            [Range(0, 4, ErrorMessage = "Category must be between 0 and 4.")]
            public int Category { get; set; } = 0;
>>>>>>> f5d10a757f302c62a92fe3d4656bbd2e93a6cd4a

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
<<<<<<< HEAD
            [Required]
            public CategoriesEnum Category { get; set; } = CategoriesEnum.Unknown;
=======

            [Range(0, 4, ErrorMessage = "Category must be between 0 and 4.")]
            public int Category { get; set; } = 0;
>>>>>>> f5d10a757f302c62a92fe3d4656bbd2e93a6cd4a

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
<<<<<<< HEAD
            public required List<User> WinnersList { get; set; } = new();
=======
            public required List<String> WinnersList { get; set; } = new();
>>>>>>> f5d10a757f302c62a92fe3d4656bbd2e93a6cd4a

            // include donor id in read DTO
            public string? DonorId { get; set; }
        }
    }
}