using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy.Domain.Entities
{
    public class HierarchyTableItem
    {
        public HierarchyTableItem(long id)
        {
            Id = id;
        }

        public long Id { get; private set; }
        public string Name { get; init; } = string.Empty;
        public long? ParentId { get; init; }
    }
}
