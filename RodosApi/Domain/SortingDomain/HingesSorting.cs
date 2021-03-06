using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain.SortingDomain
{
    public class HingesSorting
    {
        public byte IdSort { get; set; }
        public byte PriceSort { get; set; }
        public byte NameSort { get; set; }
        public byte TypeOfHingeNameSort { get; set; }
        public byte MakerNameSort { get; set; }
        public byte MaterialNameSort { get; set; }
    }
}
