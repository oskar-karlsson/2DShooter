using Godot;

public partial class Enemy : CharacterBody2D
{
    Player player;
    public static float GlobalSpeedModifier = 0f;
    public static int xpPerKill = 10;
    public float baseSpeed = 100f;
    public float speed => baseSpeed + GlobalSpeedModifier;
    public float damage = 25f;
    public float attacksPerSecond = 2f;
    float timeBetweenAttacks;
    float timeUntilAttack;
    bool withinAttackRange = false;

    public override void _Ready()
    {
        player = (Player)GetTree().Root.GetNode(Nodes.MainGame).GetNode(Nodes.Player);

        timeBetweenAttacks = 1 / attacksPerSecond;
        ResetAttackTimer();
    }

    public override void _Process(double delta)
    {
        if (PlayerAlive() && withinAttackRange && timeUntilAttack <= 0)
        {
            Attack();
            ResetAttackTimer();
        }
        else
        {
            timeUntilAttack -= (float)delta;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!PlayerAlive())
        {
            Velocity = Vector2.Zero;
            return;
        }

        LookAt(player.GlobalPosition);

        var playerPosition = player.GlobalPosition;
        var enemyPosition = GlobalPosition;

        var direction = (playerPosition - enemyPosition).Normalized();
        Velocity = direction * speed;

        MoveAndSlide();
    }

    public void Attack()
    {
        player.GetNode<PlayerHealth>(Nodes.PlayerHealth).Damage(damage);
    }

    public void OnAttackRangeBodyEnter(Node2D body)
    {
        if (body.IsInGroup(Groups.Player))
        {
            withinAttackRange = true;
        }
    }

    public void OnAttackRangeBodyExit(Node2D body)
    {
        if (body.IsInGroup(Groups.Player))
        {
            withinAttackRange = false;
            ResetAttackTimer();
        }
    }

    private void ResetAttackTimer()
    {
        timeUntilAttack = timeBetweenAttacks;
    }

    private bool PlayerAlive()
    {
        if (player != null && IsInstanceValid(player))
        {
            return true;
        }

        return false;
    }
}