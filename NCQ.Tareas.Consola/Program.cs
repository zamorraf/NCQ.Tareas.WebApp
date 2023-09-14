using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace NCQ.Tareas.Consola
{
    internal class Program
    {
        static  async Task Main(string[] args)
        {
            var url = "http://localhost:5285/api/Colaborador";
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetAsync(url);
                var respuestaString = await respuesta.Content.ReadAsStringAsync();

                //var listadoPersonas = JsonSerializer.Deserialize<List<Colaborador>>();            
            }
        }
    }
}
