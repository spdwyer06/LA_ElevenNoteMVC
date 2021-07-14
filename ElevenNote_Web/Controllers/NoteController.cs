using ElevenNote_Models.NoteModels;
using ElevenNote_Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote_Web.Controllers
{
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            var service = CreateNoteService();
            var model = service.GetAllNotes();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateNoteService();

            if (!service.CreateNote(model))
            {
                ModelState.AddModelError("", "Note could not be created.");

                return View(model);
            }

            TempData["SaveResult"] = "Your note was created.";

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int noteId)
        {
            var service = CreateNoteService();
            var model = service.GetNoteByNoteId(noteId);

            return View(model);
        }

        public ActionResult Edit(int noteId)
        {
            var service = CreateNoteService();
            var note = service.GetNoteByNoteId(noteId);
            var model = new NoteEdit
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int noteId, NoteEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Id != noteId)
            {
                ModelState.AddModelError("", "Id mismatch.");

                return View(model);
            }

            var service = CreateNoteService();

            if (!service.UpdateNote(model))
            {
                ModelState.AddModelError("", "Your note could not be updated.");

                return View(model);
            }

            TempData["SaveResult"] = "Your note was updated.";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int noteId)
        {
            var service = CreateNoteService();
            var model = service.GetNoteByNoteId(noteId);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNote(int noteId)
        {
            var service = CreateNoteService();

            service.DeleteNote(noteId);

            TempData["SaveResult"] = "Your note was deleted.";

            return RedirectToAction("Index");
        }



        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());

            return new NoteService(userId);
        }
    }
}