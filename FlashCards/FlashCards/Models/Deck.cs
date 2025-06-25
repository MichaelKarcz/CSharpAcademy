using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    internal class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Flashcard> Flashcards { get; set; }


        public Deck()
        {
            Name = string.Empty;
            Flashcards = new List<Flashcard>();
        }

        public Deck(string name, List<Flashcard> flashcards)
        {
            Name = name;
            Flashcards = flashcards;
        }

    }
}
