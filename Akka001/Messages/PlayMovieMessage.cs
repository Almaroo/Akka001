using System;

namespace Akka001.Messages
{
    public class PlayMovieMessage
    {
        public Guid UserId { get; }
        public string MovieTitle { get; }
        
        public PlayMovieMessage(Guid userId, string movieTitle)
        {
            UserId = userId;
            MovieTitle = movieTitle;
        }
    }
}