using eCommerce.SharedLibrary.Logs;
using eCommerce.SharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Data;
using System.Linq.Expressions;

namespace ProductApi.Infrastructure.Repositories
{
    internal class ProductRepository(ProductDBContext context) : IProduct
    {
        public async Task<Response> CreateAsync(Product entity)
        {
            try
            {
                // Check if a product already exist
                var getProduct = await GetByAsync(_ => _.Name!.Equals(entity.Name));
                if (getProduct is not null && !string.IsNullOrEmpty(getProduct.Name))
                {
                    return new Response(false, $"{entity.Name} already exists.");
                }

                var currentEntity = context.Products.Add(entity).Entity;
                await context.SaveChangesAsync();

                if(currentEntity is not null && currentEntity.Id > 0)
                {
                    return new Response(true, $"{entity.Name} created successfully.");
                }
                else
                {
                    return new Response(false, "Error occurred while creating the product.");
                }
            }
            catch (Exception ex)
            {
                // Log the original exception
                LogException.LogExceptions(ex);

                // Display scary-free message to the client
                return new Response(false, "Error occurred while creating the product.");
            }
        }

        public async Task<Response> DeleteAsync(int id)
        {
            try
            {
                var product = await FindByIdAsync(id);
                if (product is null)
                {
                    return new Response(false, "Not found.");
                }
                
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return new Response(true, "Deleted successfully.");
            }
            catch (Exception ex)
            {
                // Log the original exception
                LogException.LogExceptions(ex);

                // Display scary-free message to the client
                return new Response(false, "Error occurred while deleting the product.");
            }
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            try
            {
                var product = await context.Products.FindAsync(id);
                return product is not null ? product : null!;
            }
            catch (Exception ex)
            {
                // Log the original exception
                LogException.LogExceptions(ex);

                // Display scary-free message to the client
                throw new Exception("Error occurred while retrieving the product.");
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                var products = context.Products.AsNoTracking().ToListAsync();
                return (IEnumerable<Product>)(products is not null ? products : null!);
            }
            catch (Exception ex)
            {
                // Log the original exception
                LogException.LogExceptions(ex);

                // Display scary-free message to the client
                throw new InvalidOperationException("Error occurred while retrieving the product.");
            }
        }

        public async Task<Product> GetByAsync(Expression<Func<Product, bool>> predicate)
        {
            try
            {
                var product = await context.Products.Where(predicate).FirstOrDefaultAsync()!;
                return product is not null ? product : null!;
            }
            catch(Exception ex)
            {
                // Log the original exception
                LogException.LogExceptions(ex);

                // Display scary-free message to the client
                throw new InvalidOperationException("Error occurred while retrieving the product.");
            }
        }

        public async Task<Response> UpdateAsync(Product entity)
        {
            try
            {
                var product = await FindByIdAsync(entity.Id);
                if (product is null)
                {
                    return new Response(false, "Not found.");
                }

                context.Entry(product).State = EntityState.Detached;
                context.Products.Update(entity);
                await context.SaveChangesAsync();
                return new Response(true, "Updated successfully.");
            }
            catch (Exception ex)
            {
                // Log the original exception
                LogException.LogExceptions(ex);

                // Display scary-free message to the client
                return new Response(false, "Error occurred while updating the product.");
            }
        }
    }
}
