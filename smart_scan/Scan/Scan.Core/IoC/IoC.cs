using Ninject;

namespace Scan.App
{
    public static class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
