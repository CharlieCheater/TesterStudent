using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Newtonsoft.Json;
using TesterStudent.Client.Enums;
using TesterStudent.Client.Models;
using TesterStudent.Client.Utils;

namespace TesterStudent.Client.Domain;

public class ApiClient
{
    public HttpClient Client { get; init; }
    public string BaseAddress { get; init; }
    public ApiClient(string baseAddress)
    {
        HttpClientHandler handler = new HttpClientHandler()
        {
            CookieContainer = new CookieContainer()
        };
        BaseAddress = baseAddress;
        Client = new HttpClient(handler)
        {
            BaseAddress = new Uri(BaseAddress)
        };
    }
    public async Task<Result<T>> CallAsync<T>(HttpMethodTypes httpMethod, string apiMethod, params Parameter[] parameters)
    {
        string endpointParams = "";
        if(parameters != null && parameters.Count() > 0)
        {
            endpointParams = "?" + string.Join('&', parameters.Select(p => p.GetFullValue()));
        }

        var endpoint = BaseAddress + apiMethod + endpointParams;
        var request = new HttpRequestMessage(Utilities.GetHttpMethod(httpMethod), endpoint);
        var response = await Client.SendAsync(request);
        return await SendAsync<T>(response);
    }
    public async Task<Result<T>> CallAsync<T>(HttpMethodTypes httpMethod, string apiMethod, object data)
    {
        var endpoint = BaseAddress + apiMethod;
        using var request = new HttpRequestMessage(Utilities.GetHttpMethod(httpMethod), endpoint);
        var jsonData = JsonConvert.SerializeObject(data);

        using var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        request.Content = content;
        var response = await Client.SendAsync(request);
        return await SendAsync<T>(response);
    }
    private async Task<Result<T>> SendAsync<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Result<T>>(result);
        }
        throw new Exception(response.StatusCode.ToString());
    }
}