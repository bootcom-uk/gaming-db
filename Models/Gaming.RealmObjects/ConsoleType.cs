using MongoDB.Bson;
using Realms;
using System.ComponentModel.DataAnnotations;

namespace Gaming.RealmObjects
{
    public partial class ConsoleType : IRealmObject
    {

        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; }

        public string? Name { get; set; }

        public string? Manufacturer { get; set; }

        public string? ImageUrl { get; set; }

        public int IgdbId { get; set; }

        public DateTimeOffset? IgdbLastUpdated { get; set; }

        public string? Checksum { get; set; }

        public string? Slug { get; set; }

        public string? AlternativeName { get; set; }

        public DateTimeOffset? LastUpdated { get; set; }

        public bool Enabled { get; set; }

        public int? OriginalId { get; set; }

        public int? GameCount { get; set; }

        public int? CeXConsoleId { get; set; }
    }
}
