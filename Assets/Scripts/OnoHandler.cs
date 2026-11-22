using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OnoHandler : MonoBehaviour
{
    public string onomatope;
    public GameObject PopVFX;
    public TMP_Text text;
    public float returnSpeed = 1;
    public Image visual;
    private Image detectionImage;
    public Sprite normal;
    public Sprite selected;
    private Vector2 initialPosition;
    private bool isSelected;
    private Vector2 currentPosition;
    private RectTransform rect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        detectionImage = GetComponent<Image>();
        if (text.gameObject.activeInHierarchy)
        {
            text.text = onomatope; 
        }
        initialPosition = rect.anchoredPosition;
    }

    public void OnSelected()
    {
        detectionImage.raycastTarget = false;
        visual.sprite = selected;
        isSelected = true;
    }

    public void OnDeselected(bool returnToNormal)
    {
        detectionImage.raycastTarget = true;
        visual.sprite = normal;
        isSelected = false;
    }

    public void ShowAnimationEffect()
    {
        if (PopVFX != null)
        {
            GameObject popVFX = Instantiate(PopVFX, transform.position, Quaternion.identity);
            popVFX.transform.SetParent(transform.parent, true);
        }
        else
        {
            Debug.Log($"PopVFX is not assigned for {onomatope}");
        }
    }

    private void FixedUpdate()
    {
        if (isSelected)
            return;
        if (Vector3.Distance(transform.position, initialPosition) <= 0.05f)
            return;

        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition , initialPosition, returnSpeed * Time.deltaTime);
    }
}
