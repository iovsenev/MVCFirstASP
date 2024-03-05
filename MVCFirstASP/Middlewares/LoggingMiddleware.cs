namespace MVCFirstASP.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var messageLog = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}\n";

            LogConsole(messageLog);
            await LogFileAsync(messageLog);

            await _next.Invoke(context);
        }

        private void LogConsole(string message)
        {
            Console.WriteLine(message);
        }

        private async Task LogFileAsync(string message)
        {
            var pathFileLog = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
            await File.AppendAllTextAsync(pathFileLog, message);
        }
    }
}
