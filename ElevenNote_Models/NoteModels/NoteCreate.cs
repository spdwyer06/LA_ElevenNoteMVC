using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote_Models.NoteModels
{
    public class NoteCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Enter atleast 2 characters for the note title.")]
        [MaxLength(100, ErrorMessage = "Max of 100 characters allowed for the note title.")]
        public string Title { get; set; }

        [MaxLength(8000)]
        public string Content { get; set; }
    }
}
