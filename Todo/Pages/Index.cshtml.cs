using Database.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Services;

namespace Todo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDbService _dbService;

        //[BindProperty]
        public IList<Item> Items { get; set; }

        public IndexModel(ILogger<IndexModel> logger,
                          IDbService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }

        public async Task OnGetAsync()
        {
            Items = await _dbService.GetAllItems();
        }
    }
}