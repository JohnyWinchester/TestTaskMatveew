using System.ComponentModel.DataAnnotations;

namespace TestTaskMatveew.Domain
{
    public class Offer
    {
        [Required]
        public int Id { get; set; }
        public string Url { get; set; }
        public int Price { get; set; }
        public string CurrencyId { get; set; }
        public string CategoryId { get; set; }
        public string Picture { get; set; }
        public bool Delivery { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
    }
}
