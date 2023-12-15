namespace GlobalServiceProvider.Middlewares
{
    public class StartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                App.ServiceProvider = app.ApplicationServices;

                next(app);
            };
        }
    }
}
