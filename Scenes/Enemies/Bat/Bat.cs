using Godot;
using System;

public class Bat : Node2D
{
    private AnimatedSprite bat;                 // The animated sprite node for the bat

    public float BatSpeed = 400;                // The speed of the bat

    private RandomNumberGenerator rand;         // The random number generator

    private Vector2 spriteSize = Vector2.Zero;  // Vector 2 to hold the sprite size
    public override void _Ready()
    {
        rand = new RandomNumberGenerator();                 // Initialize the random number generator
        rand.Randomize();                                   // Randomize a new start seed
        bat = GetNode<AnimatedSprite>("AnimatedSprite");    // Get the bat AnimatedSprite node
        GetSpriteSize();                                    // Get the size of the bat sprite
        RandomizeBat();                                     // Ranomize the bat start position and animation
    }

    private void GetSpriteSize()
    {
        int w = bat.Frames.GetFrame("Flap1",0).GetWidth();  // Get the "Flap1" bat animation sprite width
        int h = bat.Frames.GetFrame("Flap1",0).GetHeight(); // Get the "Flap1" bat animation sprite height
        spriteSize = new Vector2(w,h);
    }
    private void RandomizeBat()
    {
        // Move the bat back and set a new random height position on the screen
        bat.Position = new Vector2(-spriteSize.x,rand.RandfRange(0, OS.WindowSize.y - spriteSize.y));

        uint randomAnim = rand.Randi()%2;       // Randomize a value betwen 0 and 1

        // If the value was zero
        if(randomAnim == 0)
        {
            bat.Play("Flap1");  // Play the flap 1 animation
            BatSpeed = 400;     // Set bat speed to 400
        }
        // If the value was 1
        else
        {
            bat.Play("Flap2");  // Play the flap 2 animation
            BatSpeed = 500;     // Set bat speed to 500
        }

    }
    public override void _Process(float delta)
    {
        // Move the bat forward
        bat.Position += new Vector2(BatSpeed * delta,0);

        // If the bat has passed the edge of the screen
        if(bat.Position.x > OS.WindowSize.x + spriteSize.x)
        {
            RandomizeBat();    // Randomize the bat start position and animation
        }
    }
}
