﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PL.Startup))]
namespace PL
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
