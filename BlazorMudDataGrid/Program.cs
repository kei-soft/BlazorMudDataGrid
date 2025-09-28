using System.Data;

using BlazorMudDataGrid.Components;

using Microsoft.Data.Sqlite;

using MudBlazor.Services;

using MudBlazorSample.Components;

namespace BlazorMudDataGrid;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddMudServices();

        // SQLite �����ͺ��̽� ���� �߰�
        builder.Services.AddScoped<IDbConnection>((sp) =>
        {
            // wwwroot ������ �����ͺ��̽� ���� ����
            var dbPath = Path.Combine(builder.Environment.WebRootPath, "products.db");
            return new SqliteConnection($"Data Source={dbPath}");
        });

        var app = builder.Build();

        // SQLite �����ͺ��̽� �ʱ�ȭ
        using (var scope = app.Services.CreateScope())
        {
            using var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
            connection.Open();
            DatabaseInitializer.Initialize(connection);
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
