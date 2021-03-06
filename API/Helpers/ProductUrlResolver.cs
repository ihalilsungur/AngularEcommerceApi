using API.Core.DbModels;
using API.Dtos;
using AutoMapper;
using Microsoft.Extensions.Configuration;


namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnResponse, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductToReturnResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
