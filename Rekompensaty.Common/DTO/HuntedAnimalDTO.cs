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
        public decimal PricePerKilo { get; set; }
        public double Weight { get; set; }

        public string HuntDateString
        {
            get { return HuntDate.ToShortDateString(); }
        }

        public double TotalPrice
        {
            get
            {
                return Convert.ToDouble(PricePerKilo) * Weight;
            }
        }

        public double Revenue
        {
            get
            {
                return TotalPrice * 0.1;
            }
        }

        public Guid UserId { get; set; }
    }
}
