namespace ChineseAuctionProject.Models
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<int> OrdersList { get; set; } = new(); // orderIds
        public bool IsAdmin { get; set; }
    }
}