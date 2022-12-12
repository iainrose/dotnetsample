namespace SnykTest;

public static class DataExampleFactory
{
    public static IDataExample Create<T>() where T : IDataExample, new()
    {
        return new T();
    }
}