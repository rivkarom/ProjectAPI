using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionProject.DTOs
{
    public class DonorDTOs
    {
        public class DonorCreateDto
        {
            [Required]
            [MaxLength(9, ErrorMessage = "Remove digits, ID number must be exactly 9 characters long")]
            [MinLength(9, ErrorMessage = "Add digits, ID number must be exactly 9 characters long")]
            public string Id { get; set; }

            [Required]
            [MaxLength(50)]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            [MaxLength(100)]
            public string Email { get; set; }

            [Required]
            [Phone]
            [MaxLength(10, ErrorMessage = "Remove digits, phone number must be no more than 10 characters long")]
            [MinLength(7, ErrorMessage = "Add digits, phone number must be at least 7 characters long")]
            public string Phone { get; set; }

            [Required]
            public List<int> DonatiosList { get; set; } = new();

        }

        public class DonorUpdateDto
        {
            [MaxLength(50)]
            public string? Name { get; set; }

            [EmailAddress]
            [MaxLength(100)]
            public string? Email { get; set; }

            [Phone]
            [MaxLength(10, ErrorMessage = "Remove digits, phone number must be no more than 10 characters long")]
            [MinLength(7, ErrorMessage = "Add digits, phone number must be at least 7 characters long")]
            public string? Phone { get; set; }

            public List<int> DonatiosList { get; set; };
        }

        public class DonorResponseDto
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public List<int> DonatiosList { get; set; }
        }

    }
}
