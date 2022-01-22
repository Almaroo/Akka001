// See https://aka.ms/new-console-template for more information

using Akka.Actor;
using Akka.Configuration;

using var movieStreamingActorSystem = ActorSystem.Create(
    "MovieStreamingActorSystem",
    ConfigurationFactory.ParseString(File.ReadAllText("../../../app.conf")
    ));
Console.WriteLine("MovieStreamingActorSystem created in remote process");


Console.ReadKey();

Console.WriteLine("Actor system terminated");