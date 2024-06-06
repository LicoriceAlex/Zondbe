
namespace AiFirst
{
    public static class Data
    {
        public enum Scenes { Menu, Game, GameOver, Pause, About }
        public static Scenes CurrentScene { get; set; } = Scenes.Menu;
        public static int ScreenWidth { get; set; } = 1600;
        public static int ScreenHeight { get; set; } = 900;
        public static int Score { get; set; }
        public static int CoinsCollected { get; set; }
        public static bool Exit {  get; set; }

    }
}
