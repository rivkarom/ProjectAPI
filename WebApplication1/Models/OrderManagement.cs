namespace ChineseAuctionProject.Models
{
    public class OrderManagement
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int GiftId { get; set; }
        public int TicketsCount { get; set; }
        public bool IsPaid { get; set; }
    }
}