using AplikasiAbsensi.Core.Services;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Jalankan menu di thread terpisah
Task.Run(() =>
{
    Menu menu = new Menu();
    menu.TampilkanMenu();
});

app.Run(); // Web API tetap berjalan
