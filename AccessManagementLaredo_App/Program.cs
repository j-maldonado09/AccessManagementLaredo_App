using AccessManagementLaredo_App.Data;
using AccessManagementLaredo_App.Models;
using AccessManagementLaredo_App.Services;
using DataTier;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
	options =>
	{
		options.SignIn.RequireConfirmedAccount = true;
		options.SignIn.RequireConfirmedEmail = true;

	})
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddKendo();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<Microsoft.Data.SqlClient.SqlConnection>();

//builder.Services.AddTransient<AccessManagementLaredo.IPermitRequestResidentialRepository, AccessManagementLaredo.PermitRequestResidentialRepository>();
builder.Services.AddTransient<AccessManagementLaredo.IPermitRequestRepository, AccessManagementLaredo.PermitRequestRepository>();
builder.Services.AddTransient<AccessManagementLaredo.IHighwayRepository, AccessManagementLaredo.HighwayRepository>();
builder.Services.AddTransient<AccessManagementLaredo.IHighwayPrefixRepository, AccessManagementLaredo.HighwayPrefixRepository>();
builder.Services.AddTransient<AccessManagementLaredo.IConstructionTypeRepository, AccessManagementLaredo.ConstructionTypeRepository>();
builder.Services.AddTransient<AccessManagementLaredo.IPermitEventRepository, AccessManagementLaredo.PermitEventRepository>();
builder.Services.AddTransient<AccessManagementLaredo.IAttachmentRepository, AccessManagementLaredo.AttachmentRepository>();
builder.Services.AddTransient<AccessManagementLaredo.IAttachmentTypeRepository, AccessManagementLaredo.AttachmentTypeRepository>();
builder.Services.AddTransient<AccessManagementLaredo.ICountyRepository, AccessManagementLaredo.CountyRepository>();
builder.Services.AddTransient<AccessManagementLaredo.HelperModels.PermitEventHelperModel>();
builder.Services.AddTransient<AccessManagementLaredo.HelperModels.PermitRequestStatusHelperModel>();

builder.Services.AddTransient<ExportPDF>();
builder.Services.AddTransient<EMailSender>();
builder.Services.AddTransient<IdentityServices>();

builder.Services.AddTransient<DataTier.Tools>();
builder.Services.AddSingleton<DataTier.Interfaces.IUnitOfWork, DataTier.UnitOfWorkSQLServer_MSFT>();

QuestPDF.Settings.License = LicenseType.Community;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
