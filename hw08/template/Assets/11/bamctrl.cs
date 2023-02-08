using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bamctrl : MonoBehaviour
{
    public static float timer = 0.0f;
    public static bool is_shot = false;
    public static int score;
    int shoot_count;
    int windforce;
    public static float x;
    public static float y;
   

    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(-200.0f, 200.0f);
        y = Random.Range(-100.0f, 200.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
       
        GetComponent<Rigidbody>().AddForce(new Vector3(x, y, 0)*Time.deltaTime);
      
        
        


    }

    public void Shoot(Vector3 dir)
    {
        is_shot = true;
        GetComponent<Rigidbody>().AddForce(dir);
        

        

    }

    private void OnCollisionEnter(Collision other)
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(x, y, 0));
        GetComponent<Rigidbody>().isKinematic = true;
        is_shot = false;
        
        GetComponent<ParticleSystem>().Play();
        Vector3 collided_position = transform.position;
        float distance = collided_position.x * collided_position.x + (collided_position.y - 6.5f) * (collided_position.y - 6.5f);
        distance = Mathf.Sqrt(distance);
        if(distance>=0.0 && distance < 0.4)
        {
            score = score + 100;
        }
        else if (distance >= 0.4 && distance < 0.8)
        {
            score = score + 90;
        }
        else if (distance >= 0.8 && distance < 1.2)
        {
            score = score + 70;
        }
        else if (distance >= 1.2 && distance < 1.6)
        {
            score = score + 50;
        }
        else if (distance >= 1.6 && distance < 2.0)
        {
            score = score + 30;
        }
        else if (distance >= 2.0 )
        {
            score = score + 0;
        }

    }
    
}
