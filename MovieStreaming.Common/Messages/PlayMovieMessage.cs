namespace MovieStreaming.Common.Messages;

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