using MongoDB.Bson;
using Realms;

namespace Gaming.RealmObjects
{
    public partial class Game : IRealmObject
    {
        [MapTo("_id")]
        [PrimaryKey]
        public ObjectId Id { get; set; }
        public ConsoleType? ConsoleId { get; set; }
        public string? GameCoverUrl { get; set; }
        public bool? HasCase { get; set; }
        public bool? HasGame { get; set; }
        public bool? HasManual { get; set; }
        public long? IgdbId { get; set; }
        public string? IgdbUrl { get; set; }
        public bool? IsCollectorsEdition { get; set; }
        public bool? IsCopy { get; set; }
        public bool? IsNonPalRegion { get; set; }
        public DateTimeOffset? LastUpdated { get; set; }
        public string? Name { get; set; }
        public int? OriginalId { get; set; }
        public string? Summary { get; set; }
        public Guid? UserId { get; set; }
        public string? Notes { get; set; }
        public double? AmountPaid { get; set; }
        public CeXGame? CexDetails { get; set; }
    }
}
