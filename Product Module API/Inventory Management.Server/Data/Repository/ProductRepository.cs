using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management.Server.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;

        public ProductRepository(InventoryDbContext context)
        {
            _context = context;
        }
        public async Task<Product> AddProductAsync(Product newItem)
        {
            _context.Add(newItem);
            await _context.SaveChangesAsync();
            return newItem;
        }

        public  async Task<bool> DeleteProductByIDAsync(int id)
        {
            var itemToDelete = _context.Products.Find(id);
            _context.Remove(itemToDelete);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Product> GetProductByIDAsync(int Id)
        {
            IQueryable<Product> result = _context.Products.Where(i => i.Id == Id);
            return await result.FirstOrDefaultAsync();

        }

        public async Task<Product[]> GetProductsAync()
        {
            IQueryable<Product> result = _context.Products;

            result = result.OrderByDescending(i => i.Price);
            return await result.ToArrayAsync();
        }

        public  async Task<Product> UpdateProductAsync(Product changedItem)
        {
            var modifiedItem = _context.Products.Find(changedItem.Id);
            modifiedItem.Name = changedItem.Name;
            modifiedItem.Price = changedItem.Price;
            modifiedItem.Description = changedItem.Description;
            modifiedItem.DiscountedPrice = changedItem.DiscountedPrice;
            modifiedItem.Quantity = changedItem.Quantity;
            modifiedItem.Category = changedItem.Category;
            await _context.SaveChangesAsync();
            return changedItem;

        }
    }
}
