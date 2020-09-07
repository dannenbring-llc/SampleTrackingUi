using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SampleTrackingApi
    {
        static HttpClient client = new HttpClient();
        private static string _baseAddress;

        public SampleTrackingApi(IConfiguration configration)
        {
            _baseAddress = configration.GetSection("Api").GetSection("BaseAddress").Value;
        }

        public static async Task<List<UserDto>> GetUsers()
        {

            List<UserDto> usersDto = null;
            HttpResponseMessage response = await client.GetAsync($"{_baseAddress}Administration/Users");
            if (response.IsSuccessStatusCode)
            {
                usersDto = await response.Content.ReadAsAsync<List<UserDto>>();
            }
            return usersDto;
        }
    }
}
