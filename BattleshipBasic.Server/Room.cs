using Microsoft.AspNetCore.SignalR;

namespace BattleshipBasic.Server;

public class Room : Hub
{
	public async Task JoinRoom(string roomName)
	{
		await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
		await Clients.Group(roomName).SendAsync("PlayerJoined", Context.ConnectionId);
	}
	
	public async Task SendMessage(string roomName, string message)
	{
		await Clients.OthersInGroup(roomName).SendAsync("Message", Context.ConnectionId, message);
	}
}