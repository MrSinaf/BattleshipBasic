using BattleshipBasic.Core;
using Microsoft.AspNetCore.SignalR.Client;
using Ratelite;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace BattleshipBasic.Scenes;

public class Menu(string? errorMessage = null) : Scene
{
	private Canvas canvas = null!;
	private Label message = null!;
	private TextInput codeInput = null!;
	
	public override void Init()
	{
		canvas = AddPlugin<Canvas>();
	}
	
	public override async Task Load()
		=> await Client.LeaveRoom();
	
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
		canvas.root.AddChild(
			message = new Label(string.Empty)
			{
				pivotAndAnchors = new Vector2(0.5F)
			}
		);
		codeInput = new TextInput
		{
			placeholder = "Code du lobby",
			pivotAndAnchors = new Vector2(0.5F, 0),
			position = new Vector2(0, 35),
			maxLenght = 8
		};
		codeInput.onValueChanged += code => Client.roomCode = code;
		canvas.root.AddChild(codeInput);
		
		var buttons = new Layout
		{
			orientation = Orientation.Horizontal,
			pivotAndAnchors = new Vector2(0.5F, 0),
			spacing = 10
		};
		buttons.AddChild(new Button("Héberger", () => Stage.Load(new Lobby(true))));
		buttons.AddChild(new Button("Rejoindre", OnJoinClick));
		buttons.AddChild(new Button("Quitter", () => { }));
		canvas.root.AddChild(buttons);
		
		var nameInput = new TextInput
		{
			placeholder = "Pseudo",
			value = Client.username,
			pivotAndAnchors = Vector2.zero,
			position = new Vector2(20),
		};
		nameInput.onValueChanged += name => Client.username = name;
		canvas.root.AddChild(nameInput);
		
		if (errorMessage != null)
			SetErrorMessage(errorMessage);
	}
	
	private void OnJoinClick()
	{
		var value = codeInput.value;
		if (value == string.Empty)
		{
			SetErrorMessage("Veuillez entrer un code pour le lobby.");
			return;
		}
		
		SetInfoMessage("Connexion en cours...");
		Task.Run(async () =>
			{
				var response = await Client.JoinRoom(value);
				if (response.success)
					Stage.Load(new Lobby(false));
				else
					SetErrorMessage("Le code du lobby est invalide!");
			}
		);
	}
	
	private void SetErrorMessage(string error)
	{
		message.tint = new Color(0xCE333E);
		message.text = error;
	}
	
	private void SetInfoMessage(string infos)
	{
		message.tint = Color.cyan;
		message.text = infos;
	}
}