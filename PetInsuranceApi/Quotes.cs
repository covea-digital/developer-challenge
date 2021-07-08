using System;
using System.Linq;

namespace PetInsuranceApi
{
    public class Quotes
    {
        IEnumerable<Quote> Quotes { get; set; }

        public double TotalPremiums 
        {
            get
            {
                return Quotes?.Select(x => x.Premium).Sum();
            }
        }
    }
}
