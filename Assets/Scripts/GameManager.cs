using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private ArrayList stackGenerators = new ArrayList();

    private GameMode gameMode;

    private void Awake()
    {
        Instance = this;
        gameMode = GameMode.None;
    }

    public void StartTestMyStackGameMode()
    {
        gameMode = GameMode.TestMyStack;
        Debug.Log(stackGenerators.Count);
        foreach (StackGenerator stackGenerator in stackGenerators)
        {
            stackGenerator.DestroyAllGlassBlocks();
        }
    }

    public void AddStackGenerator(StackGenerator stackGenerator)
    {
        stackGenerators.Add(stackGenerator);
    }
}

[System.Serializable]
public enum GameMode
{
    None,
    TestMyStack,
    StrengthenMyStack,
    Earthquake,
    BuildMyStack,
    Challenge
}
