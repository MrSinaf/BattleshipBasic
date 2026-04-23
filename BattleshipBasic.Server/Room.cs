namespace BattleshipBasic.Server;

public class Room(Player host)
{
	public readonly Player playerA = host;
	public Player? playerB = null;
}