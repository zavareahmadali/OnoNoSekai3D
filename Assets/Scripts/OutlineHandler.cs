using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Renderer targetRenderer;
    private Outline outline;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (DragAndDropHandler.currentOno == null)
            return;

        if (outline != null)
        {
            outline.enabled = true;
        }
        else
        {
            outline = (targetRenderer.GetComponent<Outline>() == null) ? targetRenderer.gameObject.AddComponent<Outline>() : targetRenderer.GetComponent<Outline>();

            outline.OutlineWidth = 5;
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.yellow;

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (outline != null)
        {
            outline.enabled = false;
        }
    }
}
