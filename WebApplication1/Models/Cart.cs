namespace ChineseAuctionProject.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; }
        public int GiftId { get; set; }
        public Gift? Gift { get; set; }
        public int TicketsCount { get; set; }
        public bool IsConfirmed { get; set; } = false; // false for draft
    }
}