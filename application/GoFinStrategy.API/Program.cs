using GoFinStrategy.API.Filters;
using GoFinStrategy.API.Middlewares;
using GoFinStrategy.IoC;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

string GetPathOfXmlFromAssembly() => Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers(options => options.Filters.Add<NotificationFilter>())
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        })
        .ConfigureApiBehaviorOptions(options => options.SuppressMapClientErrors = true);

    services.AddRouting(options => options.LowercaseUrls = true);
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
        options.IncludeXmlComments(GetPathOfXmlFromAssembly());
    });

    services.AddCors();

    NativeInjectorBootStrapper.AddDependencyInjection(services);
}


void Configure(WebApplication app, IConfiguration configuration)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(config =>
    {
        config.AllowAnyHeader();
        config.AllowAnyMethod();
        config.AllowCredentials();
        config.WithOrigins(configuration.GetSection("CorsHosts").Get<string[]>());
    });

    //app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseModelValidationMiddleware();

    app.MapControllers();

    app.Run();
}


ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app, builder.Configuration);
