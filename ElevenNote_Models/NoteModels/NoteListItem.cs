using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote_Models.NoteModels
{
    public class NoteListItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Created At")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
