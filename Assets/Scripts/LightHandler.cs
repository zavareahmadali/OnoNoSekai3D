using UnityEngine;

public class LightHandler : MonoBehaviour, IInteractable
{
    public Light normalLight;
    public ParticleSystem normalLightParticles;
    public Light highLight;
    public ParticleSystem highLightParticles;
    public float KiraKiraClearRadius = 2;
    public float GiraGiraClearRadius = 5;
    public Transform DetectionOrigin;
    public LayerMask DarkSpaceLayer;
    public bool showGizmos;

    public void Interact(string ono)
    {
        if (ono == Onos.KIRAKIRA)
        {
            highLight.enabled = false;
            normalLight.enabled = true;
            normalLightParticles.gameObject.SetActive(true);
            highLightParticles.gameObject.SetActive(false);
        }
        else if (ono == Onos.GIRAGIRA)
        {
            normalLight.enabled = false;
            highLight.enabled = true;
            normalLightParticles.gameObject.SetActive(false);
            highLightParticles.gameObject.SetActive(true);
        }

        //Collider[] objectsInside = Physics.OverlapBox(transform.position, new Vector3(2, 2, (ono == Onos.GIRAGIRA)? GiraGiraClearRadius : KiraKiraClearRadius) * 0.5f, Quaternion.identity);
        Collider[] objectsInside = Physics.OverlapSphere(transform.position, (ono == Onos.GIRAGIRA) ? GiraGiraClearRadius : KiraKiraClearRadius);

        if (objectsInside.Length > 0)
        {
            foreach (var o in objectsInside)
            {
                Debug.Log(o.name);
                if (o.TryGetComponent<IActivatable>(out IActivatable d))
                {
                    d.Activate(ono);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(DetectionOrigin.position , new Vector3(2, 2, KiraKiraClearRadius));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(DetectionOrigin.position, new Vector3(2, 2, GiraGiraClearRadius));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(DetectionOrigin.position, GiraGiraClearRadius);
    }
}
