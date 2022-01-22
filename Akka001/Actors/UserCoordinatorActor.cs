using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka001.Messages;

namespace Akka001.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<Guid, IActorRef> _users = new();

        public UserCoordinatorActor()
        {
            Receive<PlayMovieMessage>(HandlePlayMovieMessage);
            Receive<StopMovieMessage>(HandleStopMovieMessage);
        }

        private void HandleStopMovieMessage(StopMovieMessage stopMovieMessage)
        {
            IActorRef childActor = GetOrCreateChildUser(stopMovieMessage.UserId);
            
            childActor.Tell(stopMovieMessage);
        }

        private void HandlePlayMovieMessage(PlayMovieMessage playMovieMessage)
        {
            IActorRef childActor = GetOrCreateChildUser(playMovieMessage.UserId);

            childActor.Tell(playMovieMessage);
        }

        private IActorRef GetOrCreateChildUser(Guid userId)
        {
            if (_users.ContainsKey(userId))
                return _users[userId];
            
            var newChildActorRef = 
                Context.ActorOf(Props.Create(() => new UserActor(userId)), $"user-{userId}");

            _users.Add(userId, newChildActorRef);
            
            Console.WriteLine($"Creating new user with id: {userId}. Total number of users: {_users.Count}");
            
            return newChildActorRef;
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            Console.WriteLine("UserCoordinator actor pre start");
        }

        protected override void PostStop()
        {
            Console.WriteLine("UserCoordinator actor post stop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine($"UserCoordinator actor pre restart because: {reason.Message}");
            
            base.PreRestart(reason, message);   
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine($"UserCoordinator actor post stop because {reason.Message}");
            
            base.PostRestart(reason);
        }

        #endregion
    }
}