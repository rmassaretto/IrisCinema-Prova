namespace IrisCinema.API.Middleware
{    
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { message = "Internal server error" });
            }
        }
    }
}