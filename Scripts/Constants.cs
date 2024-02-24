public static class InputActions
{
    public const string Left = "left";
    public const string Right = "right";
    public const string Up = "up";
    public const string Down = "down";
    public const string Click = "click";
}

public static class NodeNames
{
    public const string Timer = "Timer";
    public const string EnemyHealth = "EnemyHealth";
    public const string PlayerHealth = "PlayerHealth";
    public const string MainGame = "MainGame";
    public const string Player = "Player";
    public const string PuEngine = "PuEngine";
    public const string PuGun = "PuGun";
    public const string PuBullet = "PuBullet";
    public const string Gun = "Gun";
}

public static class NodePaths
{
    public const string EnemySpawner = "/root/MainGame/EnemySpawner";
    public const string KillCount = "/root/MainGame/KillCount";
    public const string XP = "/root/MainGame/XP";
    public const string AreaLevel = "/root/MainGame/AreaLevel";
    public const string HP = "/root/MainGame/HP";
}

public static class Groups
{
    public const string Attacker = "attacker";
    public const string Player = "player";
    public const string PowerUps = "powerUps";
}

public static class Signals
{
    public const string EnemyKilled = "EnemyKilled";
    public const string body_entered = "body_entered";
}

public static class Scenes
{
    public const string Bomb = "res://Entities/Bomb.tscn";
}