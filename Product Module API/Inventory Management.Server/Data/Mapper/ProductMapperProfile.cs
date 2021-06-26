using AutoMapper;

namespace Inventory_Management.Server.Data
{
    public class ProductMapperProfile :Profile
    {
        public ProductMapperProfile() 
        {
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}
