namespace ChineseAuctionProject.Models
{
    public class Winner
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
        public Gift? Gift { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; }
        public DateTime WonAt { get; set; }
    }
}