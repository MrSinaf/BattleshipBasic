using BattleshipBasic.Core;
using Ratelite;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace BattleshipBasic.Scenes;

public class Menu : Scene
{
	private Canvas canvas = null!;
	private Label? label;
	
	public override void Init()
	{
		canvas = AddPlugin<Canvas>();
	}
	
	public override void Start()
	{
		Client.Connection();
		Client.onMessageReceived += (id, message) => label?.text += $"\n{id} : {message}";
		Client.onPlayerJoined += id => label?.text += $"\nPlayer joined : {id}";
		canvas.root.AddChild(new Button("Say hello", () => Client.SendMessage("Hello!")));
		canvas.root.AddChild(label = new Label
		{
			pivotAndAnchors = new Vector2(0, 1)
		});
	}
}