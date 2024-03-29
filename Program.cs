using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adicione os serviços do Entity Framework
var connectionString = builder.Configuration.GetConnectionString("ApplicationDBcontextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDBcontextConnection' not found.");
builder.Services.AddDbContext<ApplicationDBcontext>(options => options.UseSqlServer(connectionString));

// Adicione os serviços de autenticação e autorização
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDBcontext>();

// Adicione os serviços MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
var env = app.Services.GetRequiredService<IWebHostEnvironment>();

// Configuração específica com base no ambiente
if (env.IsDevelopment())
{
    // Configurações específicas para o ambiente de desenvolvimento
}
else
{
    // Configurações específicas para outros ambientes (produção, teste, etc.)
}

// Configurar pipeline de solicitação HTTP
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
