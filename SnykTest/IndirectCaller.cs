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
    
    public async Task CallDirect(string input)
    {
        var parameter = input;
        await _provider.Process(parameter);
    }
    
    public async Task PassThrough(string input)
    {
        await _provider.Process(input);
    }
}