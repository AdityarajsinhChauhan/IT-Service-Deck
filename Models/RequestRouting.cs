namespace IT_Service_Deck.Models
{
    public class RequestRouting
    {
        public int Id {  get; set; }

        public int RequestId {  get; set; }

        public string FirstApprover { get; set; }

        public string SecondApprover {  get; set; }
    }
}
