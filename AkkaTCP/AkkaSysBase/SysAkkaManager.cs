using Akka.Actor;
using Akka.DI.Core;
using System;
using System.Collections.Generic;

namespace AkkaSysBase.Base
{
    public class SysAkkaManager : ISysAkkaManager
    {
        public ActorSystem ActorSystem { get; set; }

        private readonly Dictionary<string, IActorRef> _actorDics = new Dictionary<string, IActorRef>();

        public SysAkkaManager(ActorSystem actorSystem)
        {
            ActorSystem = actorSystem;
        }

        // Create

        public IActorRef CreateActor<T>() where T : ActorBase
        {
            return CreateActor<T>(() => ActorSystem);
        }

        public IActorRef CreateChildActor<T>(IUntypedActorContext context) where T : ActorBase
        {
            return CreateActor<T>(() => context);
        }

        private IActorRef CreateActor<T>(Func<IActorRefFactory> func) where T : ActorBase
        {
            var actName = typeof(T).Name;
            if (_actorDics.ContainsKey(actName)) return _actorDics[actName];
            return RegisterActor(actName, func().ActorOf(ActorSystem.DI().Props<T>(), typeof(T).Name));
        }

        private IActorRef RegisterActor(string actName, IActorRef actor)
        {
            if (_actorDics.ContainsKey(actName)) throw new ArgumentException($"You have been register Action {actName}");
            _actorDics.Add(actName, actor);
            return actor;
        }

        // Get
        public IActorRef GetActor(string actName)
        {
            if (!_actorDics.ContainsKey(actName)) throw new ArgumentException($"It't doesn't has register Action {actName}");
            return _actorDics[actName];
        }

        public ActorSelection GetActorSelection(string actorPath)
        {
            return ActorSystem.ActorSelection(actorPath);
        }

    }
}
