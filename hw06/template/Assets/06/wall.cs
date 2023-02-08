using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        speed = Random.Range(-4.0f, -6.0f);
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
