namespace BattleshipBasic.Shared;

public record class JoinRoomResponse(bool success, string hostName);
public record class CreateRoomResponse(string roomCode);