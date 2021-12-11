namespace MainMenu
{
    public static class MainMenuData
    {
        public static bool UseObstacleToggle { get; private set; } = true;

        public static void ChangeObstacleToggle(bool value)
        {
            UseObstacleToggle = value;
        }
        
    }
}
