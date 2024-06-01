using System;
using TechTalk.SpecFlow;
using RestSharp;
using Newtonsoft.Json;
using BarberiAppNegocio.Models;
using System.Net;

namespace BDDNegocio.StepDefinitions
{
    [Binding]
    public class ServicioStepDefinitions
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private string _token;

        [Given(@"que la API esta disponible para servicio")]
        public void GivenQueLaAPIEstaDisponibleParaServicio()
        {
            _client = new RestClient("https://localhost:7237/");
        }

        [Given(@"tengo un token valido para servicio")]
        public void GivenTengoUnTokenValidoParaServicio()
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

        [When(@"hago una solicitud GET a servicio ""([^""]*)""")]
        public void WhenHagoUnaSolicitudGETAServicio(string resource)
        {
            _request = new RestRequest(resource, Method.Get);
            _request.AddHeader("Authorization", $"Bearer {_token}");
            _response = _client.Execute(_request);
        }

        [Then(@"el código de respuesta para servicio debe ser (.*)")]
        public void ThenElCodigoDeRespuestaParaServicioDebeSer(int expectedStatusCode)
        {
            Assert.Equal(expectedStatusCode, (int)_response.StatusCode);
        }

        [Then(@"la respuesta debe contener una lista de servicio")]
        public void ThenLaRespuestaDebeContenerUnaListaDeServicio()
        {
            var servicios = JsonConvert.DeserializeObject<List<Servicio>>(_response.Content);
            Assert.NotEmpty(servicios);
        }
    }
}
