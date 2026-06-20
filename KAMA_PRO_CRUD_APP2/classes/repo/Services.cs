using KAMA_PRO_CRUD_APP.classes.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace KAMA_PRO_CRUD_APP2.classes.services
{
    public class Services
    {
        private String BASE_ADDRESS = "https://localhost:7238/api/";
        HttpClient client = new HttpClient();
        public async Task CreateAssemblage(Assemblages assemblage)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                await client.PostAsJsonAsync(BASE_ADDRESS + "/Assemblages", assemblage);
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
        }
    }
}
