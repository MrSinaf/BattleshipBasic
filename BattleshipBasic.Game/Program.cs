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
		 await Vault.LoadResource<BitmapFont>(
			 "fonts/tommy.ttf",
			 "big",
			 new BitmapFont.Config(new Vector2Int(256), 32, -1)
		 );
		 await Vault.LoadResource<BitmapFont>(
			 "fonts/tommy.ttf",
			 "default",
			 new BitmapFont.Config(new Vector2Int(256), 18, -1)
		 );
		 UIPrefab.Add<Label>(string.Empty, LabelPrefab);
		 UIPrefab.Add<Label>("big", LabelPrefabBig);
		 progress.Report(0.5F);
		 await Vault.LoadResource<Texture2D>("textures/purrvert.png", "purrvert-icon");
		 progress.Report(0.75F);
		 await Vault.LoadResource<Texture2D>("textures/ui.png", "ui");
		 progress.Report(1);
	 }
 )
 .SetStartingScene<Splash>()
 .Run();

void LabelPrefab(Label e)
{
	var font = Vault.GetAsset<BitmapFont>("default")!;
	e.font = font.data;
	e.material = font.material;
	e.useMeshBoundsSize = false;
}

void LabelPrefabBig(Label e)
{
	var font = Vault.GetAsset<BitmapFont>("big")!;
	e.font = font.data;
	e.material = font.material;
	e.useMeshBoundsSize = false;
}