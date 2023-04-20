using UnityEngine;

public class BlockView : MonoBehaviour
{
    public Block Block { get; set; }

    private void Update()
    {
        if (!Input.GetMouseButtonUp(0))
        {
            return;
        }
        RaycastHit hit;
        Ray ray = ReferenceManager.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                /*Show the following details available in the API response
                    [Grade level]: [Domain]
                    [Cluster]
                    [Standard Description]*/
                string blockInfoPanelText = "- " + Block.grade + ": " + Block.domain + "\n\n- " +
                                            Block.cluster + "\n\n- " +
                                            Block.standarddescription;
                ReferenceManager.Instance.BlockInfoPanelText.text = blockInfoPanelText;
                ReferenceManager.Instance.BlockInfoPanel.SetActive(true);
            }
        }
    }
}