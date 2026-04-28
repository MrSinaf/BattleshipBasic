using Ratelite;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace BattleshipBasic.UI;

public sealed class PlayerPanel : UIElement
{
	private readonly Label usernameLabel;
	
	public string username { get => usernameLabel.text; set => usernameLabel.text = value; }
	
	public PlayerPanel(char playerChar)
	{
		mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		
		tint = new Color(0x081722);
		size = new Vector2(450, 70);
		cornerRadius = new Region(8);
		padding = new Region(10);
		
		var layout = new Layout { pivotAndAnchors = new Vector2(0, 0.5F) };
		AddChild(layout);
		layout.AddChild(new Label("Joueur " + playerChar, "big"));
		layout.AddChild(usernameLabel = new Label());
	}
	
	public void SetEmpty()
		=> usernameLabel.text = string.Empty;
}