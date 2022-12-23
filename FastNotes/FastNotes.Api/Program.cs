using FastNotes.Api.Extensions;
using FastNotes.Domain.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsService();
builder.Services.AddFastNotesWorkerService(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Keys:AuthenticationToken").Value)),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
    //    c =>
    //{
    //    c.RoutePrefix = "";
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");        
    //}
    );
}

app.UseHttpsRedirection();

app.UseCors("corspolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
