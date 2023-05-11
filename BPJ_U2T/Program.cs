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

app.UseDefaultFiles(); // ͹حҵ������¡����ҧ� � wwwroot

app.UseStaticFiles(); // ͹حҵ�����Ҷ֧����Ҥ������ 

app.UseRouting();

app.UseCors(CorsInstaller.MyAllowSpecificOrigins);
//��õ�Ǩ�ͺ�Է���
app.UseAuthentication();
//���͹حҵ
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToController("Index", "Fallback"); // �͡��鹷ҧ�ѹ��͹
});

app.Run();