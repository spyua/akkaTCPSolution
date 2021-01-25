using Akka.Actor;

namespace AkkaSysBase
{
    public interface ISysAkkaManager
    {
        ActorSystem ActorSystem { get; }

        IActorRef CreateActor<T>() where T : ActorBase;

        IActorRef CreateChildActor<T>(IUntypedActorContext context) where T : ActorBase;

        IActorRef GetActor(string actName);

        ActorSelection GetActorSelection(string actorPath);
    }
}
