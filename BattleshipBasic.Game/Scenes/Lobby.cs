using BattleshipBasic.Core;
using BattleshipBasic.Shared;
using Ratelite;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace BattleshipBasic.Scenes;

public class Lobby(bool hosting) : Scene
{
	private Canvas canvas = null!;
	
	public Label hostName = null!;
	public Label playerName = null!;
	
	public override void Init()
	{
		canvas = AddPlugin<Canvas>();
		Window.current.keyPressed += OnKeyPressed;
		Client.playerJoined += OnPlayerJoined;
	}
	
	public override void Start()
	{
		var panel = new Panel
		{
			pivotAndAnchors = new Vector2(0.5F, 1),
			size = new Vector2(200, 50),
			cornerRadius = new Region(0, 0, 10, 10)
		};
		canvas.root.AddChild(panel);
		panel.AddChild(
			new Label(Client.roomCode, "big")
			{
				pivotAndAnchors = new Vector2(0.5F),
				position = new Vector2(0, -2)
			}
		);
		
		var layout = new Layout();
		layout.AddChild(hostName = new Label { tint = Color.green });
		layout.AddChild(playerName = new Label { tint = Color.red });
		if (hosting)
		{
			hostName.text = Client.username;
			playerName.text = Client.enemy ?? string.Empty;
		}
		else
		{
			hostName.text = Client.enemy ?? string.Empty;
			playerName.text = Client.username;
		}
		canvas.root.AddChild(layout);
	}
	
	public override void Unload()
	{
		Window.current.keyPressed -= OnKeyPressed;
		Client.playerJoined -= OnPlayerJoined;
	}
	
	private void OnPlayerJoined(PlayerJoinedEvent e)
	{
		playerName.text = e.username;
	}
	
	private static void OnKeyPressed(Key key, int _)
	{
		if (key == Key.Escape)
			Stage.Load(new Menu());
	}
}