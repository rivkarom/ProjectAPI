namespace ChineseAuctionProject.Interfaces
{
    public interface IEmailService
    {
        Task SendWinnerNotificationAsync(string email, string userName, string giftName);
    }
}