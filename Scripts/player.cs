using System.Collections.Generic;
using Godot;

public partial class Player : CharacterBody2D
{
    public float speed = 150f;
    public int experience = 0;
    private int currentLevel = 1;
    private List<int> experienceLevels = new List<int>{ 50 };
    private int additionalXpIncrease = 50; // Start with 200 for the first level, will increase by 100 each time

    public Player()
    {
        AddNextExperienceLevel();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("rightClick"))
        {
            PlaceBomb();
        }
    }

    public override void _PhysicsProcess(double delta){
        LookAt(GetGlobalMousePosition());

        var move_input = Input.GetVector(InputActions.Left, InputActions.Right, InputActions.Up, InputActions.Down);

        Velocity = move_input * speed;

        MoveAndSlide();
    }

    public void AddExperience(int xp)
    {
        experience += xp;
        CheckForLevelUp();
        UpdateXpUi();
    }

    private void UpdateXpUi()
	{
		var xpLabel = GetNode<Label>(NodePaths.XP);
		xpLabel.Text = $"XP: {experience}";
	}

    private void PlaceBomb()
    {
        var bombScene = (PackedScene)ResourceLoader.Load(Scenes.Bomb);
        Bomb bomb = (Bomb)bombScene.Instantiate();
        bomb.Position = Position; // Or a specified offset
        GetParent().AddChild(bomb); // Assuming the player and bombs are on the same node level
    }

    private void CheckForLevelUp()
    {
        // Since only one level can be gained at a time, check the next level's threshold
        if (currentLevel <= experienceLevels.Count && experience >= experienceLevels[currentLevel - 1])
        {
            var playerHealth = GetNode<PlayerHealth>(NodeNames.PlayerHealth);
            playerHealth.Heal();

            currentLevel++;
            UpdateLevelUi();
            SpawnPowerUps(); // Method to spawn power-ups
            AddNextExperienceLevel(); // Ensure there's always a next level threshold
        }
    }

    private void UpdateLevelUi()
	{
        var playerLevel = GetNode<Label>(NodePaths.PlayerLevel);
        playerLevel.Text = $"Level: {currentLevel}";
    }

    private void SpawnPowerUps()
    {
        // Assuming you have set up paths or references to your power-up scenes
        PackedScene puSpeedScene = (PackedScene)ResourceLoader.Load("res://Entities/PuEngine.tscn");
        PackedScene puFireRateScene = (PackedScene)ResourceLoader.Load("res://Entities/PuGun.tscn");
        PackedScene puRangeScene = (PackedScene)ResourceLoader.Load("res://Entities/PuBullet.tscn");

        // Instantiate power-ups
        Node2D puEngine = (Node2D)puSpeedScene.Instantiate();
        Node2D puGun = (Node2D)puFireRateScene.Instantiate();
        Node2D puBullet = (Node2D)puRangeScene.Instantiate();

        puEngine.AddToGroup(Groups.PowerUps);
        puGun.AddToGroup(Groups.PowerUps);
        puBullet.AddToGroup(Groups.PowerUps);

        var mainGameNode = GetTree().Root.GetNode(NodeNames.MainGame);

        // Set positions
        puEngine.Position = GetNode<Marker2D>("/root/MainGame/PowerUps/PuEngine").Position;
        puGun.Position = GetNode<Marker2D>("/root/MainGame/PowerUps/PuGun").Position;
        puBullet.Position = GetNode<Marker2D>("/root/MainGame/PowerUps/PuBullet").Position;

        mainGameNode.CallDeferred("add_child", puEngine);
        mainGameNode.CallDeferred("add_child", puGun);
        mainGameNode.CallDeferred("add_child", puBullet);
    }

    private void AddNextExperienceLevel()
    {
        int nextLevelExp = experienceLevels[experienceLevels.Count - 1] + additionalXpIncrease;
        experienceLevels.Add(nextLevelExp);

        // Increase the additionalIncrease for the next level
        additionalXpIncrease += 50;
    }
}