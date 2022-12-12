using Microsoft.AspNetCore.Mvc;

namespace SnykTest.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IEnumerable<IDataExample> _dataProviders;
    private readonly IDataExampleDerivedInterface _singleProvider;
    private readonly DataExampleBase _baseClassProvider;
    private readonly IndirectCaller _indirectCaller;

    public LoginController(
            IEnumerable<IDataExample> dataProviders, 
            IDataExampleDerivedInterface singleProvider, 
            DataExampleBase baseClassProvider,
            IndirectCaller indirectCaller)
    {
        _dataProviders = dataProviders;
        _singleProvider = singleProvider;
        _baseClassProvider = baseClassProvider;
        _indirectCaller = indirectCaller;
    }
    
    // not flagged
    [HttpPost]
    public async Task<IActionResult> Login(string name, string password)
    {
        foreach (var provider in _dataProviders)
        {
            await provider.Process(name);
        }

        return new OkResult();
    }
    
    // flagged
    [HttpPost]
    public async Task<IActionResult> LoginStringBuilderDirect(string name, string password)
    {
        var provider = new DataExampleWithStringBuilder();
        await provider.Process(name);
        
        return new OkResult();
    }
    
    [HttpPost]
    public async Task<IActionResult> LoginSafeDirect(string name, string password)
    {
        var provider = new DataExampleSafe();
        await provider.Process(name);
        
        return new OkResult();
    }
    
    // flagged
    [HttpPost]
    public async Task<IActionResult> LoginStringDirect(string name, string password)
    {
        var provider = new DataExampleWithString();
        await provider.Process(name);
        
        return new OkResult();
    }
    
    [HttpPost]
    public async Task<IActionResult> LoginStringBuilderIndirect(string name, string password)
    {
        var provider = (IDataExample)new DataExampleWithStringBuilder();
        await provider.Process(name);
        
        return new OkResult();
    }
    
    [HttpPost]
    public async Task<IActionResult> LoginSafeIndirect(string name, string password)
    {
        var provider = (IDataExample)new DataExampleSafe();
        await provider.Process(name);
        
        return new OkResult();
    }
    
    // not flagged
    [HttpPost]
    public async Task<IActionResult> LoginStringIndirect(string name, string password)
    {
        var provider = (IDataExample)new DataExampleWithString();
        await provider.Process(name);
        
        return new OkResult();
    }
    
    // not flagged
    [HttpPost]
    public async Task<IActionResult> LoginStringBuilderFactory(string name, string password)
    {
        var provider = DataExampleFactory.Create<DataExampleWithStringBuilder>();
        await provider.Process(name);
        
        return new OkResult();
    }

    // not flagged
    [HttpPost]
    public async Task<IActionResult> LoginStringFactory(string name, string password)
    {
        var provider = DataExampleFactory.Create<DataExampleWithString>();
        await provider.Process(name);
        
        return new OkResult();
    }
    
    // flagged
    [HttpPost]
    public async Task<IActionResult> LoginStringBuilderBaseClass(string name, string password)
    {
        DataExampleBase provider = new DataExampleWithStringBuilder();

        await provider.Process(name);
        
        return new OkResult();
    }
    
    
    // flagged
    [HttpPost]
    public async Task<IActionResult> LoginStringBaseClass(string name, string password)
    {
        DataExampleBase provider = new DataExampleWithString();
        await provider.Process(name);
        
        return new OkResult();
    }
    
    // flagged
    [HttpPost]
    public async Task<IActionResult> LoginStringSimpleInterfaceDependencyInjection(string name, string password)
    {
        await _singleProvider.Process(name);
        
        return new OkResult();
    }
    
    // flagged
    [HttpPost]
    public async Task<IActionResult> LoginStringSimpleBaseClassDependencyInjection(string name, string password)
    {
        await _baseClassProvider.Process(name);
        
        return new OkResult();
    }
    
    // not flagged
    [HttpPost]
    public async Task<IActionResult> LoginStringIndirectCall(string name, string password)
    {
        await _indirectCaller.ManipulateStringAndCall(name);
        
        return new OkResult();
    }
}