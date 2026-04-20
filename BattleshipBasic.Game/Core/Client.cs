using Microsoft.AspNetCore.SignalR.Client;
using Ratelite;

namespace BattleshipBasic.Core;

public static class Client
{
	private static HubConnection? hub;
	public static event Action<string> onPlayerJoined = delegate { };
	public static event Action<string, string> onMessageReceived = delegate { };
	
	public static void Connection()
	{
		hub = new HubConnectionBuilder()
			  .WithUrl("https://localhost:7135/room")
			  .WithAutomaticReconnect()
			  .Build();
		
		hub.On<string>(
			"PlayerJoined",
			id =>
			{
				onPlayerJoined(id);
				Log.Write($"Player joined : {id}", Log.Level.Info);
			}
		);
		hub.On<string, string>(
			"Message",
			(id, message) =>
			{
				onMessageReceived(id, message);
				Log.Write($"{id} : {message}", Log.Level.Info);
			}
		);
		hub.StartAsync().Wait();
		hub.InvokeAsync("JoinRoom", "room-1").Wait();
	}
	
	public static void SendMessage(string message)
	{
		hub?.InvokeAsync("SendMessage", "room-1", message).Wait();
	}
}