using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionProject.DTOs
{
    public class CategoryCreateDTO
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Icon { get; set; } = string.Empty;
    }

    public class CategoryUpdateDTO
    {
        [MaxLength(20)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Icon { get; set; }
    }

    public class CategoryResponsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}