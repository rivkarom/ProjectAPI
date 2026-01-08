using System.Collections.Generic;

namespace ChineseAuctionProject.Models
{
    public class Gift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoriesEnum Category { get; set; }
        public string Description { get; set; }
        public int WinnersCount { get; set; }
        public int TicketPrice { get; set; }
        public List<User> WinnersList { get; set; } = new(); // userIds
        public string? DonorId { get; set; }
    }
}