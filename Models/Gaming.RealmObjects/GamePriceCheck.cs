using MongoDB.Bson;
using Realms;

namespace Gaming.RealmObjects
{
    public partial class GamePriceCheck : IRealmObject
    {

        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; }

        [Indexed]
        public Guid UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CexId { get; set; } = string.Empty;

        public string? CategoryName { get; set; }

        public string? GameImageUrl { get; set; } 

        public DateTimeOffset DateAdded { get; set; } = DateTime.Now;

        public IList<GamePrice> Prices { get; }

        public IList<ItemStore> StoreIds { get; }

        public DateTimeOffset DateLastChecked { get; set; }

        public decimal? LastSalePrice { get; set; }

        public decimal? LastExchangePrice { get; set; }

        public decimal? LastPurchasePrice { get; set; }

        public DateTimeOffset MinPriceDate { get; set; }

        public decimal? MinSalePrice { get; set; }

        public decimal? MinExchangePrice { get; set; }

        public decimal? MinPurchasePrice { get; set; }

        public DateTimeOffset MaxPriceDate { get; set; }

        public decimal? MaxSalePrice { get; set; }

        public decimal? MaxExchangePrice { get; set; }

        public decimal? MaxPurchasePrice { get; set; }

        public bool? GameOwned { get; set; }

    }
}
