using DAL.Models;

namespace HouseHero.Models.ViewModels
{
    public class RequestServiceViewModel
    {
        public int ProviderId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime RequestDate { get; set; }
        public Preferred_Communication PreferredCommunication { get; set; }
        public string? Comment { get; set; } = null!;
        public Status Status { get; set; }
    }
}