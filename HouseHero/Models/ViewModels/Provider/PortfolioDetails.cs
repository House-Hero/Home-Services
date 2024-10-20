namespace HouseHero.Models.ViewModels.Provider
{
    public class PortfolioDetails
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}