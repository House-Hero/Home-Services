namespace DAL.Models
{
    public class Category : ModelBase
    {
        public int Id {  get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Service>? Services { get; set; } = new HashSet<Service>();
    }
}
