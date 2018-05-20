# RestWell.Test.Component
## What Is This?
These are the component level tests (unit tests) for the RestWell project. This is where you will test individual components of the RestWell project. These tests should not spin up any resources or require any sort of integration. Any layers requiring layers of integration should be mocked.

* Tests are written in a BDD format
* Behaviors are defined in a Features class (e.g. ProxyFeatures.cs)
    * This is the entry point for the tests
    * Describes the test using BDD style
* The logic for the tests are is defined in a Steps class (e.g. ProxySteps.cs)
    * The Features class uses the Steps class to do the work of the tests

## Example
```csharp
public class SomeClassFeatures
{
    private SomeClassSteps steps;

    public SomeClassFeatures()
    {
        this.steps = new SomeClassSteps();
    }

    [Fact]
    public async Task CanDoSomething()
    {
        this.steps.GivenIHaveSomething();

        await this.steps.WhenIDoSomething();

        this.steps.ThenICanVerifyICanDoSomething();
    }
}

public class SomeClassSteps
{
    internal void GivenIHaveSomething()
    {
        // Logic for this step here
    }

    internal async Task WhenIDoSomething()
    {
        // Logic for this step here
    }

    internal void ThenICanVerifyICanDoSomething()
    {
        // Logic for this step here
    }
}
```