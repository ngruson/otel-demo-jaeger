// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("Hello, World!");
Console.WriteLine("Let's get 10 catalog items by pressing any key...");
Console.ReadLine();

var activitySource = new ActivitySource("ConsoleApp");
var _ = activitySource.StartActivity("Main");

var httpClient = new HttpClient();
var response = await httpClient.GetAsync("https://localhost:50146/api/v1/catalog/items?PageSize=10&PageIndex=0");
response.EnsureSuccessStatusCode();

Console.WriteLine("Response OK");

