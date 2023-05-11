using Autofac;
using Autofac.Extensions.DependencyInjection;
using BPJ_U2T.Installers;
using BPJ_U2T.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.MyInstallerExtensions(builder);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles(); // อนุญาตให้เรียกไฟล์ต่างๆ ใน wwwroot

app.UseStaticFiles(); // อนุญาตให้เข้าถึงไฟล์ค่าคงที่ได้ 

app.UseRouting();

app.UseCors(CorsInstaller.MyAllowSpecificOrigins);
//การตรวจสอบสิทธิ์
app.UseAuthentication();
//การอนุญาต
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToController("Index", "Fallback"); // บอกเส้นทางมันก่อน
});

app.Run();