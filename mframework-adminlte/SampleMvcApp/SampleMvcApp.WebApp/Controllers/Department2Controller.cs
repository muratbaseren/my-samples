using SampleMvcApp.Business.Abstract;
using SampleMvcApp.Entities.Concrete;
using SampleMvcApp.WebApp.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace SampleMvcApp.WebApp.Controllers
{
    public class Department2Controller : Controller
    {
        IEntityManager<Department> departmentManager;
        IEntityManager<Member> memberManager;

        public Department2Controller(IEntityManager<Department> departmentManager, IEntityManager<Member> memberManager)
        {
            this.departmentManager = departmentManager;
            this.memberManager = memberManager;
        }

        public ActionResult Index()
        {
            // ÖZel sorgu oluşturma..
            //var query = departmentManager
            //    .ListQueryable()
            //    .Where(x => x.Id > 5)
            //    .OrderBy(x => x.Name)
            //    .Take(5)
            //    .ToList();

            var departments = departmentManager.List();
            var member = memberManager.List();

            var model = member.Select(m => new MemberDepartmentViewModel
            {
                Name = m.Name,
                Surname = m.Surname,
                DepartmentName = departments.First()?.Name,
                DepartmentDesc = departments.First()?.Description
            }).ToList();

            return View(model);
        }
    }
}