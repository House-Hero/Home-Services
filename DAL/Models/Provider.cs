using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    //Base Class
    public class Provider :ModelBase
    {
        public int Id { get; set; }

        public string? Bio { get; set; } = null!;
        public int Rating { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }

        public int? ServiceId { get; set; }
        public Service? Service { get; set; } = null!;

        public ICollection<Portfolio_item>? Portfolio_Item { get; set; } = new HashSet<Portfolio_item>();
        public ICollection<Available_Day>? Available_Day { get; set; } = new HashSet<Available_Day>();
        public ICollection<Review>? Reviews { get; set; } = new HashSet<Review>();
        public ICollection<SavedProvider>? Saved { get; set; } = new HashSet<SavedProvider>();
        public ICollection<Requests>? Requests { get; set; } = new HashSet<Requests>();
        public int ApplicationUserId { get; set; } 
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
