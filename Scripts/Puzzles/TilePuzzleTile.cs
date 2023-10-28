using Godot;
using System;

public class TilePuzzleTile : Area2D
{
    public PuzzleTileState State { get; private set; }

    [Export]
    private Texture goodTexture;
    [Export]
    private Texture badTexture;

    private Sprite currentSprite;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentSprite = GetNode<Sprite>("Sprite");
    }

    public void ChangeState(PuzzleTileState newState)
    {
        State = newState;
        if (newState == PuzzleTileState.Danger)
        {
            currentSprite.Texture = badTexture;
        }
        else
        {
            currentSprite.Texture = goodTexture;
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
