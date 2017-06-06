using BLL.Interface.Interfaces;
using BLL.Services;
using DAL;
using DAL.Interface.Interfaces;
using DAL.Interfaces.Interfaces;
using DAL.Repositories;
using Ninject;
using Ninject.Web.Common;
using ORM.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver
{
    public static class DependencyResolver
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel,true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<DbContext>().To<SocialNetworkContext>().InRequestScope();
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            }
            else
            {
                kernel.Bind<DbContext>().To<SocialNetworkContext>().InSingletonScope();
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            }
            
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();

            kernel.Bind<IUserProfileService>().To<UserProfileService>();
            kernel.Bind<IUserProfileRepository>().To<UserProfileRepository>();

            kernel.Bind<IFriendRequestService>().To<FriendRequestService>();
            kernel.Bind<IFriendRequestRepository>().To<FriendRequestRepository>();

            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<IMessageRepository>().To<MessageRepository>();

            kernel.Bind<IPhotoService>().To<PhotoService>();
            kernel.Bind<IPhotoRepository>().To<PhotoRepository>();

        }
    }
}
