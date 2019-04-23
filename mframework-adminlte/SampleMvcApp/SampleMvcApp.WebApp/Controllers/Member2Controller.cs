using SampleMvcApp.Business.Abstract;
using SampleMvcApp.Entities.Concrete;

namespace SampleMvcApp.WebApp.Controllers
{
    public class Member2Controller : ControllerEntityBase<Member, MemberCommand, MemberQuery>
    {
        private readonly IEntityManager<Member> MemberManager;

        public Member2Controller(IEntityManager<Member> memberManager) : base(memberManager)
        {
            MemberManager = memberManager;
        }
    }
}