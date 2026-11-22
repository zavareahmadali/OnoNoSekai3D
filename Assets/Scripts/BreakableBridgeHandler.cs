using UnityEngine;

public class BreakableBridgeHandler : MonoBehaviour, IBreakable
{
    public GameObject BreakParticle;

    public void Demolish(Vector3 hitPosition)
    {
        Instantiate(BreakParticle, transform.position, BreakParticle.transform.rotation);
        Destroy(gameObject);
    }
}
