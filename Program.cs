var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Assuming you have "appsettings.json" with the necessary keys
var configuration = builder.Configuration;
builder.Services.AddSingleton<Kernel>(sp => {
    var kernelBuilder = Kernel.CreateBuilder();

    // Add OpenAI services to the kernel
    kernelBuilder.AddAzureOpenAITextToImage(
        configuration["OpenAI:DalleDeployment"],
        configuration["OpenAI:Endpoint"],
        configuration["OpenAI:ApiKey"]);

    kernelBuilder.AddAzureOpenAIChatCompletion(
        configuration["OpenAI:GptDeployment"],
        configuration["OpenAI:Endpoint"],
        configuration["OpenAI:ApiKey"]);

    return kernelBuilder.Build();
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
