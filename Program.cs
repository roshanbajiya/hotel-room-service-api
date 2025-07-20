using Microsoft.EntityFrameworkCore;
using room_service.Data;

var builder = WebApplication.CreateBuilder(args);
// Configure HTTPS endpoint
// Configure Kestrel for both HTTP and HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(7001); // HTTP
    options.ListenLocalhost(7002, listenOptions =>
    {
        listenOptions.UseHttps();  // HTTPS
    });
});



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}
builder.Services.AddDbContext<RoomDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();
app.MapControllers();

app.Run();
