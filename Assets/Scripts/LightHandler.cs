using UnityEngine;
using UnityEngine.Serialization;

public class LightHandler : MonoBehaviour, IInteractable
{
    [Header("キラキラ")]
    [SerializeField] private Light normalLight;
    [SerializeField] private ParticleSystem normalLightParticles;
    [SerializeField] private float KiraKiraClearRadius = 2;

    [Header("ギラギラ")]
    [SerializeField] private Light highLight;
    [SerializeField] private ParticleSystem highLightParticles;
    [SerializeField] private float GiraGiraClearRadius = 5;

    [Header("Other Settings")]
    [SerializeField] private Transform DetectionOrigin;
    [SerializeField] private LayerMask DarkSpaceLayer;
    [SerializeField] private bool showGizmos;

    private AudioHandler audioHandler;
    
    private void Awake()
    {
        audioHandler = GetComponent<AudioHandler>();
    }
    
    public void Interact(string ono)
    {
        if (ono == Onos.KIRAKIRA)
        {
            highLight.enabled = false;
            normalLight.enabled = true;
            normalLightParticles.gameObject.SetActive(true);
            highLightParticles.gameObject.SetActive(false);
            audioHandler.PlayAudio(OnoType.KIRAKIRA);
        }
        else if (ono == Onos.GIRAGIRA)
        {
            normalLight.enabled = false;
            highLight.enabled = true;
            normalLightParticles.gameObject.SetActive(false);
            highLightParticles.gameObject.SetActive(true);
            audioHandler.PlayAudio(OnoType.GIRAGIRA);
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
