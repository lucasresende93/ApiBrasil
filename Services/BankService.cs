using ApiBrasil.Models;
using ApiBrasil.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiBrasil.Services
{
    public class BankService : IBankInterface
    {
        private readonly HttpClient _httpClient;

        public BankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GenericResponse<Banks>> GetBankById(int id)
        {
            var responseAPI = await _httpClient.GetAsync($"https://brasilapi.com.br/api/banks/v1/{id}");

            if (!responseAPI.IsSuccessStatusCode)
            {
                return new GenericResponse<Banks>
                {
                    HttpCode = responseAPI.StatusCode,
                    ErrorMessage = $"Erro ao acessar API: {responseAPI.ReasonPhrase}"
                };
            }

            var contentResponse = await responseAPI.Content.ReadAsStringAsync();         
            var JSONresponse = JsonSerializer.Deserialize<Banks>(contentResponse);
            return new GenericResponse<Banks>
            {
                Data = JSONresponse,
                HttpCode = responseAPI.StatusCode
            };
        }

        public async Task<GenericResponse<IEnumerable<Banks>>> GetAllBanks()
        {

            var responseAPI = await _httpClient.GetAsync("https://brasilapi.com.br/api/banks/v1");

            if (!responseAPI.IsSuccessStatusCode)
            {
                return new GenericResponse<IEnumerable<Banks>>
                {
                    Data = Enumerable.Empty<Banks>(), // Retorna lista vazia em caso de erro
                    HttpCode = responseAPI.StatusCode,
                    ErrorMessage = $"Erro ao acessar API: {responseAPI.ReasonPhrase}"
                };
            }

            var contentResponse = await responseAPI.Content.ReadAsStringAsync();
            var JSONresponse = JsonSerializer.Deserialize<IEnumerable<Banks>>(contentResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? Enumerable.Empty<Banks>();

            return new GenericResponse<IEnumerable<Banks>>
            {
                Data = JSONresponse,
                HttpCode = responseAPI.StatusCode
            };
        }
    }
}
