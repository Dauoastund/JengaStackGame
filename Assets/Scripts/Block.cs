using System;
using UnityEngine;

/// <summary>
/// Base class for our jenga blocks.
/// </summary>
public class Block : MonoBehaviour
{
    private BlockType myBlockType;
    private GradeData gradeData;

    public void SetBlockType(BlockType blockType)
    {
        myBlockType = blockType;
    }

    public BlockType GetBlockType()
    {
        return myBlockType;
    }

    public void SetGradeData(GradeData gradeData)
    {
        this.gradeData = gradeData;
    }

    public GradeData GetGradeData()
    {
        return gradeData;
    }
}

public enum BlockType
{
    None,
    Glass,
    Wood,
    Stone
}