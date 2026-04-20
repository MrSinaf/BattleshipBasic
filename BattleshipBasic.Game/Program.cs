using BattleshipBasic.Scenes;
using Ratelite;
using Ratelite.UI;

R.CreateGame("Battleship Basic").AddModule<UIModule>().SetStartingScene<Menu>().Run();