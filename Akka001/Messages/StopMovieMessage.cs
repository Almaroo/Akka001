using System;

namespace Akka001.Messages
{
    public class StopMovieMessage
    {
        public Guid UserId { get; }
        
        public StopMovieMessage(Guid userId)
        {
            UserId = userId;
        }
    }
}