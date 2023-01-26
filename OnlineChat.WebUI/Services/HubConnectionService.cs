namespace OnlineChat.WebUI.Services
{
    public class HubConnectionService
    {
        public Dictionary<int, List<string>> ConnectedUsers { get; init; }

        public HubConnectionService()
        {
            ConnectedUsers = new Dictionary<int, List<string>>();
        }
    }
}
