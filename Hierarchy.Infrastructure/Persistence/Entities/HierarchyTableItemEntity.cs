using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy.Infrastructure.Persistence.Entities
{
    public class HierarchyTableItemEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long? ParentId { get; set; }
    }
}
