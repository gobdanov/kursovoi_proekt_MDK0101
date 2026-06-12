using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes;

namespace KAMA_PRO_CRUD_APP2.classes.repo
{
    public class Repository
    {
        public List<Assemblages> Assemblages { get; set; }
        public List<Assemblers> Assemblers { get; set; }
        public List<Component_linkto_Trailer> Component_linkto_Trailer { get; set; }
        public List<Components> Components { get; set; }
        public List<Packs> Packs { get; set; }
        public List<Plan_linkto_Trailer> Plan_linkto_Trailer { get; set; }
        public List<Plans> Plans { get; set; }
        public List<Trailers> Trailers { get; set; }

        //для конкретной сборки, не является частью бд
        public List<Concrete_Assemblage_Date> concrete_Assemblage;



        public static String BASE_ADDRESS = "https://localhost:7238/api/";

        public async Task GetConcreteAssemblagesAsync(string date)
        {
            concrete_Assemblage = await GetSmthngAsync<Concrete_Assemblage_Date>("Assemblages/"+date);
        }

        public async Task GetAssemblagesAsync()
        {
            Assemblages = await GetSmthngAsync<Assemblages>("Assemblages");
        }
        public async Task GetAssemblersAsync()
        {
            Assemblers = await GetSmthngAsync<Assemblers>("Assemblers");
        }

        public async Task GetTrailersAsync()
        {
            Trailers = await GetSmthngAsync<Trailers>("Trailers");
        }

        public async Task GetComponentsAsync()
        {
            Components = await GetSmthngAsync<Components>("Components");
        }

        public async Task GetPlansAsync()
        {
            Plans = await GetSmthngAsync<Plans>("Plans");
        }
        
        //http-client
        HttpClient client = new HttpClient();

        //универсальный метод
        public async Task<List<T>> GetSmthngAsync<T>(string endpoint)
        {
            List<T> massive = new List<T>();
            try
            {
                
                var response = await client.GetAsync(BASE_ADDRESS + endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var apiMassive = JsonSerializer.Deserialize<List<T>>(json, options);

                    massive = apiMassive;

                    return massive;
                }
            }
            catch (HttpIOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (JsonException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return massive;
        }
    }
}
