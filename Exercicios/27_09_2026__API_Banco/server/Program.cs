using BancoAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;
using DotNetEnv;
using BancoAPI.Domains;

var builder = WebApplication.CreateBuilder(args);

//
// Carregando string de conexao da env
Env.Load();

// Pegando a string de conexao
var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

// Conectando com o banco
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
                                                            connectionString,
                                                            npgsqloptions => {
                                                                npgsqloptions.MapEnum<tipo_usuario_enum>(
                                                                    "tipo_usuario_enum",
                                                                    "banco");

                                                                npgsqloptions.MapEnum<tipo_transferencia_enum>(
                                                                    "tipo_transferencia_enum",
                                                                    "banco");

                                                                npgsqloptions.MapEnum<status_movimentacao_enum>(
                                                                    "status_movimentacao_enum",
                                                                    "banco");

                                                                npgsqloptions.MapEnum<tipo_alteracao_enum>(
                                                                    "tipo_alteracao_enum",
                                                                    "banco");

                                                                npgsqloptions.MapEnum<tipo_deposito_enum>(
                                                                    "tipo_deposito_enum",
                                                                    "banco");

                                                                npgsqloptions.MapEnum<tipo_movimentacao_enum>(
                                                                    "tipo_movimentacao_enum",
                                                                    "banco");

                                                                npgsqloptions.MapEnum<tipo_pagamento_enum>(
                                                                    "tipo_pagamento_enum",
                                                                    "banco");

                                                                npgsqloptions.MapEnum<tipo_saque_enum>(
                                                                    "tipo_saque_enum",
                                                                    "banco");

                                                            }));

builder.Services.AddControllers();

// Configurando o start do Swagger
// O AddSwaggerGen vem do pacote Swashbukle.AspNetCore
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>{
        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Value = Bearer TokenJWT"
        };

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes.Add("Bearer", securityScheme);

        document.Security ??= new List<OpenApiSecurityRequirement>();

        var requirements = new OpenApiSecurityRequirement
        {
            {new OpenApiSecuritySchemeReference("Bearer", document), new List<string>()}
        };

        document.Security.Add(requirements);

        return Task.CompletedTask;
    });
});


// Configuração da Autenticação JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["Jwt:Key"]!;
        var issuer = builder.Configuration["Jwt:Issuer"]!;
        var audience = builder.Configuration["Jwt:Audience"]!;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
