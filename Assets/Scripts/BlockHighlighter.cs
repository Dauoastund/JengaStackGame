using UnityEngine;

/// <summary>
/// Handles the highlighting of blocks when the mouse hovers over them and 
/// displays the block info when the right mouse button is clicked.
/// </summary>
public class BlockHighlighter : MonoBehaviour
{
    [SerializeField]
    private BlockInfoPanel blockInfoPanel;
    [SerializeField]
    private Material highlightMaterial;

    private Material originalMaterial;
    private Renderer lastRenderer;

    void Update()
    {
        HighlightBlock();
    }

    void HighlightBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Reset the last highlighted block if any
        if (lastRenderer != null)
        {
            lastRenderer.material = originalMaterial;
            lastRenderer = null;
        }

        if (Physics.Raycast(ray, out hit))
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null && renderer.gameObject.CompareTag("Block"))
            {
                // Store the original material
                originalMaterial = renderer.material;

                // Set the highlight material
                renderer.material = highlightMaterial;

                // Store the renderer of the highlighted block
                lastRenderer = renderer;

                CheckForClickEvents(renderer);
            }
        }
    }

    private void CheckForClickEvents(Renderer hitRenderer)
    {
        if(Input.GetMouseButtonDown(1) && hitRenderer.GetComponent<Block>())
        {
            GradeData gradeData = hitRenderer.GetComponent<Block>().GetGradeData();
            blockInfoPanel.SetInfo(gradeData,hitRenderer.transform);
        }
    }
}
