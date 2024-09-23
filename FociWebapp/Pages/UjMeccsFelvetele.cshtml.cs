using FociWebapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FociWebapp.Pages
{
    public class UjMeccsFelveteleModel : PageModel
    {
        [BindProperty]
        public Meccs UjMeccs { get; set; }
        public List<Meccs> meccsekLista { get; set; } = new List<Meccs>();
        private readonly FociDbContext _context;
        public UjMeccsFelveteleModel(FociDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            meccsekLista = _context.Meccsek.ToList();
        }
        public IActionResult OnPost() {
            _context.Meccsek.Add(UjMeccs);
            _context.SaveChanges();
            return Page();
        }
    }
}
