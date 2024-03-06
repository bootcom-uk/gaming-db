using MongoDB.Bson.Serialization.Attributes;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming.RealmObjects
{
    public partial class ItemStore : IEmbeddedObject
    {

        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTimeOffset DateLastChecked { get; set; }

        public int StoreId { get; set; }

        public string? StoreName { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

    }
}
