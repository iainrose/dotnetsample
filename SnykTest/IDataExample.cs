namespace SnykTest;

public interface IDataExample
{
    public Task Process(string name);
}

public interface IDataExampleDerivedInterface : IDataExample
{
    
}

class DataExampleSafe : IDataExampleDerivedInterface
{
    public Task Process(string name)
    {
        return Task.CompletedTask;
    }
}