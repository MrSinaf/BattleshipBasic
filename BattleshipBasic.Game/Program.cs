using BattleshipBasic.Scenes;
using Ratelite;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

Stage.loadingScene = new Loading();

R.CreateGame("Battleship Basic")
 .AddModule<UIModule>()
 .LoadingAssets(async progress =>
 {
	 await Vault.LoadResource<BitmapFont>("fonts/tommy.ttf", "default");
	 UIPrefab.Add<Label>(string.Empty, LabelPrefab);
	 progress.Report(0.5F);
	 await Vault.LoadResource<Texture2D>("textures/purrvert.png", "purrvert-icon");
	 progress.Report(0.75F);
	 await Vault.LoadResource<Texture2D>("textures/ui.png", "ui");
	 progress.Report(1);
 })
 .SetStartingScene<Splash>()
 .Run();

void LabelPrefab(Label e)
{
	var font = Vault.GetAsset<BitmapFont>("default")!;
	e.font = font.data;
	e.material = font.material;
	e.useMeshBoundsSize = false;
}