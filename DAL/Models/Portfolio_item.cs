using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Portfolio_item : ModelBase
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Bio { get; set; } 
        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = null!;

        public ICollection<Portfolio_image>? Portfolio_Image { get; set; } = new HashSet<Portfolio_image>();
    }
}
