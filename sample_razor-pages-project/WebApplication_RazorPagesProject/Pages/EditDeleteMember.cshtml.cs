using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication_RazorPagesProject.Pages.Shared
{
    public class EditDeleteMemberModel : PageModel
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

        private void LoadMemberTypes()
        {
            MemberTypes = new List<string> { "Admin", "User" };
        }

        public void OnGet()
        {
            LoadMemberTypes();

            Name = "Murat";
            Surname = "Baþeren";
            Age = 30;
            MemberType = "Admin";
        }

        public void OnPostSave()
        {
            LoadMemberTypes();
            Result = "Kaydedildi.";
        }

        public void OnPostRemove()
        {
            LoadMemberTypes();
            Result = "Silindi.";
        }
    }
}
