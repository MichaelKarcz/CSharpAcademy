using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    internal class StudySession
    {
        internal int Id { get; set; }
        internal Deck StudyDeck { get; set; }
        internal int Score { get; set; }
        internal DateTime SessionDate { get; set; }

        internal StudySession() 
        {

        }
    }
}
