using Database.Domain;

namespace Service.Services
{
    public interface IDbService
    {
        Task<bool> ClearTodoList();
        Task<bool> DeleteItem(int id);
        Task ErrorLog(string message);
        Task<IList<Item>> GetAllItems();
        Task<Item> GetItemById(int id);
        Task InfoLog(string message);
        Task<bool> InsertItem(Item item);
        Task<bool> UpdateItem(Item item);
    }
}