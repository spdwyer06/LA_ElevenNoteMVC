using ElevenNote_Data;
using ElevenNote_Models.NoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote_Services
{
    public class NoteService
    {
        private readonly Guid _userId;

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            var note = new Note
            {
                OwnerId = _userId,
                Title = model.Title,
                Content = model.Content,
                CreatedUtc = DateTimeOffset.UtcNow
            };

            using (var db = new ApplicationDbContext())
            {
                db.Notes.Add(note);

                return db.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetAllNotes()
        {
            using (var db = new ApplicationDbContext())
            {
                var notes = db.Notes.Where(note => note.OwnerId == _userId)
                                    .Select(note => new NoteListItem
                                    {
                                        Id = note.Id,
                                        Title = note.Title,
                                        CreatedUtc = note.CreatedUtc
                                    });

                return notes.ToArray();
            }
        }

        public NoteDetail GetNoteByNoteId(int noteId)
        {
            using (var db = new ApplicationDbContext())
            {
                var note = db.Notes.Single(n => n.Id == noteId && n.OwnerId == _userId);

                return new NoteDetail
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content,
                    CreatedUtc = note.CreatedUtc,
                    ModifiedUtc = note.ModifiedUtc
                };
            }
        }

        public bool UpdateNote(NoteEdit model)
        {
            using (var db = new ApplicationDbContext())
            {
                var note = db.Notes.Single(n => n.Id == model.Id && n.OwnerId == _userId);

                note.Title = model.Title;
                note.Content = model.Content;
                note.ModifiedUtc = DateTimeOffset.UtcNow;

                return db.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int noteId)
        {
            using (var db = new ApplicationDbContext())
            {
                var note = db.Notes.Single(n => n.Id == noteId && n.OwnerId == _userId);

                db.Notes.Remove(note);

                return db.SaveChanges() == 1;
            }
        }
    }
}
