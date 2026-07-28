using MiProyectoMVC.Data;

// Configura los servicios que usara la app 
// Crear el constructor de la aplicacion
var builder = WebApplication.CreateBuilder(args);

// Añade servicios al contenedor 
builder.Services.AddControllersWithViews(); // Dice que se usara MVC 
builder.Services.AddScoped<ClsAccesoDatos>(); // Permite al controller recibir clsAccesoDatos
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build(); // Construye la app con lo que se configuro arriba

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Registrar}/{id?}")
    .WithStaticAssets();


app.Run();
