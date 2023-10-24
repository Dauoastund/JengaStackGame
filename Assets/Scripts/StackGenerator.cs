using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to generate a stack of blocks based on the grade selected in the inspector and the data loaded from the JSON file.
/// </summary>
public class StackGenerator : MonoBehaviour
{
    [SerializeField]
    private Block blockPrefab, blockWoodPrefab, blockGlassPrefab, blockStonePrefab;
    [SerializeField]
    public Grade grade;

    //Called when the data is loaded from the JSON file
    private void OnDataLoaded()
    {
        GenerateStack(JSONLoader.Instance.GetGradeData((int)grade));
    }

    //Generate a tower of blocks based on the grade selected in the inspector
    public void GenerateStack(GradeData[] gradeData)
    {
        for (int i = 0; i < gradeData.Length; i++)
        {
            CreateBlock(i, gradeData[i].mastery);
        }
    }

    //Create a block at the specified index
    private void CreateBlock(int index, int mastery)
    {
        Block block = blockPrefab;
        switch (mastery)
        {
            case 0:
                block = blockGlassPrefab;
                break;
            case 1:
                block = blockWoodPrefab;
                break;
            case 2:
                block = blockStonePrefab;
                break;
        }
        block = Instantiate(block);
        block.transform.parent = transform;
        block.transform.localPosition = GetBlockStackLocation(index);
        block.transform.localRotation = GetBlockStackRotation(index);
    }

    //Determine the location of each block in the stack, starting from the bottom
    private Vector3 GetBlockStackLocation(int index)
    {
        int layerID = index / 3;

        float xLocation = 0f;
        float yLocation = 0f;
        float zLocation = 0f;

        if (layerID % 2 == 0)
        {
            xLocation = index % 3 - 1;
            xLocation *= 1.5f;
            yLocation = layerID * 0.75f;
            zLocation = 0f;
        }
        else
        {
            xLocation = 0f;
            yLocation = layerID * 0.75f;
            zLocation = index % 3 - 1;
            zLocation *= 1.5f;
        }

        Vector3 blockLocation = new Vector3(xLocation, yLocation, zLocation);
        return blockLocation;
    }

    //Determine the rotation of each block in the stack, starting from the bottom
    private Quaternion GetBlockStackRotation(int index)
    {
        int layerID = index / 3;

        Quaternion blockRotation = Quaternion.identity;

        if (layerID % 2 == 0)
        {
            blockRotation = Quaternion.Euler(0f, 90f, 0f);
        }

        return blockRotation;
    }

    private void OnEnable()
    {
        JSONLoader.Instance.OnDataLoaded += OnDataLoaded;
    }

    private void OnDisable()
    {
        JSONLoader.Instance.OnDataLoaded -= OnDataLoaded;
    }
}

[System.Serializable]
public enum Grade
{
    Sixth = 6,
    Seventh = 7,
    Eighth = 8
}