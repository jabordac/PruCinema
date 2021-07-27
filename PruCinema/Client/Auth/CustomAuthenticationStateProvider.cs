using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace PruCinema.Client.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        enum StorageType
        {
            token,
            expiry
        }

        string TokenGeneral = "";
        private readonly IJSRuntime jsRuntime;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await GetTokenAsync();
            var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : new ClaimsIdentity(ParseClaimsFromJWT(token), "JWT");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task SetTokenAsync(string token, string expiry)
        {
            Console.WriteLine($"entro a set token {token}");
            if (token == null)
            {
                var a = await jsRuntime.InvokeAsync<object>("RemoveData", StorageType.token);
                var b = await jsRuntime.InvokeAsync<object>("RemoveData", StorageType.expiry);
            }
            else
            {
                Console.WriteLine("token valido");
                TokenGeneral = token;
                var x = await jsRuntime.InvokeAsync<object>("SaveData", StorageType.token, token);//, "token",token
                var y = await jsRuntime.InvokeAsync<object>("SaveData", StorageType.expiry, expiry);
            }
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<string> GetTokenAsync()
        {
            var expiry = await jsRuntime.InvokeAsync<string>("GetData", StorageType.expiry);
            Console.WriteLine($"expiry {expiry}");
            if (expiry != null)
            {
                if (DateTime.Parse(expiry.ToString()) > DateTime.Now)
                {
                    return await jsRuntime.InvokeAsync<string>("GetData", StorageType.token);
                }
                else
                {
                    await SetTokenAsync(null, null);
                }
            }
            return null;
        }

        private IEnumerable<Claim> ParseClaimsFromJWT(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64Withoutpadding(payload);
            var keyValuesPairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            List<Claim> result = new List<Claim>();
            Claim aux;

            foreach (var data in keyValuesPairs)
            {
                aux = new Claim(data.Key, data.Value.ToString());
                result.Add(aux);
            }

            return result;//keyValuesPairs.Select(kv => new Claim(kv.Key,kv.Value.ToString()));
        }

        private byte[] ParseBase64Withoutpadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

    }
}

