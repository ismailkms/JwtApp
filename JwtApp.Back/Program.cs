using JwtApp.Back.Concrete;
using JwtApp.Back.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        //Meta veri adresi veya yetkilisi için HTTPS gerekip gerekmediðini alýr veya ayarlar. Varsayýlan deðer true'dur. Bu yalnýzca geliþtirme ortamlarýnda devre dýþý býrakýlmalýdýr.
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:IssuerSigningKey"]))
        };
    });
//Audience => Oluþturulacak token deðerini kimlerin/hangi originlerin/sitelerin kullanacaðýný belirlediðimiz alandýr. Örneðin; “www.bilmemne.com”
//Issuer => Oluþturulacak token deðerini kimin daðýttýðýný ifade edeceðimiz alandýr. Örneðin; “www.myapi.com”
//LifeTime => Oluþturulan token deðerinin süresini kontrol edecek olan doðrulamadýr.
//SigningKey => Üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden security key verisinin doðrulamasýdýr.
//ClockSkew => Üretilecek token deðerinin expire süresinin belirtildiði deðer kadar uzatýlmasýný saðlayan özelliktir. Örneðin; kullanýlabilirlik süresi 5 dakika olarak ayarlanan token deðerinin ClockSkew deðerine 3 dakika verilirse eðer ilgili token 5 + 3 = 8 dakika kullanýlabilir olacaktýr. Bunun nedeni, aralarýnda zaman farký olan farklý lokasyonlardaki sunucularda yayýn yapan bir uygulama üzerinde elde edilen ortak token deðerinin saati ileride olan sunucuda geçerliliðini daha erken yitirmemesi için ClockSkew propertysi sayesinde aradaki fark kadar zamaný tokena eklememiz gerekmektedir.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
