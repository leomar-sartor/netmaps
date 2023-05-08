using NetMaps;
using NetMaps.Interfaces;
using NetMaps.Repository;
using NetMaps.Services;

var builder = WebApplication.CreateBuilder(args);

ContextoMongoDb.ConnectionString = builder.Configuration.GetSection("MongoConnection:ConnectionString").Value;
ContextoMongoDb.DatabaseName = builder.Configuration.GetSection("MongoConnection:Database").Value;
ContextoMongoDb.IsSSL = Convert.ToBoolean(builder.Configuration.GetSection("MongoConnection:IsSSL").Value);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<SettingsConfig>(builder.Configuration);
builder.Services.AddScoped<IGasStationAppService, GasStationAppService>();
builder.Services.AddScoped<IGasStationRepository, GasStationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
