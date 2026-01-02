using System.ComponentModel.DataAnnotations;

namespace Hierarchy.MVC.Models
{
    public class EditHierarchyTableItem
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
