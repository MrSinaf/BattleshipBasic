using BattleshipBasic.Shared;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipBasic.Server;

public class RoomHub : Hub
{
	private static readonly Dictionary<string, Room> rooms = [];
	private static readonly Dictionary<string, string> players = [];
	
	public override async Task OnDisconnectedAsync(Exception? exception)
		=> await LeaveRoom();
	
	public async Task<CreateRoomResponse> CreateRoom(CreateRoomRequest request)
	{
		const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
		string code;
		do
		{
			code = new string(
				Enumerable.Range(0, 8)
						  .Select(_ => chars[Random.Shared.Next(chars.Length)])
						  .ToArray()
			);
		} while (rooms.ContainsKey(code));
		
		await Groups.AddToGroupAsync(Context.ConnectionId, code);
		rooms.Add(code, new Room(new Player(Context.ConnectionId, request.username)));
		return new CreateRoomResponse(players[Context.ConnectionId] = code);
	}
	
	public async Task<JoinRoomResponse> JoinRoom(JoinRoomRequest request)
	{
		if (!rooms.TryGetValue(request.roomCode, out var room) || room.playerB != null)
			return new JoinRoomResponse(false, string.Empty);
		
		await Groups.AddToGroupAsync(Context.ConnectionId, request.roomCode);
		room.playerB = new Player(Context.ConnectionId, request.username);
		players[Context.ConnectionId] = request.roomCode;
		
		await Clients.OthersInGroup(request.roomCode).SendAsync(
			"PlayerJoined",
			new PlayerJoinedEvent(request.username)
		);
		return new JoinRoomResponse(true, room.playerA.name);
	}
	
	public async Task LeaveRoom()
	{
		if (players.TryGetValue(Context.ConnectionId, out var roomCode))
		{
			if (rooms.TryGetValue(roomCode, out var room))
			{
				if (room.playerA.id == Context.ConnectionId)
					rooms.Remove(roomCode);
				else
					room.playerB = null;
			}
			players.Remove(Context.ConnectionId);
			await Clients.OthersInGroup(roomCode).SendAsync("PlayerLeft", new PlayerLeftEvent());
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomCode);
		}
	}
}