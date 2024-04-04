namespace IT_Service_Deck.Models
{
    public class Requests
    {
        public int Id {  get; set; }

        public string E_id { get; set; }

        public string RequestType {  get; set; }

        public string? RequestDuration { get; set; }

        public string Status {  get; set; }

        public DateTime RequestedAt { get; set; }

        public AppUser Employee {  get; set; }



    }
}
