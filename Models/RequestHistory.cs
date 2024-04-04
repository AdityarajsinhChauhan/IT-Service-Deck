using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Service_Deck.Models
{
    public class RequestHistory
    {
        public int Id { get; set; }
        public int RequestId {  get; set; }

        public string RequesterId {  get; set; }

        public string ApproverId {  get; set; }

        public string Decision {  get; set; }

        public DateTime DecisionAt { get; set; }

        public Requests Requests { get; set; }


    }
}
