using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Service : ModelBase
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; }=null!;
        public ICollection<Provider>? Providers { get; set; } = new HashSet<Provider>();
        public ICollection<Requests>? Requests { get; set; } = new HashSet<Requests>();

    }
}
