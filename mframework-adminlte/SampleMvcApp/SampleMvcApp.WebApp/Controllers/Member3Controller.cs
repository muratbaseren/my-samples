using AutoMapper;
using SampleMvcApp.Business.Abstract;
using SampleMvcApp.Entities.Concrete;
using SampleMvcApp.WebApp.Substructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMvcApp.WebApp.Controllers
{
    public class Member3Controller : Controller
    {
        private readonly IEntityManager<Member> MemberManager;

        public Member3Controller(IEntityManager<Member> memberManager)
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

        public PartialViewResult ListMember()
        {
            List<Member> members = MemberManager.List();
            List<MemberQuery> model = members.Select(x => Mapper.Map<MemberQuery>(x)).ToList();

            return PartialView("_ListMember", model);
        }

        // GET: Member/Create
        public PartialViewResult Create()
        {
            return PartialView("_CreateMember", new MemberCommand());
        }

        // POST: Member/Create
        [HttpPost]
        public JsonResult Create(MemberCommand model)
        {
            if (ModelState.IsValid)
            {
                if (MemberManager.Find(x => x.Email.ToLower() == model.Email.ToLower()) != null)
                {
                    ModelState.AddModelError(nameof(MemberCommand.Email), "Bu e-posta zaten kayıtlıdır.");
                }
                else
                {
                    Member member = Mapper.Map<Member>(model);
                    MemberManager.Insert(member);

                    return Json(new JsonResultObject<string>() {
                        ResultMessage = "Kayıt yapılmıştır."
                    });
                }
            }

            JsonResultObject<List<string>> result =
                new JsonResultObject<List<string>>()
                {
                    HasError = true,
                    ErrorMessages = ModelState.GetExceptionMessageList()
                };

            return Json(result);
        }

        // GET: Member/Create
        public PartialViewResult Edit(int id)
        {
            Member member = MemberManager.Find(x => x.Id == id);
            MemberCommand model = Mapper.Map<MemberCommand>(member);

            return base.PartialView("_EditMember", model);
        }

        // POST: Member/Create
        [HttpPost]
        public JsonResult Edit(MemberCommand model)
        {
            if (ModelState.IsValid)
            {
                if (MemberManager.Find(x => x.Email.ToLower() == model.Email.ToLower() && x.Id != model.Id) != null)
                {
                    ModelState.AddModelError(nameof(MemberCommand.Email), "Bu e-posta zaten kayıtlıdır.");
                }
                else
                {
                    Member member = MemberManager.Find(x => x.Id == model.Id);
                    Mapper.Map(model, member);
                    MemberManager.Update(member);

                    return Json(new JsonResultObject<string>()
                    {
                        ResultMessage = "Güncelleme yapılmıştır."
                    });
                }
            }

            JsonResultObject<List<string>> result =
                new JsonResultObject<List<string>>()
                {
                    HasError = true,
                    ErrorMessages = ModelState.GetExceptionMessageList()
                };

            return Json(result);
        }
    }
}