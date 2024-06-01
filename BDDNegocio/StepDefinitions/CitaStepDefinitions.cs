using System.Net;
using RestSharp;
using Newtonsoft.Json;
using BarberiAppNegocio.Models;
using NuGet.Frameworks;

namespace BDDNegocio.StepDefinitions
{
    [Binding]
    public class CitaStepDefinitions
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private string _token;

        [Given(@"que la API está disponible para citas")]
        public void GivenQueLaAPIEstaDisponibleParaCitas()
        {
            _client = new RestClient("https://localhost:7237/");
        }

        [Given(@"tengo un token válido para citas")]
        public void GivenTengoUnTokenValidoParaCitas()
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

        [When(@"hago una solicitud GET a citas ""([^""]*)""")]
        public void WhenHagoUnaSolicitudGETACitas(string resource)
        {
            _request = new RestRequest(resource, Method.Get);
            _request.AddHeader("Authorization", $"Bearer {_token}");
            _response = _client.Execute(_request);
        }

        [Then(@"el código de respuesta citas debe ser (.*)")]
        public void ThenElCodigoDeRespuestaCitasDebeSer(int expectedStatusCode)
        {
            Assert.Equal(expectedStatusCode, (int)_response.StatusCode);
        }

        [Then(@"la respuesta debe contener la fecha ""([^""]*)""")]
        public void ThenLaRespuestaDebeContenerLaFecha(string fecha)
        {
            var cita = JsonConvert.DeserializeObject<Cita>(_response.Content);
            Assert.Equal(fecha, cita.Fecha.ToString());
        }

        [Then(@"la respuesta debe contener el hora ""([^""]*)""")]
        public void ThenLaRespuestaDebeContenerElHora(string hora)
        {
            var cita = JsonConvert.DeserializeObject<Cita>(_response.Content);
            Assert.Equal(hora, cita.Hora);
        }

        [Then(@"la respuesta debe contener el estado ""([^""]*)""")]
        public void ThenLaRespuestaDebeContenerElEstado(string estado)
        {
            var cita = JsonConvert.DeserializeObject<Cita>(_response.Content);
            Assert.Equal(estado, cita.Estado);
        }

        [Then(@"la la respuesta debe contener el cliente_id (.*)")]
        public void ThenLaLaRespuestaDebeContenerElCliente_Id(int clienteId)
        {
            var cita = JsonConvert.DeserializeObject<Cita>(_response.Content);
            Assert.Equal(clienteId, cita.ClienteId);
        }

        [Then(@"la la respuesta debe contener el barberia_id (.*)")]
        public void ThenLaLaRespuestaDebeContenerElBarberia_Id(int barberiaId)
        {
            var cita = JsonConvert.DeserializeObject<Cita>(_response.Content);
            Assert.Equal(barberiaId, cita.BarberiaId);
        }
    }

}
