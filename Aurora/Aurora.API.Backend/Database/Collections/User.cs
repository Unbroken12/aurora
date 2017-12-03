using AspNetCore.Identity.MongoDB;
using MongoDB.Bson.Serialization.Attributes;

namespace Aurora.API.Backend.Database.Collections
{
    [BsonIgnoreExtraElements]
    public class User : MongoIdentityUser
    {
        public User(string userName, string email) : base(userName, email)
        {
        }
    }
}
