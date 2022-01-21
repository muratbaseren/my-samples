using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication_RazorPagesProject.Pages
{
    public class CreateMemberModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Surname { get; set; }

        [BindProperty]
        public int Age { get; set; }

        [BindProperty]
        public string MemberType { get; set; }

        public List<string> MemberTypes { get; set; }

        public string Result { get; set; }

        public void OnGet()
        {
            Result = null;
            LoadMemberTypes();
        }

        public void OnPost()
        {
            LoadMemberTypes();
            Result = $"{Name} {Surname} ({Age}) - {MemberType}";

            Name = null;
            Surname = null;
            Age = 0;
            MemberType = null;
        }

        private void LoadMemberTypes()
        {
            MemberTypes = new List<string> { "Admin", "User" };
        }
    }
}
