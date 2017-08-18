# RestWell
RestWell is a simple, easy to use, light weight .NET generic client which provides simple abstraction of the communication layer between your code and RESTful Web APIs. This project was inspired by the RestSharp project; however, it is different in that it focuses solely on .NET clients and provides simple mechanisms for controlling your request pipeline.

# Main Features
 * Small
 * Lightweight
 * Rediculously easy to use
 * Mockabe (easy to test)
 * Injectable (we all love dependendency injection)
 * Configurable
    * You can inject your own delegating handlers into the request pipeline giving you complete control over the flow of your request (e.g. use a delegating handler to obtain an authorization token prior to issuing a request)

# Simple Example
RestWell is rediculously easy to use! Let's say you want to call an API with the following: 
 * The API route is https://www.myapi.com/api/test/[message].
 * The request type is Get
 * The request requires the argument in the path: **message**
 * The request body is a JSON object that looks like the following
    * { "Message": [message] } where [message] is the message you entered as a path argument

In order to use RestWell to call this API with the above setup, first we create a Data Transfer Object (DTO) to represent the JSON object coming back.

```csharp
public class SuperCoolResponseDto {
    public string Message { get; set; }
}
```

Next, we create a ProxyConfiguration which is used by the proxy

```csharp
var proxyConfiguration = new ProxyConfiguration();
```

Next, we create a proxy using the configuration we just created

```csharp
var proxy = new Proxy(proxyConfiguration);
```

Now, we create a ProxyRequest using the ProxyRequestBuilder with the required information

```csharp
var proxyRequest = ProxyRequestBuilder<SuperCoolResponseDto>
                                .CreateBuilder("https://www.myapi.com/api/test")
                                .Accept("application/json")
                                .AddPathArguments("Hello World")
                                .AsGetRequest()
                                .Build();
```

Next, we use the proxy to invoke the request

```csharp
var proxyResponse = await Proxy.InvokeAsync(proxyRequest);
```

Now, we get the response DTO the API returned to us and use it

```csharp
var SuperCoolResponseDto = proxyResponse.ResponseDto;
var message = SuperCoolResponseDto.Message; // message = "Hello World"
```

Put it all together, and it looks like this

```csharp
public class SuperCoolResponseDto {
    public string Message { get; set; }
}

public class ProxyExample {
    public async Task ApiRequestDemo() {
        // Create a configuration for our proxy
        var proxyConfiguration = new ProxyConfiguration();
        
        // Create the proxy
        var proxy = new Proxy(proxyConfiguration);
        
        // Create a proxy request with the necessary information
        var proxyRequest = ProxyRequestBuilder<SuperCoolResponseDto>
                                .CreateBuilder("https://www.myapi.com/api/test")
                                .Accept("application/json")
                                .AddPathArguments("Hello World")
                                .AsGetRequest()
                                .Build();
                                
        // Invoke the request using the Proxy
        var proxyResponse = await Proxy.InvokeAsync(proxyRequest);
        
        // Get our resulting response body
        var SuperCoolResponseDto = proxyResponse.ResponseDto;

        // Now we use it
        var message = SuperCoolResponseDto.Message; // message = "Hello World"
    }
}
```

# An Even Cooler Example
Now let's say we have the same API as above, however, this time it requires you to have some sort of API key in you Authorization header in order to use that. There are three ways in which we can achieve this. The first two ways involve setting the Authorization header when creating the ProxyRequest

```csharp
var proxyRequest = ProxyRequestBuilder<SuperCoolResponseDto>
                        .CreateBuilder("https://www.myapi.com/api/test")
                        // Add Auth Header using Authorization method
                        .Authorization("Bearer", "MySuperCoolApiKey")
                        .Accept("application/json")
                        .AddPathArguments("Hello World")
                        .AsGetRequest()
                        .Build();
```

OR

```csharp
var proxyRequest = ProxyRequestBuilder<SuperCoolResponseDto>
                        .CreateBuilder("https://www.myapi.com/api/test")
                        // Add Auth Header using AddHeader method
                        .AddHeader("Authorization", "Bearer MySuperCoolApiKey")
                        .Accept("application/json")
                        .AddPathArguments("Hello World")
                        .AsGetRequest()
                        .Build();
```

But let's be honest with ourselves. We all HATE passing around magic strings, especially when those magic strings are secrets! We also like to look super cool and use cool programming patterns and what not. So it's highly likelye that we want to reach out to a service of some sort to provide us this information auto-magically on every request... and guess what! We can do this by injecting a delegating handler into the request pipeline!!!

First, we create our delegating handler

```csharp
public class SecureRequestDelegatingHandler : DelegatingHandler
{
    private ServiceToGetApiKey someServiceToGetApiKey;

    public SecureRequestDelegatingHandler()
    {
        // Initialize any services or anything you need here
        this.someServiceToGetApiKey = new ServiceToGetApiKey();
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var scheme = "Bearer"
        var apiKey = this.someServiceToGetApiKey.GetApiKey();
        request.Headers.Authorization = new AuthenticationHeaderValue(scheme, apiKey);

        return await base.SendAsync(request, cancellationToken);
    }
}
```

Next, we create a ProxyConfiguration using the ProxyConfigurationBuilder to inject the delegating handler into the pipeline

```csharp
var proxyConfiguration = ProxyConfigurationBuilder.CreateBuilder()
                                                  .AddDelegatingHandler(new SecureRequestDelegatingHandler())
                                                  .Build()
```

We then use that ProxyConfiguration when creating our Proxy

```csharp
var proxy = new Proxy(proxyConfiguration);
```

And like magic, you've now inserted your own logic into the request pipeline that will retrieve your API key for you and insert it into the Authorization request header for you on every request made by the proxy :) Here's how it looks all together

```csharp
public class SecureRequestDelegatingHandler : DelegatingHandler
{
    private ServiceToGetApiKey someServiceToGetApiKey;

    public SecureRequestDelegatingHandler()
    {
        // Initialize any services or anything you need here
        this.someServiceToGetApiKey = new ServiceToGetApiKey();
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var scheme = "Bearer"
        var apiKey = this.someServiceToGetApiKey.GetApiKey();
        request.Headers.Authorization = new AuthenticationHeaderValue(scheme, apiKey);

        return await base.SendAsync(request, cancellationToken);
    }
}

public class SuperCoolResponseDto {
    public string Message { get; set; }
}

public class ProxyExample {
    public async Task ApiRequestDemo() {
        // Create a configuration for our proxy with our new delegating handler
        var proxyConfiguration = ProxyConfigurationBuilder.CreateBuilder()
                                                          .AddDelegatingHandler(new SecureRequestDelegatingHandler())
                                                          .Build()
        
        // Create the proxy
        var proxy = new Proxy(proxyConfiguration);
        
        // Create a proxy request with the necessary information
        var proxyRequest = ProxyRequestBuilder<SuperCoolResponseDto>
                                .CreateBuilder("https://www.myapi.com/api/test")
                                .Accept("application/json")
                                .AddPathArguments("Hello World")
                                .AsGetRequest()
                                .Build();
                                
        // Invoke the request using the Proxy
        var proxyResponse = await Proxy.InvokeAsync(proxyRequest);
        
        // Get our resulting response body
        var SuperCoolResponseDto = proxyResponse.ResponseDto;

        // Now we use it
        var message = SuperCoolResponseDto.Message; // message = "Hello World"
    }
}
```