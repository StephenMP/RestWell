# Overview
![logo](./Logo.png)

[![Build Status](https://travis-ci.org/StephenMP/RestWell.svg?branch=master)](https://travis-ci.org/StephenMP/RestWell) [![NuGet](https://img.shields.io/nuget/v/RestWell.svg)](https://www.nuget.org/packages/RestWell/)
[![NuGet downloads](https://img.shields.io/nuget/dt/RestWell.svg)](https://badge.fury.io/nu/RestWell) [![license](https://img.shields.io/github/license/StephenMP/RestWell.svg)]()

RestWell is a simple, easy to use, light weight .NET generic client which provides simple abstraction of the communication layer between your code and RESTful Web APIs. It is written in .NET Core 2.0 and was inspired by the RestSharp project; however, it is different in that it focuses solely on .NET clients and provides simple mechanisms for controlling your request pipeline. RestWell is distributed as a NuGet Package hosted in the Nuget Gallery here.

**Main Features**
 * Small
 * Lightweight
 * Rediculously easy to use
 * Mockable (easy to test)
 * Injectable (we all love dependendency injection)
 * Configurable
    * You can inject your own delegating handlers into the request pipeline giving you complete control over the flow of your request (e.g. use a delegating handler to obtain an authorization token prior to issuing a request)

**Useful Links**
* [Introduction](https://github.com/StephenMP/RestWell.Examples/blob/master/RestWell.Examples.Introduction/Introduction.cs)
* [Documentation](https://github.com/StephenMP/RestWell/wiki)
* [Contributing](https://github.com/StephenMP/RestWell/blob/master/CONTRIBUTING.md)
