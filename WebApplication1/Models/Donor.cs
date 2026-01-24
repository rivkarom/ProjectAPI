namespace ChineseAuctionProject.Models
{
    public class Donor
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<Gift> DonationsList { get; set; } = new();
    }
}