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
        //Meta veri adresi veya yetkilisi i�in HTTPS gerekip gerekmedi�ini al�r veya ayarlar. Varsay�lan de�er true'dur. Bu yaln�zca geli�tirme ortamlar�nda devre d��� b�rak�lmal�d�r.
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
//Audience => Olu�turulacak token de�erini kimlerin/hangi originlerin/sitelerin kullanaca��n� belirledi�imiz aland�r. �rne�in; �www.bilmemne.com�
//Issuer => Olu�turulacak token de�erini kimin da��tt���n� ifade edece�imiz aland�r. �rne�in; �www.myapi.com�
//LifeTime => Olu�turulan token de�erinin s�resini kontrol edecek olan do�rulamad�r.
//SigningKey => �retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden security key verisinin do�rulamas�d�r.
//ClockSkew => �retilecek token de�erinin expire s�resinin belirtildi�i de�er kadar uzat�lmas�n� sa�layan �zelliktir. �rne�in; kullan�labilirlik s�resi 5 dakika olarak ayarlanan token de�erinin ClockSkew de�erine 3 dakika verilirse e�er ilgili token 5 + 3 = 8 dakika kullan�labilir olacakt�r. Bunun nedeni, aralar�nda zaman fark� olan farkl� lokasyonlardaki sunucularda yay�n yapan bir uygulama �zerinde elde edilen ortak token de�erinin saati ileride olan sunucuda ge�erlili�ini daha erken yitirmemesi i�in ClockSkew propertysi sayesinde aradaki fark kadar zaman� tokena eklememiz gerekmektedir.

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
