using AutoMapper;
using SampleMvcApp.Business.Abstract;
using SampleMvcApp.Entities.Abstract;
using SampleMvcApp.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SampleMvcApp.WebApp.Controllers
{
    public class ControllerEntityBase<TEntity, TEntityCommand, TEntityQuery> : Controller
        where TEntity : EntityBase<int>, IEntity, new()
        where TEntityCommand : class, new()
        where TEntityQuery : class, new()
    {
        private readonly IEntityManager<TEntity> manager;

        public ControllerEntityBase(IEntityManager<TEntity> manager)
        {
            this.manager = manager;
        }

        // GET: Member
        public ActionResult Index()
        {
            List<TEntity> list = manager.List();
            List<TEntityQuery> model = list.Select(x => Mapper.Map<TEntityQuery>(x)).ToList();

            return View(model);
        }

        // GET: Member/Details/5
        public ActionResult Details(int id)
        {
            TEntity entity = manager.Find(x => x.Id == id);
            TEntityQuery model = Mapper.Map<TEntityQuery>(entity);

            return View(model);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            return View(new TEntityCommand());
        }

        // POST: Member/Create
        [HttpPost]
        public ActionResult Create(TEntityCommand model)
        {
            if (ModelState.IsValid)
            {
                TEntity entity = Mapper.Map<TEntity>(model);
                manager.Insert(entity);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int id)
        {
            TEntity entity = manager.Find(x => x.Id == id);
            TEntityCommand model = Mapper.Map<TEntityCommand>(entity);

            return View(model);
        }

        // POST: Member/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TEntityCommand model)
        {
            if (ModelState.IsValid)
            {
                TEntity entity = manager.Find(x => x.Id == id);
                Mapper.Map(model, entity);

                manager.Update(entity);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int id)
        {
            TEntity entity = manager.Find(x => x.Id == id);
            TEntityQuery model = Mapper.Map<TEntityQuery>(entity);

            return View(model);
        }

        // POST: Member/Delete/5
        [HttpPost]
        [ActionName(nameof(Delete))]
        public ActionResult DeleteConfirm(int id)
        {
            TEntity entity = manager.Find(x => x.Id == id);
            manager.Delete(entity);

            return RedirectToAction(nameof(Index));
        }
    }
}