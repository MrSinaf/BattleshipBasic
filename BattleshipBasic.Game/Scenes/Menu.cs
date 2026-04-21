using Ratelite;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace BattleshipBasic.Scenes;

public class Menu : Scene
{
	private Canvas canvas = null!;
	
	public override void Init()
	{
		canvas = AddPlugin<Canvas>();
	}
	
	public override void Start()
	{
		canvas.root.padding = new Region(10);
		var title = new Layout
		{
			orientation = Orientation.Vertical,
			pivot = new Vector2(0.5F),
			anchors = new Vector2(0.5F, 0.75F),
			spacing = 5,
			alignment = 1
		};
		title.AddChild(
			new Image(Vault.GetAsset<Texture2D>("ui")!)
			{
				scale = new Vector2(8)
			}
		);
		title.AddChild(new Label("Développé par PurrVert"));
		canvas.root.AddChild(title);
		
		var buttons = new Layout
		{
			orientation = Orientation.Horizontal,
			pivotAndAnchors = new Vector2(0.5F, 0),
			spacing = 10
		};
		buttons.AddChild(new Button("Héberger", () => { }));
		buttons.AddChild(new Button("Rejoindre", () => { }));
		buttons.AddChild(new Button("Quitter", () => { }));
		canvas.root.AddChild(buttons);
		
		canvas.root.AddChild(
			new TextInput
			{
				placeholder = "Pseudo",
				value = Environment.GetEnvironmentVariable("USERNAME") ?? string.Empty,
				pivotAndAnchors = new Vector2(0.5F, 0),
				position = new Vector2(0, 60)
			}
		);
	}
}