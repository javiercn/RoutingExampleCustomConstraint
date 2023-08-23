
using System.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("notcontains", typeof(NotContains));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

class NotContains(string text) : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out var routeValue)
            && routeValue != null)
        {
            var parameterValueString = Convert.ToString(routeValue, CultureInfo.InvariantCulture)!;

            var segments = parameterValueString.Split('/');

            foreach (var segment in segments)
            {
                if (string.Equals(segment, text, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
