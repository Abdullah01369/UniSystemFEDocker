using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using ServiceLayer.AbstractsModel;
using ServiceLayer.DbConnection;
using ServiceLayer.Services;
using ServiceLayer.Services.AdminServices;
using ServiceLayer.Services.ResearcherServices;
using SharedLayer.Extentions;
using SharedLayer.Models;
using System;
using System.Configuration;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.Json;
using UniSystemFE.Areas.Academician.Models;
using UniSystemFE.Options;

var builder = WebApplication.CreateBuilder(args);

#region HuggingFace

builder.Services.AddHttpClient("HuggingFaceAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api-inference.huggingface.co");
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {builder.Configuration["HuggingFace:APIKey"]}");
});
builder.Services.AddDirectoryBrowser();







#endregion






builder.Services.AddHttpContextAccessor();

#region httpsconn
builder.Services.AddDbContext<DbContextModel>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(DbContextModel)).GetName().Name);
    });
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpClient<IProductService, ProductService>(opt =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    opt.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient<IProjectService, ProjectService>(opt =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    opt.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient<IAnnouncementService, AnnouncementService>(opt =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    opt.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient<ICourseServices, CourseServices>(opt =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    opt.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient<IMessagesServices, MessageServices>(opt =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    opt.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient<IAcademicianService, AcademicianService>(opt =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    opt.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient<IAcademicYearServices, AcademicYearServices>(opt =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    opt.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<IResearcherService, ResearcherService>(opt =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    opt.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<IAccountServices, AccountService>(opt =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    Console.WriteLine($"BaseUrl: {baseUrl}"); // Debug amaçlý ekleme
    if (string.IsNullOrEmpty(baseUrl))
    {
        throw new InvalidOperationException("BaseUrl is not configured.");
    }
    opt.BaseAddress = new Uri(baseUrl);
});

#endregion
 

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpClient<ILessonServices, LessonServices>(opt =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    opt.BaseAddress = new Uri(baseUrl);
});
 
 
 
var app = builder.Build();


#region index.html - style.css
app.UseDefaultFiles();
app.UseStaticFiles();
#endregion

#region wwwroot/audio_outputs
string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "audio_outputs");
if (!Directory.Exists(webRootPath))
    Directory.CreateDirectory(webRootPath);

IFileProvider fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "audio_outputs"));

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = "/audio_outputs"
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = fileProvider,
    RequestPath = "/audio_outputs"
});
#endregion



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
 
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Student",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Student" });
app.MapControllerRoute(
    name: "Academician",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Student" });
app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Admin" });
app.MapControllerRoute(
    name: "AcademicianInformationSystem",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "AcademicianInformationSystem" });




app.MapPost("/text-to-speech", async (IHttpClientFactory httpClientFactory, HttpContext httpContext) =>
{
    try
    {
        HttpClient httpClient = httpClientFactory.CreateClient("HuggingFaceAPI");

        using StreamReader streamReader = new StreamReader(httpContext.Request.Body);
        string requestBody = await streamReader.ReadToEndAsync();
        TextToSpeechRequest request = JsonSerializer.Deserialize<TextToSpeechRequest>(requestBody)!;

        var requestData = new
        {
            inputs = request.Text
        };

        StringContent stringContent = new(
            JsonSerializer.Serialize(requestData),
            Encoding.UTF8,
            MediaTypeNames.Application.Json
            );

        Console.WriteLine("Ses oluþturuluyor...");

        HttpResponseMessage response = await httpClient.PostAsync("models/facebook/mms-tts-tur", stringContent);
        if (response.IsSuccessStatusCode)
        {
            byte[] audioBytes = await response.Content.ReadAsByteArrayAsync();
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "audio_outputs", $"output_{DateTime.Now:yyyyMMddHHmmss}.wav");

            //Ses dosyasý kaydediliyor
            File.WriteAllBytes(outputPath, audioBytes);
            httpContext.Response.ContentType = "audio/wav";
            await httpContext.Response.Body.WriteAsync(audioBytes);
        }
        else
        {
            string error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Hatasý\t: {response.StatusCode}");
            Console.WriteLine($"Hata Detayý\t: {error}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hata oluþtu\t: {ex.Message}");
    }
});
app.MapPost("/speech-to-text", async (IHttpClientFactory httpClientFactory, HttpContext httpContext) =>
{
    try
    {
        IFormCollection form = await httpContext.Request.ReadFormAsync();
        IFormFile? file = form.Files.GetFile("audio");

        if (file is { Length: 0 } or null)
        {
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsJsonAsync(new { Error = "Ses dosyasý bulunamadý" });
            return;
        }

        using MemoryStream memoryStream = new();
        await file.CopyToAsync(memoryStream);
        byte[] audioBytes = memoryStream.ToArray();

        HttpClient httpClient = httpClientFactory.CreateClient("HuggingFaceAPI");

        ByteArrayContent byteArrayContent = new(audioBytes);
        byteArrayContent.Headers.Add(HeaderNames.ContentType, "audio/wav");

        //Whisper small modeli kullanýlýyor | Hýzlý ve güvenilir!
        HttpResponseMessage response = await httpClient.PostAsync("models/openai/whisper-large-v3-turbo", byteArrayContent);
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"API Yanýtý\t: {responseContent}");
        if (!response.IsSuccessStatusCode)
        {
            httpContext.Response.StatusCode = (int)response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(new { Error = $"API Hatasý\t:{responseContent}" });
            return;
        }

        //Whisper modeli için özel yanýt formatý ayarlanýyor
        Dictionary<string, string>? result = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);
        string? text = result?.GetValueOrDefault("text", "");

        if (string.IsNullOrEmpty(text))
        {
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(new { Error = "Metin çevrilemedi!" });
            return;
        }

        await httpContext.Response.WriteAsJsonAsync(new { Text = text });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hata\t: {ex.Message}");
        httpContext.Response.StatusCode = 500;
        await httpContext.Response.WriteAsJsonAsync(new { Error = $"Sunucu hatasý\t: {ex.Message}" });
    }
});
app.Run();
