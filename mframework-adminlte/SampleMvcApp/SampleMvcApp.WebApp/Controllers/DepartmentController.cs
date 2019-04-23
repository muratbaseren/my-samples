using SampleMvcApp.Business.Abstract;
using SampleMvcApp.Entities.Concrete;

namespace SampleMvcApp.WebApp.Controllers
{
    public class DepartmentController : ControllerEntityBase<Department, DepartmentCommand, DepartmentQuery>
    {
        IEntityManager<Department> departmentManager;

        public DepartmentController(IEntityManager<Department> departmentManager) : base(departmentManager)
        {
            this.departmentManager = departmentManager;
        }
    }
}