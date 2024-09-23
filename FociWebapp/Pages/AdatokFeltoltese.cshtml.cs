using FociWebapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FociWebapp.Pages
{
    public class AdatokFeltolteseModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly FociDbContext _context;
        public AdatokFeltolteseModel(IWebHostEnvironment env, FociDbContext context)
        {
            _env = env;
            _context = context;
        }
        [BindProperty]
        public IFormFile Feltoltes { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync() {

            if (Feltoltes == null || Feltoltes.Length == 0) {
                ModelState.AddModelError("Feltoltes", "Adj meg egy állományt!");
                return Page();
            }

            var UploadDirPath = Path.Combine(_env.ContentRootPath, "uploads");
            var UploadFilePath = Path.Combine(UploadDirPath, Feltoltes.FileName);

            using (var stream = new FileStream(UploadFilePath, FileMode.Create))
            {
                await Feltoltes.CopyToAsync(stream);
            }

            StreamReader sr = new StreamReader(UploadFilePath);
            sr.ReadLine();

            while (!sr.EndOfStream) {
                var line = sr.ReadLine();
                var elemek = line.Split(" ");

                Meccs ujMeccs = new Meccs();
                ujMeccs.Fordulo = int.Parse(elemek[0]);
                ujMeccs.HazaiVeg = int.Parse(elemek[1]);
                ujMeccs.VendegVeg = int.Parse(elemek[2]);
                ujMeccs.HazaiFelido = int.Parse(elemek[3]);
                ujMeccs.VendegFelido = int.Parse(elemek[4]);
                ujMeccs.HazaiCsapat = elemek[5];
                ujMeccs.VendegCsapat = elemek[6];

                _context.Meccsek.Add(ujMeccs);
            }

            sr.Close();
            _context.SaveChanges();

            System.IO.File.Delete(UploadFilePath);
            return Page();
        }
    }
}
