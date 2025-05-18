using Kickdrop.Api.Models;

public interface IProductService
{
    Task<List<Shoe>> GetAllShoesAsync();
    Task<Shoe> GetShoeByIdAsync(int id);
    Task<Shoe> CreateShoeAsync(Shoe shoe);
    Task<Shoe> UpdateShoeAsync(Shoe shoe);
    Task<bool> DeleteShoeAsync(int id);
    Task<List<Shoe>> GetShoesByColorAsync(ShoeColor color);
}