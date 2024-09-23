using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FociWebapp.Pages
{
    public class AdatokFeltolteseModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public AdatokFeltolteseModel(IWebHostEnvironment env)
        {
            _env = env;
        }
        [BindProperty]
        public IFormFile Feltoltes { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync() {

            var UploadDirPath = Path.Combine(_env.ContentRootPath, "uploads");
            var UploadFilePath = Path.Combine(UploadDirPath, Feltoltes.FileName);

            using (var stream = new FileStream(UploadFilePath, FileMode.Create))
            {
                Feltoltes.CopyTo(stream);
            }
            return Page();
        }
    }
}
