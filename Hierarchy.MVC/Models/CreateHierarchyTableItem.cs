using System.ComponentModel.DataAnnotations;

namespace Hierarchy.MVC.Models
{
    public class CreateHierarchyTableItem
    {
        public string Name { get; init; } = string.Empty;
        public long? ParentId { get; set; }
    }
}
