using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekompensaty.Common.DTO
{
    public class HuntedAnimalDTO : BaseDTO
    {
        public DateTime HuntDate { get; set; }
        public AnimalTypeDTO AnimalType { get; set; }
        public decimal CashGiveback { get; set; }

        public string HuntDateString
        {
            get { return HuntDate.ToShortDateString(); }
        }
    }
}
