using Kickdrop.Api.Data;
using Kickdrop.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Kickdrop.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly KickdropContext _context;

        public ProductService(KickdropContext context)
        {
            _context = context;
        }

        public async Task<List<Shoe>> GetAllShoesAsync()
        {
            return await _context.Shoes.ToListAsync();
        }

        public async Task<Shoe> GetShoeByIdAsync(int id)
        {
            return await _context.Shoes.FindAsync(id);
        }

        public async Task<Shoe> CreateShoeAsync(Shoe shoe)
        {
            _context.Shoes.Add(shoe);
            await _context.SaveChangesAsync();
            return shoe;
        }

        public async Task<Shoe> UpdateShoeAsync(Shoe shoe)
        {
            _context.Entry(shoe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return shoe;
        }

        public async Task<bool> DeleteShoeAsync(int id)
        {
            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe == null)
            {
                return false;
            }

            _context.Shoes.Remove(shoe);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Shoe>> GetShoesByColorAsync(ShoeColor color)
        {
            return await _context.Shoes
                .Where(s => s.Color == color)
                .ToListAsync();
        }

        internal async Task GetShoesByColorAsync(string v)
        {
            throw new NotImplementedException();
        }
    }
}