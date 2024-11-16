using Database;
using Database.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;

namespace Service.Services
{
    public class DbService : IDbService
    {
        private readonly TodoContext _db;

        public DbService(TodoContext db)
        {
            _db = db;
        }

        #region Log
        public async Task ErrorLog(string message)
        {
            var entity = new Logs() { Type = "Error", TimestampUtc = DateTime.UtcNow, Message = message };
            await _db.Log.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task InfoLog(string message)
        {
            var entity = new Logs() { Type = "Info", TimestampUtc = DateTime.UtcNow, Message = message };
            await _db.Log.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
        #endregion

        #region Todo list
        public async Task<bool> ClearTodoList()
        {
            try
            {
                //_db.Item.RemoveRange(_db.Item);
                await _db.Database.ExecuteSqlRawAsync("truncate table [Item]");
                return true;
            }
            catch (Exception ex)
            {
                await ErrorLog(ex.Message);
                return false;
            }
        }
        #endregion

        #region Item
        public async Task<IList<Item>> GetAllItems()
        {
            try
            {
                var items = await _db.Item.ToListAsync();
                return items.OrderBy(x => x.DueDateUtc).ToList();
            }
            catch (Exception ex)
            {
                await ErrorLog(ex.Message);
                return new List<Item>();
            }
        }

        public async Task<Item> GetItemById(int id)
        {
            try
            {
                var item = _db.Item.Where(x => x.Id == id).First();
                return item;
            }
            catch (Exception ex)
            {
                await ErrorLog(ex.Message);
                return null;
            }
        }

        public async Task<bool> InsertItem(Item item)
        {
            try
            {
                await _db.Item.AddAsync(item);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await ErrorLog(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateItem(Item item)
        {
            try
            {
                var entity = _db.Item.Where(x => x.Id == item.Id).First();
                entity.IsDone = item.IsDone;
                _db.Item.Update(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await ErrorLog(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteItem(int id)
        {
            try
            {
                try
                {
                    var entity = _db.Item.Where(x => x.Id == id).First();
                    _db.Item.Remove(entity);
                    await _db.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    await ErrorLog($"Can't delete Entity with Id: {id} - it's not exist.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await ErrorLog(ex.Message);
                return false;
            }
        }
        #endregion
    }
}