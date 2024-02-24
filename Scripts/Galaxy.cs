using Godot;

public partial class Galaxy : Sprite2D
{
    private int areaLevel = 1;
    public void OnEnterNextLevelEntered(Node body)
    {
        if (body.IsInGroup(Groups.Player))
        {
            var spawner = GetNode<EnemySpawner>(NodePaths.EnemySpawner);
            if (spawner != null && spawner.killCount > spawner.lastKillCountAtEntry)
            {
                spawner.LevelUpEnemies();
                areaLevel++;
                UpdateUI();
            }
        }
    }

    private void UpdateUI()
	{
		var areaLabel = GetNode<Label>(NodePaths.AreaLevel);

		areaLabel.Text = $"Galaxy: {areaLevel}";
	}
}