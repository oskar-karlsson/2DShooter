using Godot;

public partial class Player : CharacterBody2D
{
    public float speed = 150f;
    public int experience = 0;

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

}