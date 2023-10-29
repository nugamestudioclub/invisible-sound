using Godot;
using System;
using System.Linq;
public class TilePuzzle : SceneServiceNode
{
    public const int ROW_WIDTH = 5;
    public const int COL_HEIGHT = 5;
    private PuzzleTile[,] tiles = new PuzzleTile[ROW_WIDTH, COL_HEIGHT];

    public PuzzleTileState[,] InState { get; } = ConvertToStates(new int[ROW_WIDTH, COL_HEIGHT]
    {
        { 0,1,0,0,0 },
        { 0,1,0,1,0 },
        { 0,0,0,1,0 },
        { 1,1,1,0,0 },
        { 1,0,0,0,1 }
    });

    public PuzzleTileState[,] OutState { get; } = ConvertToStates(new int[ROW_WIDTH, COL_HEIGHT]
    {
        { 0,0,0,0,1 },
        { 1,1,1,0,1 },
        { 0,0,0,0,0 },
        { 0,1,1,1,0 },
        { 0,0,1,0,0 }
    });

    private static PuzzleTileState[,] ConvertToStates(int[,] intStates)
    {
        int cols = intStates.GetLength(0);
        int rows = intStates.GetLength(1);
        PuzzleTileState[,] temp = new PuzzleTileState[cols, rows];
        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < cols; i++)
            {
                temp[i, j] = (PuzzleTileState)intStates[i, j];
            }
        }
        return temp;
    }

    public override void _Ready()
    {
        base._Ready();
        for (int j = 0; j < ROW_WIDTH; j++)
        {
            for (int i = 0; i < COL_HEIGHT; i++)
            {
                PuzzleTile currentTile = GetNode<PuzzleTile>($"{i},{j}");
                tiles[j, i] = currentTile;
                currentTile.Entered += Tile_Entered;
            }
        }
        ApplyPattern(InState);
    }

    public void ApplyPattern(PuzzleTileState[,] tileStates)
    {
        for (int j = 0; j < ROW_WIDTH; j++)
        {
            for (int i = 0; i < COL_HEIGHT; i++)
            {
                tiles[i, j].ChangeState(tileStates[i, j]);
            }
        }
    }

    private void Tile_Entered(object sender, AreaEventArgs e)
    {
        OnCollision(new CollisionEventArgs(this, e.AreaCollider, e.OtherCollider, e.Args));
    }
}