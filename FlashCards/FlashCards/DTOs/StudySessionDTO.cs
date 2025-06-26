using FlashCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.DTOs
{
    internal class StudySessionDTO
    {
        internal int Id { get; set; }
        internal Deck StudyDeck { get; set; }
        internal DateTime SessionDate { get; set; }
        internal int Score { get; set; }

        internal StudySessionDTO() 
        {

        }

        internal StudySessionDTO(int id, Deck studyDeck, DateTime sessionDate, int score)
        {
            Id = id;
            StudyDeck = studyDeck;
            SessionDate = sessionDate;
            Score = score;
        }
    }
}
