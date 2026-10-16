using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2;
using KAMA_PRO_CRUD_APP2.classes.DTO;
using KAMA_PRO_CRUD_APP2.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

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


        private string WithToken(string endpoint)
        {
            var token = temp_variables.token;
            if (string.IsNullOrEmpty(token))
                return endpoint;

            var sep = endpoint.Contains('?') ? "&" : "?";
            return $"{endpoint}{sep}token={Uri.EscapeDataString(token)}";
        }

        public async Task<string> CreateAssemblagesAsync(DTO_AddAssemblage request)
        {
            try
            {
                var response = await client.PostAsJsonAsync(BASE_ADDRESS + WithToken("Assemblages"), request);

                if (response.IsSuccessStatusCode)
                {
                    return null; // успех
                }

                // читаем текст ошибки от сервера
                string error = await response.Content.ReadAsStringAsync();

                // сервер может вернуть строку в кавычках (JSON-строка) — убираем кавычки
                if (!string.IsNullOrWhiteSpace(error))
                {
                    error = error.Trim('"');
                }

                return string.IsNullOrWhiteSpace(error)
                    ? $"Ошибка сервера: {response.StatusCode}"
                    : error;
            }
            catch (HttpRequestException ex)
            {
                return $"Не удалось связаться с сервером: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Непредвиденная ошибка: {ex.Message}";
            }
        }

        public async Task<string> DeleteAssemblageById(string vin)
        {
            var response = await client.DeleteAsync(BASE_ADDRESS + WithToken($"Assemblages?vin={vin}"));

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return json;
            }
            return "";
        }

        public async Task CreatePayment(DTO_Payment paymentDto)
        {
            var response = await client.PostAsJsonAsync(BASE_ADDRESS + WithToken($"Payments"), paymentDto);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

            }
        }

        public async Task<List<DTO_Payment>> GetPayment(int AssemblerId)
        {
            var response = await client.GetStringAsync(BASE_ADDRESS + WithToken($"Payments/get?id={AssemblerId}"));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            if (response != null)
            {
                var json = JsonSerializer.Deserialize<List<DTO_Payment>>(response, options);
                return json ?? new List<DTO_Payment>();
            }
            return null;
        }

        public async Task PostPlan(string name, List<Plan_linkto_Trailer> list)
        {
            var response = await client.PostAsJsonAsync(BASE_ADDRESS + WithToken($"Plans?name={name}"), list);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

            }

        }

        public async Task<string> DeleteComponentAsync(string name)
        {
            try
            {
                var url = BASE_ADDRESS
                          + WithToken("Components/delete")
                          + $"&name={Uri.EscapeDataString(name)}";

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                    return null;

                string error = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(error))
                    error = error.Trim('"');

                return string.IsNullOrWhiteSpace(error)
                    ? $"Ошибка сервера: {response.StatusCode}"
                    : error;
            }
            catch (HttpRequestException ex)
            {
                return $"Не удалось связаться с сервером: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Непредвиденная ошибка: {ex.Message}";
            }
        }

        public async Task<string> CreateComponent(Components comp)
        {
            try
            {
                var response = await client.PostAsJsonAsync(
                    BASE_ADDRESS + WithToken($"Components/create"), comp);

                if (response.IsSuccessStatusCode)
                {
                    return null; // успех
                }

                // читаем текст ошибки от сервера
                string error = await response.Content.ReadAsStringAsync();

                // сервер может вернуть строку в кавычках (JSON-строка) — убираем кавычки
                if (!string.IsNullOrWhiteSpace(error))
                {
                    error = error.Trim('"');
                }

                return string.IsNullOrWhiteSpace(error)
                    ? $"Ошибка сервера: {response.StatusCode}"
                    : error;
            }
            catch (HttpRequestException ex)
            {
                return $"Не удалось связаться с сервером: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Непредвиденная ошибка: {ex.Message}";
            }
        }

        public async Task<string> DeletePlanByName(string name)
        {
            try
            {
                var response = await client.DeleteAsync(
                    BASE_ADDRESS + WithToken($"Plans?name={Uri.EscapeDataString(name)}"));

                if (response.IsSuccessStatusCode)
                {
                    return null; // успех
                }

                // читаем текст ошибки от сервера
                string error = await response.Content.ReadAsStringAsync();

                // сервер может вернуть строку в кавычках (JSON-строка) — убираем кавычки
                if (!string.IsNullOrWhiteSpace(error))
                {
                    error = error.Trim('"');
                }

                return string.IsNullOrWhiteSpace(error)
                    ? $"Ошибка сервера: {response.StatusCode}"
                    : error;
            }
            catch (HttpRequestException ex)
            {
                return $"Не удалось связаться с сервером: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Непредвиденная ошибка: {ex.Message}";
            }
        }

        public async Task UpdateAssembler(int id, DTO_Assembler dTO)
        {
            var response = await client.PostAsJsonAsync(BASE_ADDRESS + WithToken($"Assemblers/Update?old_assembler_id={id}"), dTO);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                Assemblers new_assembler = JsonSerializer.Deserialize<Assemblers>(json, options);

            }

        }


        public async Task<DTORETURN> GetSalary(int id)
        {
            var response = await client.GetStringAsync(BASE_ADDRESS + WithToken($"Assemblers/Salary?id={id}"));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true  // Игнорировать регистр
            };

            var json = JsonSerializer.Deserialize<DTORETURN>(response, options);

            if (response != null)
            {
                return json;
            }
            return null;
        }

        public class helpful_class
        {
            public string Trailer_Vin { get; set; }
            public int count { get; set; }
        }


        public async Task<LoginResponse> LoginUser(string username, string pwd)
        {

            DTO_Assembler_Login asDTO = new DTO_Assembler_Login { password = pwd, username = username };

            var response = await client.PostAsJsonAsync(BASE_ADDRESS + "Assemblers/Login", asDTO);


            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                LoginResponse assembler = JsonSerializer.Deserialize<LoginResponse>(json, options);

                return assembler;
            }

            return null;
        }


        public async Task GetConcreteAssemblagesAsync(string date)
        {
            concrete_Assemblage = await GetSmthngAsync<Concrete_Assemblage_Date>("Assemblages/" + date);
        }

        public async Task GetPlan_linkto_TrailerAsync()
        {
            Plan_linkto_Trailer = await GetSmthngAsync<Plan_linkto_Trailer>("Plan_linkto_Trailer");
        }

        public async Task GetAssemblagesAsync()
        {
            Assemblages = await GetSmthngAsync<Assemblages>("Assemblages");
        }
        public async Task CreateAssemblerAsync(DTO_Assembler assembler)
        {
            await CreateSmthngAsync<DTO_Assembler>("Assemblers", assembler);
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
            Components = await GetSmthngAsync<Components>("Components/get");
        }

        public async Task GetPlansAsync()
        {
            Plans = await GetSmthngAsync<Plans>("Plans");
        }

        //http-client
        HttpClient client = new HttpClient();

        //универсальные методы

        public async Task CreateSmthngAsync<T>(string endpoint, T obj)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                await client.PostAsJsonAsync(BASE_ADDRESS + WithToken(endpoint), obj);
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

        public async Task<List<T>> GetSmthngAsync<T>(string endpoint)
        {
            List<T> massive = new List<T>();
            try
            {
                var response = await client.GetAsync(BASE_ADDRESS + WithToken(endpoint));
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
