using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetInsuranceApi;
using System.Collections.Generic;
using System.Linq;

namespace PetInsuranceApiTests
{
    [TestClass]
    public class QuotesTests
    {
        [TestMethod]
        public void TotalPremiums_PremiumsPresent_ShouldBeSumOfPremiums()
        {
            double premiumOne = 80.56;
            double premiumTwo = 40;
            double premiumThree = 300.34;
            double total = premiumOne + premiumTwo + premiumThree;

            var quotes = new Quotes();
            var quoteList = new List<Quote>();
            quotes.AllQuotes = quoteList;

            quoteList.Add(new Quote
            {
                Premium = premiumOne
            });

            quoteList.Add(new Quote
            {
                Premium = premiumTwo
            });

            quoteList.Add(new Quote
            {
                Premium = premiumThree
            });
            
            Assert.AreEqual(total, quotes.TotalPremiums);
        }

        [TestMethod]
        public void TotalPremiums_AllQuotesCollectionNull_ShouldBeZero()
        {
            var quotes = new Quotes();

            Assert.IsNull(quotes.AllQuotes);
            Assert.AreEqual(0.0, quotes.TotalPremiums);
        }
    }
}
