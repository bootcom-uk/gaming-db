using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CeX
{
    public class Search
    {

        public async Task<Models.Game.Root?> Get(string? id)
        {
            if(string.IsNullOrWhiteSpace(id)) return null;

            var client = new HttpClient();
            return await client.GetFromJsonAsync<Models.Game.Root>($"{URLs.ItemUrl.Replace("{itemId}", id)}");
        }

        public async Task<Models.Search.Root?> SearchGames(string searchText, int? consoleId)
        {
            var consoleQueryString = "";
            if(consoleId != null)
            {
                consoleQueryString = $"&categoryId={consoleId}";
            }

            var client = new HttpClient();
            var url = $"{URLs.SearchUrl}{searchText}{consoleQueryString}";
            return await client.GetFromJsonAsync<Models.Search.Root>(url);


        }

        public async Task<Models.Search.Root?> SearchGames(string searchText)
        {
            return await SearchGames(searchText, null);

        }

    }
}
