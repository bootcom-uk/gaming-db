using Realms;

namespace Gaming.RealmObjects
{
    public partial class CeXGame_SellPrices : IEmbeddedObject
    {
        public DateTimeOffset? DateChecked { get; set; }
        public double? Price { get; set; }
    }
}
