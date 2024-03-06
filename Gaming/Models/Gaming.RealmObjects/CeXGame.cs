using MongoDB.Bson;
using Realms;

namespace Gaming.RealmObjects
{
    public partial class CeXGame : IRealmObject
    {
        [MapTo("_id")]
        [PrimaryKey]
        public ObjectId Id { get; set; }
        public string BoxId { get; set; }
        public string CeXCategoryName { get; set; }
        public double? HighestSalePrice { get; set; }
        public DateTimeOffset? HighestSalePriceDate { get; set; }
        public long? IgdbId { get; set; }
        public DateTimeOffset? LastChecked { get; set; }
        public DateTimeOffset? DateAdded { get; set; }
        public double? LastSalePrice { get; set; }
        public double? LowestSalePrice { get; set; }
        public DateTimeOffset? LowestSalePriceDate { get; set; }
        public string Name { get; set; }
        public IList<CeXGame_SellPrices> SellPrices { get; }
    }
}
