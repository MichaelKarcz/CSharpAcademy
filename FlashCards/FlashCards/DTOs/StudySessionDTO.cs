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
        public Deck StudyDeck { get; set; }
        public DateTime SessionDate { get; set; }

        public StudySessionDTO() 
        {

        }
    }
}
