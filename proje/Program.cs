using Data.Context;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Session için geçerlidir
builder.Services.AddControllersWithViews();
builder.Services.AddSession(aptimes =>
{
    aptimes.IdleTimeout = TimeSpan.FromSeconds(30);
    aptimes.Cookie.HttpOnly = true;
    aptimes.Cookie.IsEssential = true;


});

builder.Services.AddHttpContextAccessor();
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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");


using (Db db = new Db())
{
}
app.Run();
