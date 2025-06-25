using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    internal class StudySession
    {
        public int Id { get; set; }
        public Deck StudyDeck { get; set; }
        public int Score { get; set; }
        public DateTime SessionDate { get; set; }

        public StudySession() 
        {

        }
    }
}
