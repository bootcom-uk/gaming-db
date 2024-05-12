namespace CeX
{
    internal static class URLs
    {

        internal static string ItemUrl { get; } = "https://wss2.cex.uk.webuy.io/v3/boxes/{itemId}/detail";

        internal static string SearchUrl { get; } = "https://wss2.cex.uk.webuy.io/v3/boxes?sortBy=sellprice&sortOrder=asc&firstRecord=1&count=50&q=";

        internal static string StoresUrl { get; } = "https://wss2.cex.uk.webuy.io/v3/stores";

        internal static string ItemStockUrl { get; } = "https://wss2.cex.uk.webuy.io/v3/boxes/{itemId}/neareststores?latitude=53.72579575&longitude=-1.54389787";

    }
}
