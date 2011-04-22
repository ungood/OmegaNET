using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Mvc;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof(Omega.Web.AppStart.Ninject), "Start")]
[assembly: ApplicationShutdownMethod(typeof(Omega.Web.AppStart.Ninject), "Stop")]

namespace Omega.Web.AppStart
{
    public static class Ninject 
	{
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
		{
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            Bootstrapper.Initialize(CreateKernel);
            }
		
        /// <summary>
        /// Stops the application.
        /// </summary>
		public static void Stop()
		{
			Bootstrapper.ShutDown();
		}
		
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Scan(scanner => scanner.BindWithDefaultConventions());
        }		
    }
}
