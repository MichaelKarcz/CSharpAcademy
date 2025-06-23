using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards
{
    internal class FlashcardStack
    {
        List<Flashcard> Flashcards;


        public FlashcardStack()
        {
            Flashcards = new List<Flashcard>();
        }

        public FlashcardStack(List<Flashcard> flashcards)
        {
            Flashcards = flashcards;
        }

    }
}
