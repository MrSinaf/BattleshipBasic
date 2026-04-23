using BattleshipBasic.Shared;
using Microsoft.AspNetCore.SignalR.Client;

namespace BattleshipBasic.Core;

public static class Client
{
	public static HubConnection hub = null!;
	public static string? roomCode;
	
	public static event Action<PlayerJoinedEvent> playerJoined = delegate { };
	
	public static string username = Environment.GetEnvironmentVariable("USERNAME") ?? string.Empty;
	public static string? enemy;
	
	public static async Task Connection()
	{
		hub = new HubConnectionBuilder()
			  .WithUrl("https://localhost:7135/room")
			  .WithAutomaticReconnect()
			  .Build();
		hub.On<PlayerJoinedEvent>("PlayerJoined", e => playerJoined.Invoke(e));
		playerJoined += e => enemy = e.username;
		await hub.StartAsync();
	}
	
	public static async Task CreateRoom()
	{
		var response = await hub.InvokeAsync<CreateRoomResponse>(
			"CreateRoom",
			new CreateRoomRequest(username)
		);
		roomCode = response.roomCode;
		enemy = null;
	}
	
	public static async Task<JoinRoomResponse> JoinRoom(string roomCode)
	{
		var response = await hub.InvokeAsync<JoinRoomResponse>(
			"JoinRoom",
			new JoinRoomRequest(username, Client.roomCode = roomCode)
		);
		enemy = response.hostName;
		return response;
	}
}