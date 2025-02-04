using RestWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWeb.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItemsAsync(int? categoryId, int page, int pageSize);
        Task<Item> AddItemAsync(Item item);
        Task<Item> UpdateItemAsync(int id, Item item);
        Task<bool> DeleteItemAsync(int id);
    }
}
