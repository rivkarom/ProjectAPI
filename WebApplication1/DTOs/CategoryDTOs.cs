using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionProject.DTOs
{
    public class CategoryCreateDTO
    {
        [Required]
        [MaxLength(20)]
        public String Name { get; set; }=string.Empty;
        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
    }
    public class CategoryUpdateDTO
    {
        [Required]
        [MaxLength(20)]
        public String? Name { get; set; }

        [Required]
        [MaxLength(100)]
        public String? Description { get; set; }
    }
    public class CategoryResponsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

    }


}
