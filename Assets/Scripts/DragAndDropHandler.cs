using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragAndDropHandler : MonoBehaviour
{
    public Vector2 OnSelectedOffset;
    public InputActionReference clickActionRefrence;
    public InputActionReference dragActionRefrence;
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;
    public float smoothSpeed = 0.125f;
    public LayerMask onoLayer;
    private Camera camera;
    public static OnoHandler currentOno;
    private RectTransform currentOnoRect;
    private Vector2 targetPosition;
    bool isClosedEnoughToTarget = false;
    
    private void Start()
    {
        currentOno = null;
        camera = FindAnyObjectByType<Camera>();
    }

    private void FixedUpdate()
    {
        if (currentOnoRect == null)
            return;

        if (targetPosition == Vector2.zero)
            return;

        isClosedEnoughToTarget = Vector2.Distance(currentOnoRect.anchoredPosition, targetPosition) <= 0.1f;

        if (isClosedEnoughToTarget)
            return;
        
        
        currentOnoRect.anchoredPosition = Vector2.Lerp(currentOnoRect.anchoredPosition, targetPosition, Time.fixedDeltaTime * smoothSpeed);
    }
    private void OnEnable()
    {
        currentOno = null;
        clickActionRefrence.action.performed += ClickAction_performed;
        clickActionRefrence.action.canceled += ClickAction_canceled;

        dragActionRefrence.action.performed += DragAction_performed;
    }
    private void OnDisable()
    {
        clickActionRefrence.action.performed -= ClickAction_performed;
        clickActionRefrence.action.canceled -= ClickAction_canceled;

        dragActionRefrence.action.performed -= DragAction_performed;
    }

    private void DragAction_performed(InputAction.CallbackContext obj)
    {
        if (currentOno == null)
            return;

        // 1. Get the raw screen position from the mouse
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        // 2. Get the parent RectTransform of your UI element
        RectTransform parentRect = currentOnoRect.parent as RectTransform;

        // 3. Convert screen position to local position inside the parent container
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            mouseScreenPos,
            null, // Use null if your Canvas is set to 'Screen Space - Overlay'. Use 'Camera.main' if it's 'Screen Space - Camera'
            out Vector2 localPoint))
        {
            // 4. Assign the corrected local point to the anchoredPosition
            //currentOnoRect.anchoredPosition = localPoint + OnSelectedOffset;
            targetPosition = localPoint + OnSelectedOffset;
        }

    }
    
    private void ClickAction_performed(InputAction.CallbackContext obj)
    {
        if (GameStateHandler.GetIsGameOver())
            return;

        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = Mouse.current.position.value;

        // Create a list to store raycast results
        List<RaycastResult> results = new List<RaycastResult>();

        // Raycast against the UI Canvas
        raycaster.Raycast(pointerData, results);

        currentOno = null;
        currentOnoRect = null;

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Ono"))
            {
                currentOno = result.gameObject.GetComponent<OnoHandler>();
                currentOnoRect = currentOno.GetComponent<RectTransform>();
                currentOno.OnSelected();
                raycaster.enabled = false;
            }
        }

    }

    private void ClickAction_canceled(InputAction.CallbackContext obj)
    {
        if (currentOno == null)
            return;

        Ray ray = camera.ScreenPointToRay(Mouse.current.position.value);
        if (Physics.Raycast(ray, out RaycastHit hitinfo))
        {
            if (hitinfo.transform.TryGetComponent<IInteractable>(out IInteractable item))
            {
                item.Interact(currentOno.onomatope);
                //Show Effect of Current Onomatopoeia
            }
        }
        //we should check what are we hovering over somewhere
        if (currentOno)
        {
            currentOno.OnDeselected(true);
            currentOno = null;
            currentOnoRect = null;
        }
        raycaster.enabled = true;

    }

}
