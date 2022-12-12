namespace SnykTest;

public class IndirectCaller
{
    private readonly IDataExampleDerivedInterface _provider;

    public IndirectCaller(IDataExampleDerivedInterface provider)
    {
        _provider = provider;
    }

    public async Task ManipulateStringAndCall(string input)
    {
        var parameter = new string(input);
        await _provider.Process(parameter);
    }
}