using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FociWebapp.Models;

namespace FociWebapp.Pages
{
    public class MeccsekKilistazasaModel : PageModel
    {
        private readonly FociWebapp.Models.FociDbContext _context;

        public MeccsekKilistazasaModel(FociWebapp.Models.FociDbContext context)
        {
            _context = context;
        }

        public IList<Meccs> Meccs { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Meccs = await _context.Meccsek.ToListAsync();
        }
    }
}
