using Cadastro.API.Mensageria;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<PublicadorRabbit>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapMetrics();
});

app.Run();
