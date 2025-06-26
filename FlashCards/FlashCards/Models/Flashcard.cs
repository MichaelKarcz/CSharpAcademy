using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    internal class Flashcard
    {
        internal int Id { get; set; }
        internal string Front {  get; set; }
        internal string Back { get; set; }
        internal int DeckId { get; set; }

        internal Flashcard() 
        {
            Front = string.Empty;
            Back = string.Empty;
        }

        internal Flashcard(string front, string back)
        {
            Front = front;
            Back = back;
        }   
    }
}
