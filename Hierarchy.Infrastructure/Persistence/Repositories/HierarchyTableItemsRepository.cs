using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hierarchy.Domain;
using Hierarchy.Domain.Entities;
using Hierarchy.Domain.Repositories;
using Hierarchy.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Hierarchy.Infrastructure.Persistence.Repositories
{
    public sealed class HierarchyTableItemsRepository : IHierarchyTableItemsRepository
    {
        private readonly HierarchyDbContext db = null!;

        public HierarchyTableItemsRepository(HierarchyDbContext context)
        {
            db = context;
        }

        public async Task<HierarchyTableItem> GetItemByIdAsync(long id)
        {
            var hierarchyTableItemEntity = await db.HierarchyTableItemsEntity.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

            var hierarchyTableItem = new HierarchyTableItem(id)
            {
                Name = hierarchyTableItemEntity!.Name,
                ParentId = hierarchyTableItemEntity!.ParentId
            };

            return hierarchyTableItem;
        }

        public async Task<long> AddItemAsync(HierarchyTableItem hierarchyTableItem)
        {
            var hierarchyTableItemEntity = new HierarchyTableItemEntity { Name = hierarchyTableItem.Name, ParentId = hierarchyTableItem.ParentId };
            await db.HierarchyTableItemsEntity.AddAsync(hierarchyTableItemEntity);
            await db.SaveChangesAsync();
            return hierarchyTableItemEntity.Id;
        }

        public async Task<IList<HierarchyTableItem>> GetItemsByParentIdAsync(long id)
        {
            List<HierarchyTableItem> hierarchyTableItems = [];
            var hierarchyTableItemsEntity = await db.HierarchyTableItemsEntity.AsNoTracking().Where(i => i.ParentId == id).ToListAsync();

            if (hierarchyTableItemsEntity != null)
            {
                foreach (var hierarchyTableItemEntity in hierarchyTableItemsEntity)
                {
                    var hierarchyTableItem = new HierarchyTableItem(hierarchyTableItemEntity.Id)
                    {
                        Name = hierarchyTableItemEntity.Name,
                        ParentId = hierarchyTableItemEntity.ParentId
                    };
                    hierarchyTableItems.Add(hierarchyTableItem);
                }
            }

            return hierarchyTableItems;
        }

        public async Task<IList<HierarchyTableItem>> GetItemsAsync()
        {

            List<HierarchyTableItem> hierarchyTableItems = [];
            var hierarchyTableItemsEntity = await db.HierarchyTableItemsEntity.Where(i => i.ParentId == null).AsNoTracking().ToListAsync();

            if (hierarchyTableItemsEntity != null)
            {
                foreach (var hierarchyTableItemEntity in hierarchyTableItemsEntity)
                {
                    var hierarchyTableItem = new HierarchyTableItem(hierarchyTableItemEntity.Id)
                    {
                        Name = hierarchyTableItemEntity.Name,
                        ParentId = hierarchyTableItemEntity.ParentId
                    };
                    hierarchyTableItems.Add(hierarchyTableItem);
                }
            }

            return hierarchyTableItems;
        }

        public async Task RemoveItemAsync(long id)
        {
            var hierarchyTableItemEntity = await db.HierarchyTableItemsEntity.FirstOrDefaultAsync(i => i.Id == id);

            if (hierarchyTableItemEntity != null)
            {
                db.HierarchyTableItemsEntity.RemoveRange(hierarchyTableItemEntity);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateItemAsync(HierarchyTableItem hierarchyTableItem)
        {
            var hierarchyTableItemEntity = await db.HierarchyTableItemsEntity.FirstOrDefaultAsync(i => i.Id == hierarchyTableItem.Id);

            if (hierarchyTableItemEntity != null)
            {
                hierarchyTableItemEntity.Name = hierarchyTableItem.Name;

                await db.SaveChangesAsync();
            }
        }
    }
}
