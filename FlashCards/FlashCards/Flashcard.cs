using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards
{
    internal class Flashcard
    {

        public string Front {  get; set; }
        public string Back { get; set; }


        public Flashcard() 
        {
            Front = string.Empty;
            Back = string.Empty;
        }

        public Flashcard(string front, string back)
        {
            Front = front;
            Back = back;
        }   
    }
}
