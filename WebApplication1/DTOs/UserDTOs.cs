using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionProject.DTOs
{
    public class UserDTOs
    {
        public class UserCreateDTO
        {
            [Required]
            [MaxLength(9, ErrorMessage = "Remove digits, ID number must be exactly 9 characters long")]
            [MinLength(9, ErrorMessage = "Add digits, ID number must be exactly 9 characters long")]
            public string Id { get; set; }
            [Required]
            [MaxLength(100)]
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            [MaxLength(100)]
            public string Email { get; set; }
            [Required]
            [MaxLength(10, ErrorMessage = "Remove digits, phone number must be no more than 10 characters long")]
            [MinLength(7, ErrorMessage = "Add digits, phone number must be at least 7 characters long")]
            [Phone]
            public string Phone { get; set; }
            [Required]
            [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
            public string Password { get; set; } = string.Empty;

            public bool IsAdmin { get; set; } = false;
        }
        
            public class UserUpdateDTO
        {
            [Required]
            [MaxLength(9, ErrorMessage = "Remove digits, ID number must be exactly 9 characters long")]
            [MinLength(9, ErrorMessage = "Add digits, ID number must be exactly 9 characters long")]
            public string Id { get; set; }
            [Required]
            [MaxLength(100)]
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            [MaxLength(100)]
            public string Email { get; set; }
            [Required]
            [MaxLength(10, ErrorMessage = "Remove digits, phone number must be no more than 10 characters long")]
            [MinLength(7, ErrorMessage = "Add digits, phone number must be at least 7 characters long")]
            [Phone]
            public string Phone { get; set; }
            public bool IsAdmin { get; set; } = false;
            public List<int> OrdersList { get; set; } = new();//orderIds

        }

        public class UserResponseDto
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public List<int> OrdersList { get; set; } = new();//orderIds
        }
    }

}
