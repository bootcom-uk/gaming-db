using MongoDB.Bson;
using Realms;
using System.ComponentModel;

namespace Gaming.RealmObjects
{
    public partial class CeXWishlist : IRealmObject
    {

        [MapTo("_id")]
        [PrimaryKey]
        public ObjectId Id { get; set; }

        public CeXGame? BoxId { get; set; }

        public Guid? UserId { get; set; }

        public bool Active { get; set; }

        public string? CoverUrl { get; set; }

    }
}
