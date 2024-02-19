using Godot;

public partial class Player : CharacterBody2D
{
    public float speed = 150f;
    public int experience = 0;

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
        // Possibly check for level up or update UI here
    }

    private void PlaceBomb()
    {
        var bombScene = (PackedScene)ResourceLoader.Load("res://Entities/Bomb.tscn");
        Bomb bomb = (Bomb)bombScene.Instantiate();
        bomb.Position = this.Position; // Or a specified offset
        GetParent().AddChild(bomb); // Assuming the player and bombs are on the same node level
    }
}