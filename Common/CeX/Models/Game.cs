using System.Text.Json.Serialization;

namespace CeX.Models
{
    public class Game
    {

        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class AttributeInfo
        {
            [JsonPropertyName("attributeFriendlyName")]
            public string AttributeFriendlyName { get; set; }

            [JsonPropertyName("attributeValue")]
            public List<string> AttributeValue { get; set; }

            [JsonPropertyName("dataTypeId")]
            public string DataTypeId { get; set; }

            [JsonPropertyName("order")]
            public string Order { get; set; }

            [JsonPropertyName("isVariant")]
            public string IsVariant { get; set; }
        }

        public class BoxDetail
        {
            [JsonPropertyName("boxId")]
            public string BoxId { get; set; }

            [JsonPropertyName("boxName")]
            public string BoxName { get; set; }

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

            [JsonPropertyName("cannotBuy")]
            public int CannotBuy { get; set; }

            [JsonPropertyName("isNewBox")]
            public int IsNewBox { get; set; }

            [JsonPropertyName("cashPrice")]
            public double CashPrice { get; set; }

            [JsonPropertyName("exchangePrice")]
            public double ExchangePrice { get; set; }

            [JsonPropertyName("sellPrice")]
            public int SellPrice { get; set; }

            [JsonPropertyName("firstPrice")]
            public int FirstPrice { get; set; }

            [JsonPropertyName("previousPrice")]
            public int PreviousPrice { get; set; }

            [JsonPropertyName("lastPriceUpdatedDate")]
            public string LastPriceUpdatedDate { get; set; }

            [JsonPropertyName("boxRating")]
            public int BoxRating { get; set; }

            [JsonPropertyName("collectionQuantity")]
            public int CollectionQuantity { get; set; }

            [JsonPropertyName("outOfStock")]
            public int OutOfStock { get; set; }

            [JsonPropertyName("ecomQuantityOnHand")]
            public int EcomQuantityOnHand { get; set; }

            [JsonPropertyName("webSellAllowed")]
            public int WebSellAllowed { get; set; }

            [JsonPropertyName("webBuyAllowed")]
            public int WebBuyAllowed { get; set; }

            [JsonPropertyName("webShowSellPrice")]
            public int WebShowSellPrice { get; set; }

            [JsonPropertyName("webShowBuyPrice")]
            public int WebShowBuyPrice { get; set; }

            [JsonPropertyName("boxSaleAllowed")]
            public int BoxSaleAllowed { get; set; }

            [JsonPropertyName("boxBuyAllowed")]
            public int BoxBuyAllowed { get; set; }

            [JsonPropertyName("boxWebSaleAllowed")]
            public int BoxWebSaleAllowed { get; set; }

            [JsonPropertyName("boxWebBuyAllowed")]
            public int BoxWebBuyAllowed { get; set; }

            [JsonPropertyName("imageUrls")]
            public ImageUrls ImageUrls { get; set; }

            [JsonPropertyName("isMasterBox")]
            public int IsMasterBox { get; set; }

            [JsonPropertyName("boxDescription")]
            public string BoxDescription { get; set; }

            [JsonPropertyName("operatorId")]
            public object OperatorId { get; set; }

            [JsonPropertyName("gradeId")]
            public object GradeId { get; set; }

            [JsonPropertyName("productGuide")]
            public ProductGuide ProductGuide { get; set; }

            [JsonPropertyName("boxRatingText")]
            public object BoxRatingText { get; set; }

            [JsonPropertyName("attributeDetails")]
            public object AttributeDetails { get; set; }

            [JsonPropertyName("variantGroupValue")]
            public object VariantGroupValue { get; set; }

            [JsonPropertyName("displayableBoxAttributes")]
            public object DisplayableBoxAttributes { get; set; }

            [JsonPropertyName("attributeInfo")]
            public List<AttributeInfo> AttributeInfo { get; set; }

            [JsonPropertyName("imageNames")]
            public object ImageNames { get; set; }

            [JsonPropertyName("isImageTypeInternal")]
            public object IsImageTypeInternal { get; set; }
        }

        public class Data
        {
            [JsonPropertyName("boxDetails")]
            public List<BoxDetail> BoxDetails { get; set; }

            [JsonPropertyName("masterBoxDetails")]
            public object MasterBoxDetails { get; set; }
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

        public class ProductGuide
        {
            [JsonPropertyName("productLineId")]
            public int ProductLineId { get; set; }

            [JsonPropertyName("productGuideDescription")]
            public string ProductGuideDescription { get; set; }

            [JsonPropertyName("globalGuide")]
            public object GlobalGuide { get; set; }
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



    }
}
