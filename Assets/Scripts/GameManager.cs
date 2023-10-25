using System.Collections;
using TMPro.Examples;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private StackGenerator[] stackGenerators = new StackGenerator[3];

    private void Awake()
    {
        Instance = this;
    }

    public void StartTestMyStackGameMode()
    {
        int stackIndex = CameraController.Instance.GetTargetIndex();
        stackGenerators[stackIndex].DestroyAllGlassBlocks();
    }

    public void AddStackGenerator(StackGenerator stackGenerator, int index)
    {
        stackGenerators[index] = stackGenerator;
    }
}
