using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject pf_wall;
    public int interval = 0;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            transform.position = new Vector3(13, Random.Range(-3.0f, 4.0f), 0);
            Instantiate(pf_wall, transform.position, transform.rotation);
            interval = Random.Range(1, 2);
            yield return new WaitForSeconds(interval);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
