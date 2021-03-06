﻿using Akka.Actor;
using Akka.DI.Extensions.DependencyInjection;
using Akka.Event;
using AkkaSysBase;
using AkkaSysBase.Base;
using LogSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using TcpWindowsForm.Actor;

namespace TcpWindowsForm.Config
{
    public class SysDIService
    {

        private readonly IServiceCollection _service;

        public SysDIService(IServiceCollection service)
        {
            _service = service;

        }

        public IServiceCollection Inject()
        {
            _service.AddSingleton<AppSetting>();
            _service.AddSingleton<Form1>(); 
            _service.AddSingleton<ISysAkkaManager>(p =>
            {
                var appsetting = p.GetService<AppSetting>();
                var actSystem = ActorSystem.Create(appsetting.AkkaSysName, AkkaPara.Config("8999"));
                actSystem.UseServiceProvider(p);
                return new SysAkkaManager(actSystem);
            });
            _service.AddScoped(p =>
            {
                var appsetting = p.GetService<AppSetting>();
                var ipPoint = new SysIP
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(appsetting.LocalIp), appsetting.LocalPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(appsetting.RemoteIp), appsetting.RemotePort)
                };

                return new Client(ipPoint, GetLog<Client>(p, a => a.ClientLog));
            });

            _service.AddScoped(p =>
            {
                var appsetting = p.GetService<AppSetting>();
                var ipPoint = new SysIP
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(appsetting.LocalIp), appsetting.LocalPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(appsetting.RemoteIp), appsetting.RemotePort)
                };

                return new Server(ipPoint, GetLog<Server>(p, a => a.ServerLog));
            });

            return _service;
        }

        private ILog GetLog<T>(IServiceProvider context, Func<AppSetting, string> func)
        {
            return new NLogSend(Logging.GetLogger(context.GetService<ISysAkkaManager>().ActorSystem, func?.Invoke(context.GetService<AppSetting>())));
        }
    }
}
