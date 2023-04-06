using Microsoft.Extensions.DependencyInjection;

namespace MDIForms
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddScoped<FormMain>();
            services.AddScoped<FormChild>();
            services.AddTransient<FormGrandchild>();

            ServiceProvider = services.BuildServiceProvider();

        }   

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            ConfigureServices();
            FormMain FormMain = Program.ServiceProvider.GetService<FormMain>();
            Application.Run(FormMain);
        }
    }
}