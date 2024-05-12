using MongoDB.Bson;
using Realms;

namespace Gaming.RealmObjects
{
    public partial class UserDetails : IRealmObject
    {
        [MapTo("_id")]
        [PrimaryKey]
        public ObjectId Id { get; set; }

        public Guid? UserId { get; set; }

        public string? CeXMemberId {  get; set; }

        public string? CeXMemberUsername {  get; set; }

        public string? CeXMemberPassword { get; set; }

    }
}
