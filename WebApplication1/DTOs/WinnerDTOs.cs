using ChineseAuctionProject.Models;
using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionProject.DTOs
{
    public class WinnerDTOs
    {
        public class WinnerReadDTO
        {
            public int Id { get; set; }
            public int GiftId { get; set; }
            public string GiftName { get; set; } = string.Empty;
            public string UserId { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string UserEmail { get; set; } = string.Empty;
            public DateTime WonAt { get; set; }
        }
    }
}