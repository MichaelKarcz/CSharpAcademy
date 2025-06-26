using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.DTOs
{
    internal class DeckDTO
    {
        internal int Id { get; set; }
        internal string Name { get; set; }


        internal DeckDTO()
        {

        }

        internal DeckDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }



    }
}
