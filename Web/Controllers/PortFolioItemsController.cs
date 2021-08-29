using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Core.Interfaces;

namespace Web.Controllers
{
    public class PortFolioItemsController : Controller
    {

        private readonly IUnitOfWork<PortFolioItem> _portfolio;
        private readonly IHostingEnvironment _hosting;

        public PortFolioItemsController(IUnitOfWork<PortFolioItem> portfolio, IHostingEnvironment hosting)
        {

            _portfolio = portfolio;
            _hosting = hosting;
        }

        // GET: PortFolioItems
        public IActionResult Index()
        {
            return View(_portfolio.Entity.GetAll());
        }

        // GET: PortFolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portFolioItem = _portfolio.Entity.GetById(id);

            if (portFolioItem == null)
            {
                return NotFound();
            }

            return View(portFolioItem);
        }

        // GET: PortFolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortFolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PortFolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string FullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(FullPath, FileMode.Create));
                }
                PortFolioItem portFolio = new PortFolioItem
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };
                _portfolio.Entity.Insert(portFolio);
                _portfolio.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortFolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portFolioItem = _portfolio.Entity.GetById(id);
            if (portFolioItem == null)
            {
                return NotFound();
            }

            PortFolioViewModel portFolioView = new PortFolioViewModel
            {
                Id = portFolioItem.Id,
                ProjectName = portFolioItem.ProjectName,
                Description = portFolioItem.Description,
                ImageUrl = portFolioItem.ImageUrl
            };

            return View(portFolioView);
        }

        // POST: PortFolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortFolioViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string FullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(FullPath, FileMode.Create));
                    }
                    PortFolioItem portFolio = new PortFolioItem
                    {
                        Id=model.Id,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName
                    };
                    _portfolio.Entity.Update(portFolio);
                    _portfolio.Save();

                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!PortFolioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortFolioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portFolioItem = _portfolio.Entity.GetById(id);
               
            if (portFolioItem == null)
            {
                return NotFound();
            }

            return View(portFolioItem);
        }

        // POST: PortFolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _portfolio.Entity.Delete(id);
            _portfolio.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortFolioItemExists(Guid id)
        {
            return _portfolio.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
