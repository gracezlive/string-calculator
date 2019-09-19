
namespace StringCalculator.Dependencies
{
    public class Resolver
    {
        private Container _container;

        /// <summary>
        /// Instantiates types of components in the container.
        /// </summary>
        /// <param name="container"></param>
        public Resolver(Container container)
        {
            _container = container;
            _container.CreateInstances();
        }

        /// <summary>
        /// Returns an object instance by type of interface.
        /// </summary>
        /// <typeparam name="I">Type of interface</typeparam>
        /// <returns></returns>
        public object Resolve<I>()
        {
            return _container.GetInstance<I>();
        }
    }
}
