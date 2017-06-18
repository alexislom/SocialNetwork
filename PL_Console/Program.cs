using System;
using System.Linq;
using BLL.Interface.Interfaces;
using DependencyResolver;
using Ninject;

namespace PL_Console
{
    class Program
    {
        public static readonly IKernel Resolver;

        static Program()
        {
            Resolver = new StandardKernel();
            Resolver.ConfigurateResolverConsole();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Users");
            var service = Resolver.Get<IUserService>();
            var list = service.GetAllByPredicate(x => x.Id <5 ).ToList();
            foreach (var user in list)
            {
                Console.WriteLine($"{user.UserName}" + Environment.NewLine);
            }
        }
    }
}
