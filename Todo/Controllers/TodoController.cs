using Database.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Service.Services;
using Todo.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Todo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : Controller
    {
        private readonly IDbService _dbService;

        public TodoController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        [Route("/index")]
        public void Index()
        {

        }

        [HttpGet]
        [Route("/getitems")]
        public async Task<IActionResult> GetItemsPartial()
        {
            var items = await _dbService.GetAllItems();
            return PartialView("~/Views/_ItemList.cshtml", items);
        }

        [HttpPost]
        [Route("/saveitem")]
        public async Task<IActionResult> SaveItem([FromBody] Item item)
        {
            if (item.Task.Length > Config.MaxCharForTask)
            {
                await _dbService.ErrorLog("Task too long");
                return Redirect("/getitems");
            }
            
            if (String.IsNullOrEmpty(item.Task) || item.DueDateUtc == new DateTime())
            {
                await _dbService.ErrorLog("Missing Task or DueDate");
                return Redirect("/getitems");
            }

            if (item.DueDateUtc.Date < DateTime.UtcNow.Date)
            {
                await _dbService.ErrorLog("Missing Task or DueDate");
                return Redirect("/getitems");
            }

            item.CreatedDateUtc = DateTime.UtcNow;
            item.IsDone = false;
            await _dbService.InsertItem(item);

            return Redirect("/getitems");
        }

        [HttpPost]
        [Route("/deleteitem")]
        public async Task<IActionResult> DeleteItem([FromBody] Item item)
        {
            await _dbService.DeleteItem(item.Id);
            return Redirect("/getitems");
        }

        [HttpPost]
        [Route("/updateitemstatus")]
        public async Task<IActionResult> UpdateItemStatus([FromBody] Item item)
        {
            var entity = await _dbService.GetItemById(item.Id);
            if (entity != null)
            {
                entity.IsDone = item.IsDone;
                await _dbService.UpdateItem(entity);
            }

            return Redirect("/getitems");
        }
    }
}
