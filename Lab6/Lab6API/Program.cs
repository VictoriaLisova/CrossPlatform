using Lab6API.Data;
using Lab6API.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Configuration.GetValue<string>("Provider");

// connect to db provider 
if (provider == "psql")
{
    builder.Services.AddDbContext<PostgreSqlDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"))
    );
    builder.Services.AddScoped<IAPIDbContext, PostgreSqlDbContext>();
}
else if (provider == "mssql")
{
    builder.Services.AddDbContext<APIDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MSSqlConnection"))
    );
    builder.Services.AddScoped<IAPIDbContext, APIDbContext>();
}
else if (provider == "sqlite")
{
    builder.Services.AddDbContext<SqliteDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteConnection"))
    );
    builder.Services.AddScoped<IAPIDbContext, SqliteDbContext>();
}

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddMvc();

var app = builder.Build();

// initialize db by provider
if(provider == "mssql")
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetService<APIDbContext>();
    await DbInitialize.InitializeStaffCategories(context);
    await DbInitialize.InitializeRoles(context);
    await DbInitialize.InitializeStaff(context);
    await DbInitialize.InitializePatients(context);
    await DbInitialize.InitializeStaffPatientAssociatios(context);
    await DbInitialize.InitializeRecordComponents(context);
    await DbInitialize.InitializePatientRecords(context);
    await DbInitialize.InitializeAppointmentStatusCodes(context);
    await DbInitialize.InitializeAppointments(context);
}
else if(provider == "sqlite")
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetService<SqliteDbContext>();
    await DbInitialize.InitializeStaffCategories(context);
    await DbInitialize.InitializeRoles(context);
    await DbInitialize.InitializeStaff(context);
    await DbInitialize.InitializePatients(context);
    await DbInitialize.InitializeStaffPatientAssociatios(context);
    await DbInitialize.InitializeRecordComponents(context);
    await DbInitialize.InitializePatientRecords(context);
    await DbInitialize.InitializeAppointmentStatusCodes(context);
    await DbInitialize.InitializeAppointments(context);
}
else if(provider == "psql")
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetService<PostgreSqlDbContext>();
    await DbInitialize.InitializeStaffCategories(context);
    await DbInitialize.InitializeRoles(context);
    await DbInitialize.InitializeStaff(context);
    await DbInitialize.InitializePatients(context);
    await DbInitialize.InitializeStaffPatientAssociatios(context);
    await DbInitialize.InitializeRecordComponents(context);
    await DbInitialize.InitializePatientRecords(context);
    await DbInitialize.InitializeAppointmentStatusCodes(context);
    await DbInitialize.InitializeAppointments(context);
}
else
{
    using var scope = app.Services.CreateScope();
    //var context = scope.ServiceProvider.GetService<InMemoryDbContext>();
    var options = new DbContextOptionsBuilder<InMemoryDbContext>()
        .UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemoryConnection"))
        .Options;
    var context = new InMemoryDbContext(options);
    await DbInitialize.InitializeStaffCategories(context);
    await DbInitialize.InitializeRoles(context);
    await DbInitialize.InitializeStaff(context);
    await DbInitialize.InitializePatients(context);
    await DbInitialize.InitializeStaffPatientAssociatios(context);
    await DbInitialize.InitializeRecordComponents(context);
    await DbInitialize.InitializePatientRecords(context);
    await DbInitialize.InitializeAppointmentStatusCodes(context);
    await DbInitialize.InitializeAppointments(context);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();

app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
