using AplikasiAbsensi.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<LoginService>();

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

if (!app.Environment.IsProduction())
{
    Task.Run(() =>
    {
        MenuService menu = new MenuService();
        menu.TampilkanMenu();
    });
}


app.Run(); 
