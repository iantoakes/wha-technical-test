using Autofac;

namespace RiskManager.DomainLogic
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerRiskService>().AsSelf();
            builder.RegisterType<BettingRiskService>().AsSelf();
            builder.RegisterType<SettledBetsService>().AsSelf();

            base.Load(builder);
        }
    }
}
