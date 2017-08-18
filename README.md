# RestWell
RestWell is a simple, easy to use, light weight .NET generic client which provides simple abstraction of the communication layer between clients and RESTful Web APIs. This project was inspired by the RestSharp project; however, it is different in that it focuses solely on .NET clients and provides simple mechanisms for controlling your request pipeline.

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
public class TestResponseDto {
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
var proxyRequest = new ProxyRequestBuilder<TestResponseDto>("https://www.myapi.com/api/test")
                                .AddHeader("Accept", "application/json")
                                .AddPathArguments("Hello World")
                                .SetRequestType(HttpRequestMethod.Get)
                                .Build();
```

Next, we use the proxy to invoke the request

```csharp
var proxyResponse = await Proxy.InvokeAsync(proxyRequest);
```

Now, we get the response DTO the API returned to us and use it

```csharp
var testResponseDto = proxyResponse.ResponseDto;
var message = testResponseDto.Message; // message = "Hello World"
```

Put it all together, and it looks like this

```csharp
public class TestResponseDto {
    public string Message { get; set; }
}

public class ProxyExample {
    public async Task ApiRequestDemo() {
        // Create a configuration for our proxy
        var proxyConfiguration = new ProxyConfiguration();
        
        // Create the proxy
        var proxy = new Proxy(proxyConfiguration);
        
        // Create a proxy request with the necessary information
        var proxyRequest = new ProxyRequestBuilder<TestResponseDto>("https://www.myapi.com/api/test")
                                .AddHeader("Accept", "application/json")
                                .AddPathArguments("Hello World")
                                .SetRequestType(HttpRequestMethod.Get)
                                .Build();
                                
        // Invoke the request using the Proxy
        var proxyResponse = await Proxy.InvokeAsync(proxyRequest);
        
        // Get our resulting response body
        var testResponseDto = proxyResponse.ResponseDto;

        // Now we use it
        var message = testResponseDto.Message; // message = "Hello World"
    }
}
```