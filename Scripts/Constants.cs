public static class InputActions
{
    public const string Left = "left";
    public const string Right = "right";
    public const string Up = "up";
    public const string Down = "down";
    public const string Click = "click";
}

public static class Nodes
{
    public const string Timer = "Timer";
    public const string EnemyHealth = "EnemyHealth";
    public const string PlayerHealth = "PlayerHealth";
    public const string MainGame = "MainGame";
    public const string Player = "Player";
    public const string KillCount = "KillCount";
}

public static class NodePaths
{
    public const string EnemySpawner = "/root/MainGame/EnemySpawner";
    public const string KillCount = "/root/MainGame/KillCount";
}

public static class Groups
{
    public const string Attacker = "attacker";
    public const string Player = "player";
}

public static class Signals
{
    public const string EnemyKilled = "EnemyKilled";
}