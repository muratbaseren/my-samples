using AutoMapper;
using SampleMvcApp.Business.Abstract;
using SampleMvcApp.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SampleMvcApp.WebApp.Controllers
{
    public class MemberController : Controller
    {
        private readonly IEntityManager<Member> MemberManager;

        public MemberController(IEntityManager<Member> memberManager)
        {
            MemberManager = memberManager;
        }

        // GET: Member
        public ActionResult Index()
        {
            List<Member> members = MemberManager.List();
            List<MemberQuery> model = members.Select(x => Mapper.Map<MemberQuery>(x)).ToList();

            return View(model);
        }

        // GET: Member/Details/5
        public ActionResult Details(int id)
        {
            Member member = MemberManager.Find(x => x.Id == id);
            MemberQuery model = Mapper.Map<MemberQuery>(member);

            return View(model);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            return View(new MemberCommand());
        }

        // POST: Member/Create
        [HttpPost]
        public ActionResult Create(MemberCommand model)
        {
            if (ModelState.IsValid)
            {
                Member member = Mapper.Map<Member>(model);
                MemberManager.Insert(member);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int id)
        {
            Member member = MemberManager.Find(x => x.Id == id);
            MemberCommand model = Mapper.Map<MemberCommand>(member);

            return View(model);
        }

        // POST: Member/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MemberCommand model)
        {
            if (ModelState.IsValid)
            {
                Member member = MemberManager.Find(x => x.Id == id);
                Mapper.Map(model, member);

                MemberManager.Update(member);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int id)
        {
            Member member = MemberManager.Find(x => x.Id == id);
            MemberQuery model = Mapper.Map<MemberQuery>(member);

            return View(model);
        }

        // POST: Member/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Member member = MemberManager.Find(x => x.Id == id);
            MemberManager.Delete(member);

            return RedirectToAction("Index");
        }
    }
}
