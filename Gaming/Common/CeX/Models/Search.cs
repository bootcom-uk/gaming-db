using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CeX.Models
{
    public class Search
    {

        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class AttributeInfo
        {
            [JsonPropertyName("attributeId")]
            public int AttributeId { get; set; }

            [JsonPropertyName("attributeFriendlyName")]
            public string AttributeFriendlyName { get; set; }

            [JsonPropertyName("attributeValuesInfo")]
            public List<AttributeValuesInfo> AttributeValuesInfo { get; set; }
        }

        public class AttributeValuesInfo
        {
            [JsonPropertyName("attributeValue")]
            public string AttributeValue { get; set; }

            [JsonPropertyName("count")]
            public int Count { get; set; }
        }

        public class Box
        {
            [JsonPropertyName("boxId")]
            public string BoxId { get; set; }

            [JsonPropertyName("masterBoxId")]
            public object MasterBoxId { get; set; }

            [JsonPropertyName("boxName")]
            public string BoxName { get; set; }

            [JsonPropertyName("isMasterBox")]
            public int IsMasterBox { get; set; }

            [JsonPropertyName("categoryId")]
            public int CategoryId { get; set; }

            [JsonPropertyName("categoryName")]
            public string CategoryName { get; set; }

            [JsonPropertyName("categoryFriendlyName")]
            public string CategoryFriendlyName { get; set; }

            [JsonPropertyName("superCatId")]
            public int SuperCatId { get; set; }

            [JsonPropertyName("superCatName")]
            public string SuperCatName { get; set; }

            [JsonPropertyName("superCatFriendlyName")]
            public string SuperCatFriendlyName { get; set; }

            [JsonPropertyName("imageUrls")]
            public ImageUrls ImageUrls { get; set; }

            [JsonPropertyName("sellPrice")]
            public double SellPrice { get; set; }

            [JsonPropertyName("firstPrice")]
            public double FirstPrice { get; set; }

            [JsonPropertyName("previousPrice")]
            public double PreviousPrice { get; set; }

            [JsonPropertyName("lastPriceUpdatedDate")]
            public DateTime LastPriceUpdatedDate { get; set; }

            [JsonPropertyName("outOfStock")]
            public int OutOfStock { get; set; }

        }

        public class CategoryFriendlyName
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("count")]
            public int Count { get; set; }
        }

        public class Data
        {
            [JsonPropertyName("boxes")]
            public List<Box> Boxes { get; set; }

        }

        public class Error
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }

            [JsonPropertyName("internal_message")]
            public string InternalMessage { get; set; }

            [JsonPropertyName("moreInfo")]
            public List<object> MoreInfo { get; set; }
        }

        public class Facets
        {
            [JsonPropertyName("superCatName")]
            public List<SuperCatName> SuperCatName { get; set; }

            [JsonPropertyName("categoryFriendlyName")]
            public List<CategoryFriendlyName> CategoryFriendlyName { get; set; }

            [JsonPropertyName("attributeInfo")]
            public List<AttributeInfo> AttributeInfo { get; set; }

            [JsonPropertyName("manufacturerName")]
            public object ManufacturerName { get; set; }

            [JsonPropertyName("networkName")]
            public object NetworkName { get; set; }

            [JsonPropertyName("attributeStructureInfo")]
            public object AttributeStructureInfo { get; set; }
        }

        public class ImageUrls
        {
            [JsonPropertyName("large")]
            public string Large { get; set; }

            [JsonPropertyName("medium")]
            public string Medium { get; set; }

            [JsonPropertyName("small")]
            public string Small { get; set; }

            [JsonPropertyName("masterBoxLarge")]
            public object MasterBoxLarge { get; set; }

            [JsonPropertyName("masterBoxMedium")]
            public object MasterBoxMedium { get; set; }

            [JsonPropertyName("masterBoxSmall")]
            public object MasterBoxSmall { get; set; }
        }

        public class Response
        {
            [JsonPropertyName("ack")]
            public string Ack { get; set; }

            [JsonPropertyName("data")]
            public Data Data { get; set; }

            [JsonPropertyName("error")]
            public Error Error { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("response")]
            public Response Response { get; set; }
        }

        public class SuperCatName
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("count")]
            public int Count { get; set; }
        }



    }
}
