﻿using Microsoft.Extensions.DependencyInjection;
using System;
using TcpWindowsForm.Config;
using System.Windows.Forms;
using TcpWindowsForm.Actor;
using AkkaSysBase;

namespace TcpWindowsForm
{
    public class Bootstrapper
    {
        private IServiceProvider _provider;

        public Bootstrapper()
        {
            var sysDI = new SysDIService(new ServiceCollection());
            var serviceContainer = sysDI.Inject();
            _provider = serviceContainer.BuildServiceProvider();
        }

        /// <summary>
        /// Star App
        /// </summary>
        public void Run()
        {
            StarFormApp();
        }

        /// <summary>
        /// 啟用WindwForm
        /// </summary>
        private void StarFormApp()
        {
            var form = CreateMainForm();
            form.Shown += (o, e) => StarAkkaSys();
            Application.Run(form);
        }

        private void StarAkkaSys()
        {
            // 建立與外部連結系統
            var akkaSys = _provider.GetService<ISysAkkaManager>();
            akkaSys.CreateActor<Client>();
            akkaSys.CreateActor<Server>();
        }

        private Form CreateMainForm()
        {
            var form = _provider.GetService<Form1>();
            return form;
        }
    }
}
