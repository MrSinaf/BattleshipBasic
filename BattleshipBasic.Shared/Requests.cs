namespace BattleshipBasic.Shared;

public record class JoinRoomRequest(string username, string roomCode);
public record class CreateRoomRequest(string username);