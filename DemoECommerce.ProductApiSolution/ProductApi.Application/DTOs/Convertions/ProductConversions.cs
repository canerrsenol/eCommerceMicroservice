using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs.Convertions
{
    public static class ProductConversions
    {
        public static Product ToEntity(ProductDTO product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Quantity = product.Quantity,
            Price = product.Price
        };

        public static (ProductDTO?, IEnumerable<ProductDTO>?) FromEntity(Product product, IEnumerable<Product>? products)
        {
            // If a single product is provided (not null), return it
            if (product is not null)
            {
                var singleProduct = new ProductDTO
                (
                    product.Id,
                    product.Name!,
                    product.Quantity,
                    product.Price
                );

                return (singleProduct, null);
            }

            // If a list of products is provided (not null), return the list
            if (products is not null || product is not null)
            {
                var _products = products.Select(pDTO =>
                    new ProductDTO
                    (
                        pDTO!.Id,
                        pDTO.Name!,
                        pDTO.Quantity,
                        pDTO.Price
                    )
                ).ToList();

                return (null, _products);
            }

            // If both are null, return nulls
            return (null, null);
        }
    }
}
