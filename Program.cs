// 1. Создать ветку "di" на ветке "init"
// 2. Создать базовый Repository для основной сущности проекта
// 3. Создать 2 реализации на базе Repository выше (1 - JSON, 2 - SQL (Dapper))
// 4. Написать CRUD операции в Controller-е
// 5. Необходимо применить Dependency Injection
// 6. Необходимо добавить контракт и проверки для BadRequest статуса
// 7. Запушить изменения
// 8. Открыть Pull Request, добавить меня в reviewers, отправить только(!) ссылку в комментарии(!) к домашнему заданию

using POS_system.Repositories.Base;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IProductRepository, ProductSqlRepository>();

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;


var app = builder.Build();
app.UseAuthorization();

    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapControllers();

app.Run();