using Hierarchy.Application;
using Hierarchy.Domain.Entities;
using Hierarchy.Domain.Exceptions;
using Hierarchy.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hierarchy.MVC.Controllers
{
    public class HierarchyTableItemsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HierarchyTableItemService _hierarchyTableItemService;
        public HierarchyTableItemsController(ILogger<HomeController> logger, HierarchyTableItemService service)
        {
            _logger = logger;
            _hierarchyTableItemService = service;

        }

        [HttpGet]
        public async Task<IActionResult> Parents()
        {
            try
            {
                var items = await _hierarchyTableItemService.GetItems();

                return View(items);
            }
            catch (Exception)
            {
                _logger.LogError("Internal Server Error occured Parents()");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetChildItems(long id)
        {
            try
            {
                var items = await _hierarchyTableItemService.GetParentItems(id);
                return PartialView("ChildItems", items);
            }
            catch (Exception)
            {
                _logger.LogError($"Internal Server Error occured GetChildItems ({id})");
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveItem(long id)
        {
            try
            {
                await _hierarchyTableItemService.RemoveItem(id);
                return Ok();
            }
            catch (ItemNotFoundException)
            {
                _logger.LogError($"ItemNotFoundException Error occured RemoveItem ({id})");
                return NotFound();
            }
            catch (Exception)
            {
                _logger.LogError($"Internal Server Error occured RemoveItem ({id})");
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateItem([FromBody] EditHierarchyTableItem editHierarchyTableItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editItem = new HierarchyTableItem(editHierarchyTableItem.Id) { Name = editHierarchyTableItem.Name };

                    await _hierarchyTableItemService.UpdateItem(editItem);
                    return Ok();
                }
                return CheckForErrors();
            }
            catch (ItemNotFoundException)
            {
                _logger.LogError($"ItemNotFoundException Error occured UpdateItem ({editHierarchyTableItem})");
                return NotFound();
            }
            catch (Exception)
            {
                _logger.LogError($"Internal Server Error occured UpdateItem ({editHierarchyTableItem})");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateItem([FromBody] CreateHierarchyTableItem createHierarchyTableItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createdItem = new HierarchyTableItem(0)
                    {
                        Name = createHierarchyTableItem.Name,
                        ParentId = createHierarchyTableItem.ParentId
                    };
                    var id = await _hierarchyTableItemService.AddItem(createdItem);
                    return Ok(id);
                }
                return CheckForErrors();
            }
            catch (Exception)
            {
                _logger.LogError($"Internal Server Error occured CreateItem({createHierarchyTableItem})");
                return StatusCode(500);
            }
        }

        private ActionResult CheckForErrors()
        {
            string errorMessages = string.Empty;

            foreach (var item in ModelState)
            {
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    errorMessages = $"{errorMessages}\nProperties' errors {item.Key}:\n";
                    foreach (var error in item.Value.Errors)
                    {
                        errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                    }
                }
            }
            return BadRequest(errorMessages);
        }

        [HttpGet]
        public async Task<IActionResult> GetItem(long id)
        {
            try
            {
                var item = await _hierarchyTableItemService.GetItem(id);
                return PartialView("Adding", item);
            }
            catch (ItemNotFoundException)
            {
                _logger.LogError($"ItemNotFoundException Error occured GetItem ({id})");
                return NotFound();
            }
            catch (Exception)
            {
                _logger.LogError($"Internal Server Error occured GetItem ({id})");
                return StatusCode(500);
            }
        }
    }
}
