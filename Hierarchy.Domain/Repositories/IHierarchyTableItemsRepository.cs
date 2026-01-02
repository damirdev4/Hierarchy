using Hierarchy.Domain.Entities;

namespace Hierarchy.Domain.Repositories
{
    public interface IHierarchyTableItemsRepository
    {
        Task<HierarchyTableItem> GetItemByIdAsync(long id);
        Task<long> AddItemAsync(HierarchyTableItem hierarchyTableItem);
        Task<IList<HierarchyTableItem>> GetItemsByParentIdAsync(long id);
        Task<IList<HierarchyTableItem>> GetItemsAsync();
        Task RemoveItemAsync(long orderId);
        Task UpdateItemAsync(HierarchyTableItem hierarchyTableItem);
    }
}
