var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// HttpClient pour appeler l'API
builder.Services.AddHttpClient("UserAPI", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7104/api/";
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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