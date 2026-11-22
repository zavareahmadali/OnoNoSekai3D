using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float destroyDelay = 1f;
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
