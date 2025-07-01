using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.DTOs
{
    internal class FlashcardDTO
    {
        internal int Id { get; set; }
        internal string Front { get; set; }
        internal string Back { get; set; }
        internal int DeckId { get; set; }

        internal FlashcardDTO()
        {

        }
        
        internal FlashcardDTO(int id, string front, string back, int deckId) 
        {
            Id = id;
            Front = front;
            Back = back;
            DeckId = deckId;

        }

        public override string ToString()
        {
            return $"{Front} / {Back}";
        }

    }
}
