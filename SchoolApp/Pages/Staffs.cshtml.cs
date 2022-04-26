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
                new Staff{Name="Ram",Department="Physics",Image="profile.png",Education="M.Sc. M.Ed."},
                new Staff{Name="Mohammed",Department="English",Image="profile.png",Education="M.A. M.Ed."},
                new Staff{Name="Rahul",Department="Physics",Image="profile.png",Education="M.Sc. M.Ed."},
                new Staff{Name="Mohan",Department="English",Image="profile.png",Education="M.A. M.Ed."},
                new Staff{Name="Rahman",Department="Physics",Image="profile.png",Education="M.Sc. M.Ed."},
                new Staff{Name="Mohammed dhaneesh",Department="English",Image="profile.png",Education="M.A. M.Ed."},
                new Staff{Name="Aarees",Department="Physics",Image="profile.png",Education="M.Sc. M.Ed."},
                new Staff{Name="Kadhar",Department="English",Image="profile.png",Education="M.A. M.Ed."},
                new Staff{Name="Ajmal",Department="Physics",Image="profile.png",Education="M.Sc. M.Ed."},
                new Staff{Name="Umar",Department="English",Image="profile.png",Education="M.A. M.Ed."}


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
