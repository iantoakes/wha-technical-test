using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace RiskManager.Repository
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SettledBetRepository>().As<ISettledBetRepository>();
            builder.RegisterType<UnsettledBetRepository>().As<IUnsettledBetRepository>();

            base.Load(builder);
        }
    }
}
