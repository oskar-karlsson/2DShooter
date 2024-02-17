using Godot;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene enemyScene;
	[Export] public Node2D[] spawnPoints;
	public float enemiesPerSecond = 0.4f;

	float timeBetweenSpawns;
	float timeUntilSpawns = 0;
	public int killCount = 0;
	public int lastKillCountAtEntry = 0;

	public override void _Ready()
	{
		timeBetweenSpawns = 1 / enemiesPerSecond;
	}

	public override void _Process(double delta)
	{
		if (timeUntilSpawns > timeBetweenSpawns)
		{
			Spawn();

			timeUntilSpawns = 0;
		}
		else
		{
			timeUntilSpawns += (float)delta;
		}
	}

	private void Spawn()
	{
		var rng = new RandomNumberGenerator();
		var location = spawnPoints[rng.Randi() % spawnPoints.Length].GlobalPosition;
		var enemy = (Enemy)enemyScene.Instantiate();
		enemy.GlobalPosition = location;

		var enemyHealth = enemy.GetNode<EnemyHealth>(Nodes.EnemyHealth);

		enemyHealth.Connect(Signals.EnemyKilled, new Callable(this, nameof(OnEnemyKilled)));

		GetTree().Root.AddChild(enemy);
	}

	private void OnEnemyKilled()
	{
		killCount++;
		UpdateKillCountLabel();
		var player = (Player)GetTree().Root.GetNode(Nodes.MainGame).GetNode(Nodes.Player);
		if (player != null)
		{
			player.AddExperience(Enemy.xpPerKill);
		}
	}

	private void UpdateKillCountLabel()
	{
		var killCountLabel = GetNode<Label>(NodePaths.KillCount);

		killCountLabel.Text = $"Kills: {killCount}";
	}

	public void LevelUpEnemies()
	{
		enemiesPerSecond += 0.1f;
		timeBetweenSpawns = 1 / enemiesPerSecond;
		Enemy.GlobalSpeedModifier += 10f;
		Enemy.xpPerKill += (int)(Enemy.xpPerKill * 0.1);
		lastKillCountAtEntry = killCount;
	}
}