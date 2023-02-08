using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class player : MonoBehaviour
{
    private float startTime;
    public TextAlignment timeTimer;
    public float jump_power = 0;
    public Texture2D icon = null;
    // Start is called before the first frame update

    
    void Start()
    {
        jump_power = Random.Range(5.0f, 8.0f);
       startTime = Time.time;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
            GetComponent<Rigidbody>().velocity = new Vector3(0, jump_power, 0);

        float time = Time.time - startTime;
      
    }
    private void OnCollisionEnter(Collision other)
    {
        SceneManager.LoadScene("main_scene");
    }
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, Screen.height - 64, 64, 64), icon);
        GUI.Label(new Rect(81, Screen.height - 40, 128, 32), (Time.time - startTime).ToString());
        GUI.Label(new Rect(81, Screen.height - 25, 128, 32), jump_power.ToString());
    }
}
