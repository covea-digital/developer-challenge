using System;
using System.Collections.Generic;
using System.Linq;

namespace PetInsuranceApi
{
    public class Quotes
    {
        public IEnumerable<Quote> AllQuotes { get; set; }

        public double TotalPremiums 
        {
            get
            {
                return AllQuotes == null ? 0.0 : AllQuotes.Select(x => x.Premium).Sum();
            }
        }
    }
}
