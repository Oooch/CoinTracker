using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoinTracker.Tests
{
    [TestClass]
    public class CoinTests
    {
        [TestMethod]
        public void GetCoins()
        {
            CoinTracker.Models.CoinAPI coinAPI = new Models.CoinAPI();
            var result = coinAPI.CoinsGet();
        }
    }
}
