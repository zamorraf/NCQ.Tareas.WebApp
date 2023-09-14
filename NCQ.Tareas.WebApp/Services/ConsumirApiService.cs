using Microsoft.Extensions.Options;
using NCQ.Tareas.WebApp.Models;
using NCQ.Tareas.WebApp.Models.ViewModels;
using NCQ.Tareas.WebApp.Services.Interfaces;
using Newtonsoft.Json;
using System.Timers;

namespace NCQ.Tareas.WebApp.Services
{
    public class ConsumirApiService : IConsumirApiService
    {
        private HttpClient _httpClient;
        
        // Ruta de la API en el appsettings.json
        private readonly AppSettings _appSettings;

        public ConsumirApiService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            //_httpClient = new HttpClient();
        }


        public async Task<List<Colaborador>> ObtenerColaboradores()
        {
            List<Colaborador> result = null;
            string url = _appSettings.ApiBaseUrl + "/Colaborador";

            _httpClient = new HttpClient();

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request))
                {
                    using (HttpContent content = response.Content)
                    {
                        // Si la respuesta es 200
                        if (response.IsSuccessStatusCode)
                        {
                            string responseStringContent = await content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<Colaborador>>(responseStringContent);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<Colaborador> ObtenerColaborador(int id)
        {
            Colaborador result = null;
            string url = _appSettings.ApiBaseUrl + $"/Colaborador/{id}";

            _httpClient = new HttpClient();

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request))
                {
                    using (HttpContent content = response.Content)
                    {
                        // Si la respuesta es 200
                        if (response.IsSuccessStatusCode)
                        {
                            string responseStringContent = await content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<Colaborador>(responseStringContent);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<List<Colaborador>> ObtenerColaboradores2()
        {
            List<Colaborador> result = null;
            string url = _appSettings.ApiBaseUrl + "/Colaborador";

            using ( _httpClient = new HttpClient())
            {
                using (var respuesta = await _httpClient.GetAsync(url))
                {
                    switch (respuesta.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            string respuestaStringContenido = await respuesta.Content.ReadAsStringAsync();

                            result = JsonConvert.DeserializeObject<List<Colaborador>>(respuestaStringContenido);
                            break;
                        case System.Net.HttpStatusCode.BadRequest:
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            break;
                        default:
                            break;
                    }
                }
            }

            return result;
        }

        public async Task AgregarColaborador(Colaborador colaborador)
        {
            string url = _appSettings.ApiBaseUrl + "/Colaborador";

            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage respuesta = await _httpClient.PostAsJsonAsync(url, colaborador);

                if (respuesta.IsSuccessStatusCode)
                {
                    var cuerpo = await respuesta.Content.ReadAsStringAsync();
                    Console.WriteLine("El id es: " + cuerpo);
                }
            }
        }

        public async Task ModificarColaborador(Colaborador colaborador)
        {
            string url = _appSettings.ApiBaseUrl + "/Colaborador";

            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage respuesta = await _httpClient.PutAsJsonAsync(url, colaborador);

                if (respuesta.IsSuccessStatusCode)
                {
                    var cuerpo = await respuesta.Content.ReadAsStringAsync();
                    Console.WriteLine("El id es: " + cuerpo);
                }
            }
        }


        public async Task EliminarColaborador(int id)
        {
            string url = _appSettings.ApiBaseUrl + $"/Colaborador/{id}";

            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage respuesta = await _httpClient.DeleteAsync (url);

                if (respuesta.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Colaborador eliminado: {id}");
                }
                else
                { 
                    Console.WriteLine(respuesta.StatusCode.ToString());
                }
            }
        }

        // Tareas
        public async Task<List<Tarea>> ObtenerTareas()
        {
            List<Tarea> result = null;
            string url = _appSettings.ApiBaseUrl + "/Tarea";

            _httpClient = new HttpClient();

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request))
                {
                    using (HttpContent content = response.Content)
                    {
                        // Si la respuesta es 200
                        if (response.IsSuccessStatusCode)
                        {
                            string responseStringContent = await content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<Tarea>>(responseStringContent);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<List<TareaVM>> ObtTareas()
        {
            List<TareaVM> result = null;
            string url = _appSettings.ApiBaseUrl + "/Tarea/SQL";

            _httpClient = new HttpClient();

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request))
                {
                    using (HttpContent content = response.Content)
                    {
                        // Si la respuesta es 200
                        if (response.IsSuccessStatusCode)
                        {
                            string responseStringContent = await content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<TareaVM>>(responseStringContent);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<Tarea> ObtenerTarea(int id)
        {
            Tarea result = null;
            string url = _appSettings.ApiBaseUrl + $"/Tarea/{id}";

            _httpClient = new HttpClient();

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request))
                {
                    using (HttpContent content = response.Content)
                    {
                        // Si la respuesta es 200
                        if (response.IsSuccessStatusCode)
                        {
                            string responseStringContent = await content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<Tarea>(responseStringContent);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<TareaVM> ObtTarea(int id)
        {
            TareaVM result = null;
            string url = _appSettings.ApiBaseUrl + $"/Tarea/SQL/{id}";

            _httpClient = new HttpClient();

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (HttpResponseMessage response = await _httpClient.SendAsync(request))
                {
                    using (HttpContent content = response.Content)
                    {
                        // Si la respuesta es 200
                        if (response.IsSuccessStatusCode)
                        {
                            string responseStringContent = await content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<TareaVM>(responseStringContent);
                        }
                    }
                }
            }

            return result;
        }

        public async Task AgregarTarea(Tarea tarea)
        {
            string url = _appSettings.ApiBaseUrl + "/Tarea";

            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage respuesta = await _httpClient.PostAsJsonAsync(url, tarea);

                if (respuesta.IsSuccessStatusCode)
                {
                    var cuerpo = await respuesta.Content.ReadAsStringAsync();
                    Console.WriteLine("El id es: " + cuerpo);
                }
            }
        }

        public async Task ModificarTarea(Tarea tarea)
        {
            string url = _appSettings.ApiBaseUrl + "/tarea";

            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage respuesta = await _httpClient.PutAsJsonAsync(url, tarea);

                if (respuesta.IsSuccessStatusCode)
                {
                    var cuerpo = await respuesta.Content.ReadAsStringAsync();
                    Console.WriteLine("El id es: " + cuerpo);
                }
            }
        }

        public async Task EliminarTarea(int id)
        {
            string url = _appSettings.ApiBaseUrl + $"/Tarea/{id}";

            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage respuesta = await _httpClient.DeleteAsync(url);

                if (respuesta.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Tarea eliminada: {id}");
                }
                else
                {
                    Console.WriteLine(respuesta.StatusCode.ToString());
                }
            }
        }
    }
}
