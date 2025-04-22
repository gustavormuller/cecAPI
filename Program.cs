using cecAPI.Mapeamento;
using cecAPI.Profiles;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using ISession = NHibernate.ISession;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ProdutoProfile));

builder.Services.AddScoped<ISessionFactory>(factory =>
{
    string connectionString = builder.Configuration.GetConnectionString("MySql");
    return Fluently.Configure()
    .Database(MySQLConfiguration.Standard.ConnectionString(connectionString)
    .FormatSql()
    .ShowSql())
    .Mappings(x =>
    {
        x.FluentMappings.AddFromAssemblyOf<ProdutosMap>();
    }).BuildSessionFactory();
});

builder.Services.AddScoped<ISession>(factory =>
{
    return factory.GetService<ISessionFactory>().OpenSession();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
