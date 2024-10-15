namespace DAL.Models
{
    public class Portfolio_image : ModelBase
    {
        public int Id {  get; set; }
        public string Img_Url { get; set; } = null!;
        public int PortfolioId { get; set; }
        public Portfolio_item Portfolio_Item { get; set; } = null!;
    }
}
