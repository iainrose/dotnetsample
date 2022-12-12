namespace SnykTest;

public class DataExampleBase : IDataExampleDerivedInterface
{
    public virtual Task Process(string name)
    {
        throw new NotImplementedException();
    }
}