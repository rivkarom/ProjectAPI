namespace ChineseAuctionProject.Models
{
    public class Donor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<int> DonatiosList { get; set; }//giftIds
    }
}
