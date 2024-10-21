using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

 // Redirigir las solicitudes relacionadas con vuelos
         app.Map("/flights/{**path}", async context =>
         {
             var client = new HttpClient();
             var targetUrl = "http://localhost:5001/api/flight/" + context.Request.Path.ToString().Replace("/flights/", "");
             
             if (context.Request.Method == HttpMethods.Get)
             {
                 var response = await client.GetAsync(targetUrl);
                 var content = await response.Content.ReadAsStringAsync();
                 await context.Response.WriteAsync(content);
             }
             else if (context.Request.Method == HttpMethods.Post)
             {
                 var response = await client.PostAsync(targetUrl, new StringContent(await new StreamReader(context.Request.Body).ReadToEndAsync(), Encoding.UTF8, "application/json"));
                 var content = await response.Content.ReadAsStringAsync();
                 await context.Response.WriteAsync(content);
             }
         });

         // Redirigir las solicitudes relacionadas con pasajeros
         app.Map("/passengers/{**path}", async context =>
         {
             var client = new HttpClient();
             var targetUrl = "http://localhost:5002/api/passenger/" + context.Request.Path.ToString().Replace("/passengers/", "");
             
             if (context.Request.Method == HttpMethods.Get)
             {
                 var response = await client.GetAsync(targetUrl);
                 var content = await response.Content.ReadAsStringAsync();
                 await context.Response.WriteAsync(content);
             }
             else if (context.Request.Method == HttpMethods.Post)
             {
                 var response = await client.PostAsync(targetUrl, new StringContent(await new StreamReader(context.Request.Body).ReadToEndAsync(), Encoding.UTF8, "application/json"));
                 var content = await response.Content.ReadAsStringAsync();
                 await context.Response.WriteAsync(content);
             }
         });


app.Run();

