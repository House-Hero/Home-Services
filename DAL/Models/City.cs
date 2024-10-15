namespace DAL.Models
{
    public class City : ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<ApplicationUser>? ApplicationUsers { get; set; }=new HashSet<ApplicationUser>();


    }
}
