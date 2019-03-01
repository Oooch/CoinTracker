using Binance.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinTracker.Models
{
    public class CoinAPI
    {
        public CryptoExchange.Net.Objects.WebCallResult<Binance.Net.Objects.Binance24HPrice[]> CoinsGet()
        {
            using (var client = new BinanceClient())
            {
                return client.Get24HPricesList();
            }
        }

        public void BinanceUpdate(IServiceProvider serviceProvider)
        {
            using (var context = new Data.ApplicationDbContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<Data.ApplicationDbContext>>()))
            {
                var BinanceCoins = CoinsGet();
                foreach (var Coin in BinanceCoins.Data.OrderBy(o => o.Symbol))
                {
                    var coinExists = context.Coin.Where(o => o.Exchange == "Binance" && o.Pair == Coin.Symbol).FirstOrDefault();
                    if (coinExists == null)
                    {
                        Coin coin = new Coin() { Exchange = "Binance", Pair = Coin.Symbol, CurrentPrice = Coin.LastPrice, PriceChange = Coin.PriceChangePercent, Volume = Coin.Volume, LastUpdated = DateTime.Now};
                        context.Coin.Add(coin);
                    }
                    else
                    {
                        coinExists.CurrentPrice = Coin.LastPrice;
                        coinExists.PriceChange = Coin.PriceChangePercent;
                        coinExists.Volume = Coin.Volume;
                        coinExists.LastUpdated = DateTime.Now;
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
