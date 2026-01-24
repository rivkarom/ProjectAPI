namespace ChineseAuctionProject.Models
{
    public class Gift
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Foreign key to category
        public int CategoryId { get; set; }
        public Category? Category { get; set; }  // Navigation property

        public string Description { get; set; } = string.Empty;
        public int WinnersCount { get; set; }
        public int TicketPrice { get; set; }

        public string? DonorId { get; set; }
        public Donor? Donor { get; set; }  // Navigation property optional
    }
}