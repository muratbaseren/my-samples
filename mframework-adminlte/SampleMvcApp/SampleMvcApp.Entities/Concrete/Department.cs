using SampleMvcApp.Entities.Abstract;

namespace SampleMvcApp.Entities.Concrete
{
    public class Department : EntityBase<int>, IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DepartmentCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DepartmentQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
