using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Customer :ModelBase
    {

        public int Id { get; set; }
        public ICollection<Review>? Reviews { get; set; } = new HashSet<Review>();
        public ICollection<SavedProvider>? SavedProviders { get; set; } = new HashSet<SavedProvider>();
        public ICollection<Requests>? Requests { get; set; } = new HashSet<Requests>();
        public int ApplicationUserId { get; set; } 
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
