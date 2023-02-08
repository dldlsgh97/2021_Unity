using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ex03 : MonoBehaviour
{
    float speed = 5.0f;
    public GameObject ball;
    public Vector3 lanch_direction = new Vector3(10, 500, 300);
    
    private void OnCollisionEnter( Collision coll)
    {
       if(coll.gameObject.tag == "Finish")
        {
            ball.GetComponent<Rigidbody>().AddForce(lanch_direction);

        }
            
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance_per_frame = speed * Time.deltaTime;
        float vertical_input = Input.GetAxis("Vertical");
        float horizontal_input = Input.GetAxis("Horizontal");
        Vector3 lanch_direction = new Vector3(0, 1, 1);

        transform.Translate(Vector3.forward * vertical_input * distance_per_frame);
        transform.Translate(Vector3.right * horizontal_input * distance_per_frame);

       
    }


}
