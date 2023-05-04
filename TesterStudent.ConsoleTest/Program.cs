// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Text;
using System.Text.Json;

Console.WriteLine("Hello, World!");
var container = new CookieContainer();
HttpClientHandler handler = new HttpClientHandler()
{
    CookieContainer = container
};
HttpClient client = new HttpClient(handler)
{
    BaseAddress = new Uri("https://localhost:7145/")
};
HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/Account");
string jsonData = JsonSerializer.Serialize(new LoginViewModel() {Username = "GachiTeacher", Password = "03032002MNV"});
using var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
request.Content = content;
var response = await client.SendAsync(request);

if (response.IsSuccessStatusCode)
{
    response.EnsureSuccessStatusCode();

    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
}

public class LoginViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}