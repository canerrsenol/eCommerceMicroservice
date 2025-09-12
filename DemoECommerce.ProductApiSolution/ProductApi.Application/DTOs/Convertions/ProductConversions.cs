using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs.Convertions
{
    public static class ProductConversions
    {
        public static ProductDTO ToEntity(ProductDTO product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Quantity = product.Quantity,
            Price = product.Price
        };

        public static (ProductDTO?, IEnumerable<ProductDTO>?) FromEntity(Product product, IEnumerable<Product>? products)
        (
            if(product is not null || products is null)
            {



            Id: product.Id,
            Name: product.Name!,
            Quantity: product.Quantity,
            Price: product.Price
        );
    }
}
