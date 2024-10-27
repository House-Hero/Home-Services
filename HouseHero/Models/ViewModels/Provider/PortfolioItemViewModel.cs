using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace HouseHero.Models.ViewModels.Provider
{
    public class PortfolioItemViewModel
    {
        public int ProviderID {  get; set; }
        public int PortfolioId { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? image {  get; set; }
        //[Display(Name ="Images")]
        //public List<Portfolio_image>? Images_URL { get; set; }
        public List<IFormFile>? Images { get; set; }


        public static implicit operator Portfolio_image(PortfolioItemViewModel model)
        {
            return new Portfolio_image
            {
                PortfolioId = model.PortfolioId,
                Img_Url = model.image

            };
        }
        public static implicit operator Portfolio_item(PortfolioItemViewModel model)
        {
            return new Portfolio_item
            {
                ProviderId = model.ProviderID,
                Bio = model.Bio,
                Name = model.Name,
            };
        }
        public static implicit operator PortfolioItemViewModel(Portfolio_item _Item)
        {
            return new PortfolioItemViewModel
            {
                ProviderID = _Item.ProviderId,
                Bio = _Item.Bio,
                Name = _Item.Name,
            };
        }
    }
}
