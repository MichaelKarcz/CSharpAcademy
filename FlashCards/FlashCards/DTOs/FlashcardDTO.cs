using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.DTOs
{
    internal class FlashcardDTO
    {
        public string Front { get; set; }
        public string Back { get; set; }

        public FlashcardDTO(string front, string back) 
        {
            Front = front;
            Back = back;
        }
    }
}
