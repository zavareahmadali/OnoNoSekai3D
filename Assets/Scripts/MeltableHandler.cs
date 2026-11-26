using System.Collections;
using UnityEngine;

public class MeltableHandler : MonoBehaviour, IActivatable
{
    public float meltDuration = 2f;
    public float initialMeltParam = 0.6f;
    public float targetMeltParam;
    public Renderer renderer;
    private Material material;
    
    private Collider objectCollider;
    private const string MELTPARAM = "_MeltingHeight";
    void Start()
    {
        material = renderer.GetComponent<Renderer>().material;
        objectCollider = GetComponent<Collider>();
    }

    //We use Activate becuase this can be activated by the GiraGira
    public void Activate<T>(T value)
    {
        Debug.Log("Activate called with value: " + value);
        if (value.ToString() == Onos.GIRAGIRA)
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(MeltCoroutine());
        }
        
    }

    public void Deactivate<T>(T value)
    {
        
    }

    IEnumerator MeltCoroutine()
    {
        float meltAmount = initialMeltParam;
        while (meltAmount >= targetMeltParam)
        {
            meltAmount -= Time.deltaTime / meltDuration;
            material.SetFloat(MELTPARAM, meltAmount);
            yield return null;
        }
        objectCollider.enabled = false; // Disable the collider after melting
        yield return null;
    }

}
