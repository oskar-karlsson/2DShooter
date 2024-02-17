using Godot;

public partial class Galaxy : Sprite2D
{
    public void OnEnterNextLevelEntered(Node body)
    {
        if (body.IsInGroup(Groups.Player))
        {
            var spawner = GetNode<EnemySpawner>(NodePaths.EnemySpawner);
            if (spawner != null && spawner.killCount > spawner.lastKillCountAtEntry)
            {
                spawner.LevelUpEnemies();
            }
        }
    }
}