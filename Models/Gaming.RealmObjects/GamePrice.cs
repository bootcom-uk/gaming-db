using Realms;

namespace Gaming.RealmObjects
{
    public partial class GamePrice : IEmbeddedObject
    {

        [Ignored]
        public DateTime DateTimeChecked { get { return DateChecked.DateTime; } }

        public DateTimeOffset DateChecked { get; set; }

        public decimal SalePrice { get; set; }

        public decimal ExchangePrice { get; set; }

        public decimal PurchasePrice { get; set; }

    }
}
