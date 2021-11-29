using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.Domain.Models.IndexTypes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Calculator.UsersAPI.Service
{
    public class UserService : IUserService
    {
        public async Task<DisallowedSet> GetDisallowedUsers(UsersIndexType index)
        {
            using (HttpClient client = new HttpClient())
            {
                string uri = "http://webtask.future-processing.com:8069/" + GetUriSuffix(index);
                HttpResponseMessage response = await client.GetAsync(uri);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                DisallowedSet disallowedSet = JsonConvert.DeserializeObject<DisallowedSet>(jsonResponse);
                return disallowedSet;
            }
        }

        private static string GetUriSuffix(UsersIndexType index)
        {
            switch (index)
            {
                case UsersIndexType.Blocked:
                    return "Blocked";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
