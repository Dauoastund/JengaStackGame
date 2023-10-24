using UnityEngine;

/// <summary>
/// Base class for our jenga blocks.
/// </summary>
public class Block : MonoBehaviour
{
    private BlockType myBlockType;

    public void SetBlockType(BlockType blockType)
    {
        myBlockType = blockType;
    }

    public BlockType GetBlockType()
    {
        return myBlockType;
    }
}

public enum BlockType
{
    None,
    Glass,
    Wood,
    Stone
}