using Stocks.Hexagone.Ports;
using Stocks.Hexagone.UseCases.PurchaseOrder.Commands;
using Stocks.Hexagone.UseCases.PurchaseOrder.Queries;
using Stocks.Hexagone.UseCases.Stocks.Commands;
using Stocks.Hexagone.UseCases.Stocks.Queries;
using Stocks.Infrastructure.Mocks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IArticleRepository, MockArticleRepository>();
builder.Services.AddSingleton<IPurchaseOrderRepository, MockCommandsRepository>();
builder.Services.AddSingleton<ITypeArticleRepository, MockTypeArticleRepository>();

// Articles
builder.Services.AddScoped<AddArticleCommandHandler>();
builder.Services.AddScoped<DeleteArticleCommandHandler>();
builder.Services.AddScoped<UpdateArticleCommandHandler>();
builder.Services.AddScoped<GetAllArticlesQueryHandler>();
builder.Services.AddScoped<GetArticleByIntervalQueryHandler>();
builder.Services.AddScoped<GetArticleByNameQueryHandler>();
builder.Services.AddScoped<GetArticleByReferenceQueryHandler>();

// Purchase Order
builder.Services.AddScoped<CreatePurchaseOrderCommandHandler>();
builder.Services.AddScoped<GetAllPurchaseOrdersHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options
.WithOrigins("http://localhost:4500", "https://localhost:7048")
.AllowAnyMethod()
.AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
