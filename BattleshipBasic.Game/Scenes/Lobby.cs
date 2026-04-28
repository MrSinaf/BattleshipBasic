using BattleshipBasic.Core;
using BattleshipBasic.Shared;
using BattleshipBasic.UI;
using Microsoft.AspNetCore.SignalR.Client;
using Ratelite;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace BattleshipBasic.Scenes;

public class Lobby(bool hosting) : Scene
{
	private Canvas canvas = null!;
	
	private PlayerPanel playerA = null!;
	private PlayerPanel playerB = null!;
	
	public override void Init()
	{
		canvas = AddPlugin<Canvas>();
		Window.current.keyPressed += OnKeyPressed;
		Client.playerJoined += OnPlayerJoined;
		Client.playerLeft += OnPlayerLeft;
	}
	
	public override void Start()
	{
		var panel = new Panel
		{
			pivotAndAnchors = new Vector2(0.5F, 1),
			size = new Vector2(200, 50),
			cornerRadius = new Region(0, 0, 10, 10),
			tint = new Color(0x122333)
		};
		canvas.root.AddChild(panel);
		panel.AddChild(
			new Label(Client.roomCode, "big")
			{
				pivotAndAnchors = new Vector2(0.5F),
				position = new Vector2(0, -2)
			}
		);
		
		var layout = new Layout
		{
			spacing = 15,
			pivotAndAnchors = new Vector2(0.5F, 0.75F),
			orientation = Orientation.Horizontal
		};
		layout.AddChild(playerA = new PlayerPanel('A'));
		layout.AddChild(playerB = new PlayerPanel('B'));
		
		if (hosting)
		{
			playerA.username = Client.username;
			playerB.username = Client.enemy ?? string.Empty;
		}
		else
		{
			playerA.username = Client.enemy ?? string.Empty;
			playerB.username = Client.username;
		}
		canvas.root.AddChild(layout);
		canvas.root.AddChild(new Button("Démarrer", () => { })
		{
			pivotAndAnchors = new Vector2(0.5F, 0.25F)
		});
	}
	
	public override async Task Load()
	{
		if (hosting)
			await Client.CreateRoom();
	}
	
	public override void Unload()
	{
		Window.current.keyPressed -= OnKeyPressed;
		Client.playerJoined -= OnPlayerJoined;
		Client.playerLeft -= OnPlayerLeft;
	}
	
	private void OnPlayerJoined(PlayerJoinedEvent e)
	{
		playerB.username = e.username;
	}
	
	private void OnPlayerLeft(PlayerLeftEvent _)
	{
		if (hosting)
			playerB.SetEmpty();
		else
			Stage.Load(new Menu("L'hôte du lobby s'est déconnecté."));
	}
	
	private static void OnKeyPressed(Key key, int _)
	{
		if (key == Key.Escape)
			Stage.Load(new Menu());
	}
}