using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinTracker.Models
{
    public class Coin
    {
        public int Id { get; set; }
        public string Exchange { get; set; }
        public string Pair { get; set; }
        [Display(Name = "Current Price"), DisplayFormat(DataFormatString = "{0:N8}")]
        public decimal CurrentPrice { get; set; }
        [Display(Name = "Price Change"), DisplayFormat(DataFormatString = "{0:N2}%")]
        public decimal PriceChange { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Volume { get; set; }
        public DateTime LastUpdated { get; set; }
    }    
}
