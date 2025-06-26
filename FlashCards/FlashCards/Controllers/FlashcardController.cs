using FlashCards.DTOs;
using FlashCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Controllers
{
    internal static class FlashcardController
    {
        internal static void DisplayFlashcards(List<FlashcardDTO> flashcardDTOs)
        {
            foreach (FlashcardDTO flashcard in flashcardDTOs)
            {
                Console.WriteLine($"{flashcard.Id} | {flashcard.Front} | {flashcard.Back}");
            }
        }

        internal static void CreateAFlashcard(int deckId)
        {

        }


    }
}
