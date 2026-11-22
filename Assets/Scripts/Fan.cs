using UnityEditor.PackageManager;
using UnityEngine;

public class Fan : MonoBehaviour, IInteractable
{
    public float rotationSpeed;
    private bool canMove = false;
    private float actualRotationSpeed;
    void Start()
    {
        canMove = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canMove)
            return;
        transform.Rotate(Vector3.up * actualRotationSpeed * Time.deltaTime);
    }

    public void Interact(string ono)
    {
        if (ono == Onos.KURUKURU)
        {
            actualRotationSpeed = rotationSpeed / 10;
            canMove = true;
            ChangeFloatStateOfObjects(false);
        }
        else if (ono == Onos.GURUGURU)
        {
            actualRotationSpeed = rotationSpeed;
            canMove = true;
            ChangeFloatStateOfObjects(true);
        }
    }

    public void ChangeFloatStateOfObjects(bool newState)
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 3, Vector3.up, 3);
        if (hits.Length != 0)
        {
            foreach (var item in hits)
            {
                if (item.transform.TryGetComponent<IFloatable>(out IFloatable floatable))
                {
                    floatable.ChangeFloatState(newState);
                }
            }
            

        }
    }
}
