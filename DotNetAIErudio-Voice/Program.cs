using DotNetAIErudio_Voice.Extensions;
using DotNetAIErudio_Voice.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddOpenAI();

builder.Services.AddSingleton<TranscriptionService>();

builder.Services.AddCors(opt => opt.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}
));

builder.Services.AddControllers();
builder.Services.AddOpenApi(opt =>
{
    opt.AddDocumentTransformer((document, context, _) =>
    {
        document.Info = new()
        {
            Title = ".NET AI Erudio API",
            Version = "v1",
            Description = "This API provides AI-based features such as chat, image generation, recipe creations and audio transcription.",
            Contact = new()
            {
                Name = "Jean Connory",
                Email = "michaelrhcp@gmail.com",
                Url = new Uri("https://connorysoftware.com.br")
            },
            License = new()
            {
                Name = "Apache 2 License",
                Url = new Uri("https://connorysoftware.com.br")
            },
            TermsOfService = new Uri("https://connorysoftware.com.br")
        };
        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.Title = ".NET AI Erudio API";
        opt.Theme = ScalarTheme.Default;
        opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
