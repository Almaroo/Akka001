using System;
using Akka.Actor;
using Akka001.Actors;
using Akka001.Messages;

ActorSystem movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
Console.WriteLine("Actor system created");

Props playbackActorProps = Props.Create<PlaybackActor>();

IActorRef playbackActorRef = movieStreamingActorSystem.ActorOf(playbackActorProps, "Playback");

ActorSelection userCoordinatorActorSelection = movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator");

var user1Id = Guid.NewGuid();
var user2Id = Guid.NewGuid();

userCoordinatorActorSelection.Tell(new StopMovieMessage(user1Id));
userCoordinatorActorSelection.Tell(new PlayMovieMessage(user1Id, "LotR: FotR"));
userCoordinatorActorSelection.Tell(new PlayMovieMessage(user1Id, "LotR: TTT"));

userCoordinatorActorSelection.Tell(new StopMovieMessage(user1Id));
userCoordinatorActorSelection.Tell(new PlayMovieMessage(user2Id, "LotR: RotK"));
userCoordinatorActorSelection.Tell(new StopMovieMessage(user2Id));
userCoordinatorActorSelection.Tell(new PlayMovieMessage(user2Id, "LotR: FotR"));

Console.ReadKey();

await movieStreamingActorSystem.Terminate();
Console.WriteLine("Actor system terminated");