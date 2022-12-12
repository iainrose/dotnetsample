using SnykTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IDataExample, DataExampleSafe>();
builder.Services.AddTransient<IDataExample, DataExampleWithString>();
builder.Services.AddTransient<IDataExample, DataExampleWithStringBuilder>();
builder.Services.AddTransient<IDataExampleDerivedInterface, DataExampleWithString>();
builder.Services.AddTransient<DataExampleBase, DataExampleWithString>();
builder.Services.AddTransient<IndirectCaller>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();