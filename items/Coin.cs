using Godot;

public partial class Coin : Node2D
{
    [Signal]
    public delegate void CaughtCoinEventHandler(Node2D body);

    bool coinCollected = false;
    AnimatedSprite2D coinSprite;

    public override void _Ready() {
        coinSprite = GetNode<AnimatedSprite2D>("%CoinSprite");
        coinSprite.Play("coin");
    }

    void OnCoinAnimationFinished() {
        if (coinCollected) {
            QueueFree();
        }        
    }

    void OnCoinAreaEntered(Node2D body)
    {
        coinCollected = true;
        coinSprite.Play("collect_coin");
        EmitSignal(SignalName.CaughtCoin, body);
    }
}
