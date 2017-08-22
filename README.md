# RestWell
![logo](./Logo.png)

[![Build Status](https://travis-ci.org/StephenMP/RestWell.svg?branch=master)](https://travis-ci.org/StephenMP/RestWell) [![NuGet](https://img.shields.io/nuget/v/RestWell.svg)](https://www.nuget.org/packages/RestWell/)
[![NuGet downloads](https://img.shields.io/nuget/dt/RestWell.svg)](https://badge.fury.io/nu/RestWell) [![license](https://img.shields.io/github/license/StephenMP/RestWell.svg)]()

RestWell is a simple, easy to use, light weight .NET generic client which provides simple abstraction of the communication layer between your code and RESTful Web APIs. It is written in .NET Core 2.0 and was inspired by the RestSharp project; however, it is different in that it focuses solely on .NET clients and provides simple mechanisms for controlling your request pipeline.

# Main Features
 * Small
 * Lightweight
 * Rediculously easy to use
 * Mockable (easy to test)
 * Injectable (we all love dependendency injection)
 * Configurable
    * You can inject your own delegating handlers into the request pipeline giving you complete control over the flow of your request (e.g. use a delegating handler to obtain an authorization token prior to issuing a request)

# Documentation
Please refer to our [Wiki](https://github.com/StephenMP/RestWell/wiki) for more detailed documentation.

# Install
**Nuget Package Manager:**
```
PM> Install-Package RestWell
```

**.NET Core CLI:**
```
> dotnet add package RestWell
```

# Contributing
If you'd like to contribute, please follow these simple rules

1. Either
    * Submit a new issue outlining what you are going to work on
    * Pick an existing issue to work on
1. Clone the repository
1. Create a branch with the following format: `YourGitHubName/IssueNumber` (e.g. `StephenMP/703`)
1. Make your code changes
1. Ensure all added code is covered by tests (I will scrutinize this like a crazy mad man)
    * Please ensure that you are following the testing pattern. Please reference existing tests to get an idea of the pattern.
1. When committing changes, please follow the commit message format `git commit -m "IssueNumber : Commit Message"` (e.g. `git commit -m "703 : Added tests around Proxy"`)
1. Push your branch up
1. Submit a Pull Request into master from your branch

I'll be honest with you all, I'm going to be Mr. Party Pooper when it comes to contributions. I want to ensure the project is kept clean, is well tested, and follows existing patterns and conventions to make it easier to pick it up and run with.

Thanks and happy coding :)

# Usage
The best way to demonstrate the usage of RestWell is by example! That's why we've created an example project you can take a peek at:
https://github.com/StephenMP/RestWell.ExampleProject
