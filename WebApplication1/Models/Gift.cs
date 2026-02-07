namespace ChineseAuctionProject.Models
{
    public class Gift
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; } = 2;
        public Category? Category { get; set; }  // Navigation property

        public string Description { get; set; } = string.Empty;
        public int WinnersCount { get; set; }
        public int TicketPrice { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string? DonorId { get; set; } = "329084172";
        public Donor? Donor { get; set; }  // Navigation property optional

        public bool IsRaffled { get; set; } = false;
        public DateTime? RaffleDate { get; set; }

        public ICollection<Winner> Winners { get; set; } = new List<Winner>();
    }
}