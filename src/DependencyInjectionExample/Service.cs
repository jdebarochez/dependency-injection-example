namespace DependencyInjectionExample
{
    public class Service : IService
    {
        private readonly ILogger _logger;
        public Service(ILogger logger)
        {
            _logger = logger;
        }

        public void Run() {
            _logger.Write("Hello world!");
        }
    }
}
