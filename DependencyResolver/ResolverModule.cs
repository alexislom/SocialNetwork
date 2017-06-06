using Ninject.Modules;
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
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<SocialNetworkContext>().InRequestScope();

            //Bind<IunitOfWorkContext>().To<unitOfWorkContext>().InRequestScope();
            //Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            //Bind<IUserProfileRepository>().To<UserProfileRepository>().InRequestScope();
            //Bind<IRoleRepository>().To<RoleRepository>().InRequestScope();
            //Bind<IMessageRepository>().To<MessageRepository>().InRequestScope();
            //Bind<IFriendRepository>().To<FriendRepository>().InRequestScope();

            //Bind<IUserService>().To<UserService>();
            //Bind<IUserProfileService>().To<UserProfileService>();
            //Bind<IRoleService>().To<RoleService>();
            //Bind<IMessageService>().To<MessageService>();
            //Bind<IFriendService>().To<FriendService>();
            //Bind<ISearchService>().To<SearchService>();
        }
    }
}
