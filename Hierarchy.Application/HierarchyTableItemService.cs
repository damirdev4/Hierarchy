using Hierarchy.Domain.Entities;
using Hierarchy.Domain.Exceptions;
using Hierarchy.Domain.Repositories;
using System.Net.Sockets;

namespace Hierarchy.Application
{
    public class HierarchyTableItemService
    {
        private readonly IHierarchyTableItemsRepository _hierarchyTableItemsRepository;

        public HierarchyTableItemService(IHierarchyTableItemsRepository hierarchyTableItemsRepository)
        {
            _hierarchyTableItemsRepository = hierarchyTableItemsRepository;
        }

        public async Task<HierarchyTableItem> GetItem(long id)
        {
            var hierarchyItem = await _hierarchyTableItemsRepository.GetItemByIdAsync(id);
            if (hierarchyItem == null)
            {
                throw new ItemNotFoundException();
            }
            return hierarchyItem;
        }

        public async Task<long> AddItem(HierarchyTableItem hierarchyTableItem)
        {
            try
            {
                var addedItemid = await _hierarchyTableItemsRepository.AddItemAsync(hierarchyTableItem);
                return addedItemid;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IList<HierarchyTableItem>> GetItems()
        {
            try
            {
                var items = await _hierarchyTableItemsRepository.GetItemsAsync();
                return items;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IList<HierarchyTableItem>> GetParentItems(long id)
        {
            try
            {
                var items = await _hierarchyTableItemsRepository.GetItemsByParentIdAsync(id);
                return items;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateItem(HierarchyTableItem hierarchyTableItem)
        {
            try
            {
                await _hierarchyTableItemsRepository.UpdateItemAsync(hierarchyTableItem);
            }
            catch (ItemNotFoundException)
            {
                throw;
            }
        }

        public async Task RemoveItem(long id)
        {
            try
            {
                await _hierarchyTableItemsRepository.RemoveItemAsync(id);
            }
            catch (ItemNotFoundException)
            {
                throw;
            }
        }
    }
}
