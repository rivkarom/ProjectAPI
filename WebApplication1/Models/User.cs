namespace ChineseAuctionProject.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string Phone { get; set; }
        public List<int> OrdersList { get; set; } = new();//orderIds
        public bool IsAdmin { get; set; }
    }
}
