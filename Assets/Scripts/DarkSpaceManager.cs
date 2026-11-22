using UnityEngine;

public class DarkSpaceManager : MonoBehaviour, IActivatable
{
    public ParticleSystem normalParticleSystem;
    public ParticleSystem DispelParticleSystem;
    void Start()
    {
        
    }

    public void Activate<T>(T value)
    {
        Debug.Log("Dispelling");
        normalParticleSystem.Stop();
        normalParticleSystem.gameObject.SetActive(false);
        DispelParticleSystem.gameObject.SetActive(true);
    }

    public void Deactivate<T>(T value)
    {
        Debug.Log("Deactivating");
        DispelParticleSystem.gameObject.SetActive(false);
        normalParticleSystem.gameObject.SetActive(true);
        normalParticleSystem.Play();
    }
}
