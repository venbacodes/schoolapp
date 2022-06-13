using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Pages
{
    public class StaffsModel : PageModel
    {
        private readonly ILogger<StaffsModel> _logger;

        public List<Staff> StaffList{get;set;}

        public StaffsModel(ILogger<StaffsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {        
            StaffList=new List<Staff>()     
            {
                new Staff{Name="Kamarthaj",Image="profile.png",Education="Principal"},
                new Staff{Name="Sultana",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Kathija",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Parveen",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Farzana",Image="profile.png",Education="T.G.T"},
                new Staff{Name="Ayesha",Image="profile.png",Education="P.E.T"},
                new Staff{Name="Fathima",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Sumaiya",Image="profile.png",Education="T.G.T"},
                new Staff{Name="Dhanam",Image="profile.png",Education="T.G.T"},
                new Staff{Name="Diana",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Revathi",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Jafrin",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Lakshmi",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Priya",Image="profile.png",Education="T.G.T"},
                new Staff{Name="Anushiya",Image="profile.png",Education="T.G.T"},
                new Staff{Name="Jenitha",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Mercy",Image="profile.png",Education="T.G.T"},
                new Staff{Name="Jean Renitha",Image="profile.png",Education="T.G.T"},
                new Staff{Name="Deepika",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Nasrin",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Afrin",Image="profile.png",Education="P.R.T"},
                new Staff{Name="Saira banu",Image="profile.png",Education="P.R.T"}

            };
        }
    }

    public class Staff
    {
        public string Name{get;set;}
        public string Department{get;set;}
        public string Image{get;set;}

        public string Education{get;set;}

    }
}
