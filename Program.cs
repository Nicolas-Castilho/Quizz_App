using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adicione os servi�os do Entity Framework
var connectionString = builder.Configuration.GetConnectionString("ApplicationDBcontextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDBcontextConnection' not found.");
builder.Services.AddDbContext<ApplicationDBcontext>(options => options.UseSqlServer(connectionString));

// Adicione os servi�os de autentica��o e autoriza��o
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDBcontext>();

// Adicione os servi�os MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
var env = app.Services.GetRequiredService<IWebHostEnvironment>();

// Configura��o espec�fica com base no ambiente
if (env.IsDevelopment())
{
    // Configura��es espec�ficas para o ambiente de desenvolvimento
}
else
{
    // Configura��es espec�ficas para outros ambientes (produ��o, teste, etc.)
}

// Configurar pipeline de solicita��o HTTP
if (!env.IsDevelopment())
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

app.MapRazorPages();

app.Run();
