using System.Net;
using RestSharp;
using Newtonsoft.Json;
using BarberiAppNegocio.Models;

namespace BDDNegocio.StepDefinitions
{
    [Binding]
    public class BarberiaStepDefinitions
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private string _token;


        [Given(@"que la API está disponible")]
        public void GivenQueLaAPIEstaDisponible()
        {
            _client = new RestClient("https://localhost:7237/");
        }

        [Given(@"tengo un token válido")]
        public void GivenTengoUnTokenValido()
        {
            var authClient = new RestClient("https://localhost:7025");
            var authRequest = new RestRequest("/api/token", Method.Post);

            var body = new
            {

                usuarioID = 0,
                email = "string",
                alias = "adminPlat",
                contraseña = "1234",
                rolId = 2

            };

            // Serializar el cuerpo como JSON
            authRequest.AddJsonBody(body);

            var authResponse = authClient.Execute(authRequest);

            if (authResponse.StatusCode == HttpStatusCode.OK)
            {
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(authResponse.Content);
                _token = tokenResponse.AccessToken;
            }
            else
            {
                throw new Exception("Failed to obtain token");
            }
        }

        [When(@"hago una solicitud GET a ""([^""]*)""")]
        public void WhenHagoUnaSolicitudGETA(string resource)
        {
            _request = new RestRequest(resource, Method.Get);
            _request.AddHeader("Authorization", $"Bearer {_token}");
            _response = _client.Execute(_request);
        }

        [Then(@"el código de respuesta debe ser (.*)")]
        public void ThenElCodigoDeRespuestaDebeSer(int expectedStatusCode)
        {
            Assert.Equal(expectedStatusCode, (int)_response.StatusCode);
        }

        [Then(@"la respuesta debe contener una lista de barberias")]
        public void ThenLaRespuestaDebeContenerUnaListaDeBarberias()
        {
            var barberias = JsonConvert.DeserializeObject<List<Barberia>>(_response.Content);
            Assert.NotEmpty(barberias);
        }
    }

    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
