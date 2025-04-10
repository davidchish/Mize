using Excercise2;

var client = RestClientFactory.CreateHttpClient()
    .WithBaseUrl("https://jsonplaceholder.typicode.com")
    .WithHeader("Accept", "application/json");

var request = client.CreateRequest("posts/1");

var result = await request.GetAsync();
Console.WriteLine(result);

