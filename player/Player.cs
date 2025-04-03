using Godot;

public partial class Player : CharacterBody2D
{
    public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    [Export]
    public float Speed;
    [Export]
    public float JumpSpeed;
    AnimatedSprite2D animatedSprite;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite.Play();
    }

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;
        velocity.Y += Gravity * (float)delta;

        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y -= JumpSpeed;
        }

        if (Input.IsActionJustPressed("move_down"))
        {
            velocity.Y += JumpSpeed * 0.5f;
        }

        float direction = Input.GetAxis("move_left", "move_right");
        velocity.X = direction * Speed;

        if (IsOnFloor())
        {
            if (direction != 0)
            {
                animatedSprite.Animation = "run";
                animatedSprite.FlipH = direction < 0;
            }
            else
            {
                animatedSprite.Animation = "idle";
            }

            if (Input.IsActionPressed("sprint"))
            {
                velocity.X += direction * Speed;
                animatedSprite.SpeedScale = 2;
            }
            else
            {
                animatedSprite.SpeedScale = 1;
            }
        }
        else
        {
            if (velocity.Y < 0)
            {
                animatedSprite.Animation = "jump";
            }
            else if (velocity.Y >= JumpSpeed)
            {
                animatedSprite.Animation = "mid_air";
            }
            else if (velocity.Y > 0)
            {
                animatedSprite.Animation = "landing";
            }
        }


        Velocity = velocity;
        MoveAndSlide();
    }
}
