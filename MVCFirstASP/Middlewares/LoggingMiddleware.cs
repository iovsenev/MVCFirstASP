using MVCFirstASP.Models.DB;
using MVCFirstASP.Models.DB.Repositories;

namespace MVCFirstASP.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly IRequestRepository _repository;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            //_repository = repository;
        }

        public async Task InvokeAsync(HttpContext context, IRequestRepository repository)
        {
            var messageLog = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}\n";

            LogConsole(messageLog);
            await LogDBAsync(context, repository);
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

        private async Task LogDBAsync(HttpContext context, IRequestRepository repository)
        {
            string rul = context.Request.Host.Value + context.Request.Path;
            var request = new Request() { 
                URL = rul,
                Id = Guid.NewGuid(),
                Date = DateTime.Now
            };
            await repository.Add(request);
        }
    }
}
