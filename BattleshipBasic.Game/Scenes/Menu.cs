using BattleshipBasic.Core;
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
		var uiTexture = Vault.GetAsset<Texture2D>("ui")!;
		var title = new Layout
		{
			orientation = Orientation.Vertical,
			pivot = new Vector2(0.5F),
			anchors = new Vector2(0.5F, 0.75F),
			spacing = 5,
			alignment = 1
		};
		title.AddChild(
			new Image(uiTexture)
			{
				size = new Vector2Int(130, 22),
				scale = new Vector2(8),
				uv = uiTexture.GetUVRegion(new RectInt(0, 0, 130, 22))
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
		
		var nameInput = new TextInput
		{
			placeholder = "Pseudo",
			value = Client.username,
			pivotAndAnchors = new Vector2(0.5F, 0),
			position = new Vector2(0, 60)
		};
		nameInput.onValueChanged += name => Client.username = name;
		canvas.root.AddChild(nameInput);
	}
}