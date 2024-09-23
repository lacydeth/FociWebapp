using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FociWebapp.Models;
using FociWebapp.Models.FociWebapp.Models;

namespace FociWebapp.Pages
{
    public class Tabella : PageModel
    {
        private readonly FociDbContext _context;

        public Tabella(FociDbContext context)
        {
            _context = context;
        }

        public IList<TeamResult> TeamResults { get; set; } = new List<TeamResult>();

        public async Task OnGetAsync()
        {
            var matches = await _context.Meccsek.ToListAsync();

            var teamResults = new Dictionary<string, TeamResult>();

            foreach (var match in matches)
            {
                if (!teamResults.ContainsKey(match.HazaiCsapat))
                {
                    teamResults[match.HazaiCsapat] = new TeamResult
                    {
                        Name = match.HazaiCsapat,
                        Wins = 0,
                        Losses = 0,
                        Draws = 0,
                        Goals = 0,
                        Points = 0
                    };
                }

                if (!teamResults.ContainsKey(match.VendegCsapat))
                {
                    teamResults[match.VendegCsapat] = new TeamResult
                    {
                        Name = match.VendegCsapat,
                        Wins = 0,
                        Losses = 0,
                        Draws = 0,
                        Goals = 0,
                        Points = 0
                    };
                }

                teamResults[match.HazaiCsapat].Goals += match.HazaiVeg;
                teamResults[match.VendegCsapat].Goals += match.VendegVeg;

                if (match.HazaiVeg > match.VendegVeg)
                {
                    teamResults[match.HazaiCsapat].Wins++;
                    teamResults[match.HazaiCsapat].Points += 3;
                    teamResults[match.VendegCsapat].Losses++;
                }
                else if (match.HazaiVeg < match.VendegVeg)
                {
                    teamResults[match.VendegCsapat].Wins++;
                    teamResults[match.VendegCsapat].Points += 3;
                    teamResults[match.HazaiCsapat].Losses++;
                }
                else
                {
                    teamResults[match.HazaiCsapat].Draws++;
                    teamResults[match.HazaiCsapat].Points += 1;
                    teamResults[match.VendegCsapat].Draws++;
                    teamResults[match.VendegCsapat].Points += 1;
                }
            }

            TeamResults = teamResults.Values.ToList();
        }
    }
}
