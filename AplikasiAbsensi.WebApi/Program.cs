using AplikasiAbsensi.Core.Services;

var argsList = args.Select(a => a.ToLower()).ToList();

if (argsList.Contains("cli"))
{
    // Mode CLI
    Menu menu = new Menu();
    menu.TampilkanMenu();
}
else
{
    // Mode Web API
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
    app.Run();
}
