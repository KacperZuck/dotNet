using Store.Api.Data;
using Store.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// TU CONFIGURACJA APLIKACJI
builder.Services.AddValidation();

// budowanie kontekstu waze aby przed budowa aplikacji 
builder.AddGameStoreDb();

var app = builder.Build();
app.MapGamesEndPoints();
app.MapTypeEndPoints();

app.MigrateDb();
app.Run();