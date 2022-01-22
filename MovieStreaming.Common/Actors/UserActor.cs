using Akka.Actor;
using Akka001.Common.Messages;
using MovieStreaming.Common.Messages;

namespace MovieStreaming.Common.Actors;

public class UserActor : ReceiveActor
{
    private string? _currentlyWatching;
    private Guid _id;
    public UserActor(Guid id)
    {
        _id = id;
        Stopped();
    }

    private void Stopped()
    {
        Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
        Receive<StopMovieMessage>(_ => Console.WriteLine($"UserActor ({_id}) Nothing is currently playing..."));

        Console.WriteLine($"UserActor ({_id}) is becoming stopped...");
    }

    private void Playing()
    {
        Receive<PlayMovieMessage>(_ => Console.WriteLine($"UserActor ({_id}) Cannot start: Already playing {_currentlyWatching}"));
        Receive<StopMovieMessage>(_ => StopPlayingMovie());

        Console.WriteLine($"UserActor ({_id}) is playing...");
    }

    private void StartPlayingMovie(string messageMovieTitle)
    {
        _currentlyWatching = messageMovieTitle;
        Console.WriteLine($"UserActor ({_id}) is currently watching: {messageMovieTitle}");

        Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter").Tell(new IncrementPlayCountMessage(messageMovieTitle));
            
        Become(Playing);
    }

    private void StopPlayingMovie()
    {
        _currentlyWatching = null;
            
        Become(Stopped);
    }

    protected override void PreStart()
    {
        Console.WriteLine("User actor pre start");
    }

    protected override void PostStop()
    {
        Console.WriteLine("User actor post stop");
    }

    protected override void PreRestart(Exception reason, object message)
    {
        Console.WriteLine($"User actor pre restart because: {reason.Message}");
            
        base.PreRestart(reason, message);   
    }

    protected override void PostRestart(Exception reason)
    {
        Console.WriteLine($"User actor post stop because {reason.Message}");
            
        base.PostRestart(reason);
    }
}