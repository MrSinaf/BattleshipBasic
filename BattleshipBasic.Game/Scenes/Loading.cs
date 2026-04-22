using Ratelite;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace BattleshipBasic.Scenes;

public class Loading : Scene
{
	private Canvas canvas;
	private Image loadingIcon;
	
	public override void Init()
	{
		canvas = AddPlugin<Canvas>();
	}
	
	public override void Start()
	{
		var uiTexture = Vault.GetAsset<Texture2D>("ui")!;
		canvas.root.AddChild(new Image(uiTexture)
		{
			scale = new Vector2(8),
			size = new Vector2Int(130, 22),
			pivot = new Vector2(0.5F),
			anchors = new Vector2(0.5F, 0.75F),
			uv = uiTexture.GetUVRegion(new RectInt(0, 0, 130, 22))
		});
		canvas.root.AddChild(loadingIcon = new Image(uiTexture)
		{
			scale = new Vector2(8),
			size = new Vector2Int(16, 16),
			pivot = new Vector2(0.5F),
			anchors = new Vector2(0.5F, 0.25F),
			uv = uiTexture.GetUVRegion(new RectInt(0, 22, 16, 16))
		});
	}
	
	public override void Update()
		=> loadingIcon.rotation += 1000 * Time.delta;
}