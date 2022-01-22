using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka001.Messages;

namespace Akka001.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts = new();

        public MoviePlayCounterActor()
        {
            Receive<IncrementPlayCountMessage>(HandleIncrementPlayCountMessage);
        }

        private void HandleIncrementPlayCountMessage(IncrementPlayCountMessage incrementPlayCountMessage)
        {
            if (!_moviePlayCounts.ContainsKey(incrementPlayCountMessage.MovieTitle))
            {
                _moviePlayCounts.Add(incrementPlayCountMessage.MovieTitle, 1);
            }
            else
            {
                _moviePlayCounts[incrementPlayCountMessage.MovieTitle] += 1;
            }

            Console.WriteLine($"Movie {incrementPlayCountMessage.MovieTitle} has been watched: {_moviePlayCounts[incrementPlayCountMessage.MovieTitle]} times so far");
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            Console.WriteLine("PlaybackStatistics actor pre start");
        }

        protected override void PostStop()
        {
            Console.WriteLine("PlaybackStatistics actor post stop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine($"PlaybackStatistics actor pre restart because: {reason.Message}");
            
            base.PreRestart(reason, message);   
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine($"PlaybackStatistics actor post stop because {reason.Message}");
            
            base.PostRestart(reason);
        }

        #endregion
    }
}