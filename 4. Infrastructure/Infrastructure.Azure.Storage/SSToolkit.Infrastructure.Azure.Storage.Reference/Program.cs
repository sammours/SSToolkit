#pragma warning disable SA1200 // Using directives should be placed correctly
using SSToolkit.Infrastructure.Azure.Storage;
#pragma warning restore SA1200 // Using directives should be placed correctly

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ============================== Register the Blob storage =================================
var connectionString = builder.Configuration.GetValue(typeof(string), "ConnectionString").ToString() ?? string.Empty;
builder.Services.RegisterAzureBlobStorage(connectionString, "my-container", true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ============================== Minimal APIs =================================
app.MapGet("/blobs/upload/bytes", async (IAzureBlobStorage azureBlobStorage) =>
{
    await azureBlobStorage.UploadAsync($"file.txt", new byte[] { 1, 2, 3 }, shouldReplace: true);
});

app.MapGet("/blobs/upload/stream", async (IAzureBlobStorage azureBlobStorage) =>
{
    await azureBlobStorage.UploadAsync($"file.txt", new MemoryStream(new byte[] { 1, 2, 3 }), shouldReplace: true);
});

app.MapGet("/blobs/download/stream", async (IAzureBlobStorage azureBlobStorage) =>
{
    using var stream = new MemoryStream();
    await azureBlobStorage.DownloadAsync($"file.txt", stream);
    return stream.ToArray();
});

app.MapGet("/blobs/download/bytes", async (IAzureBlobStorage azureBlobStorage) =>
{
    return await azureBlobStorage.DownloadAsync($"file.txt");
});

app.MapGet("/blobs/exists", async (IAzureBlobStorage azureBlobStorage) =>
{
    return await azureBlobStorage.ExistsAsync($"file.txt");
});

app.MapGet("/blobs/copy", async (IAzureBlobStorage azureBlobStorage) =>
{
    await azureBlobStorage.CopyAsync("file.txt", "file1.txt");
    return await azureBlobStorage.ExistsAsync("file.txt") && await azureBlobStorage.ExistsAsync("file1.txt");
});

app.MapGet("/blobs/rename", async (IAzureBlobStorage azureBlobStorage) =>
{
    await azureBlobStorage.RenameAsync("file.txt", "file1.txt");
    return !await azureBlobStorage.ExistsAsync("file.txt") && await azureBlobStorage.ExistsAsync("file1.txt");
});

app.MapGet("/blobs/delete", async (IAzureBlobStorage azureBlobStorage) =>
{
    return await azureBlobStorage.DeleteAsync("file.txt");
});

app.MapGet("/containers/create", async (IAzureBlobStorage azureBlobStorage) =>
{
    // the name provided in options ("my-container") will be internally provided
    return await azureBlobStorage.GetOrCreateContainerAsync("my-container");
});

app.MapGet("/containers/delete", async (IAzureBlobStorage azureBlobStorage) =>
{
    // the name provided in options ("my-container") will be internally provided
    return await azureBlobStorage.DeleteContainerAsync("my-container");
});

app.Run();