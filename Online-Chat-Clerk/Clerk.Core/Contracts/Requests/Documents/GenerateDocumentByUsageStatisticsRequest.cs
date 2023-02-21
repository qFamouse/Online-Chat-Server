namespace Contracts.Requests.Documents
{
    public class GenerateDocumentByUsageStatisticsRequest
    {
        public int TotalMessages { get; set; }
        public int TotalSent { get; set; }
        public int TotalReceived { get; set; }
    }
}
