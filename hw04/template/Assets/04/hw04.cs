using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class hw04 : MonoBehaviour
{
    public Texture2D icon = null;
    private int ball_count = 0;
    // Start is called before the first frame update
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, Screen.height - 64, 64, 64), icon);
        GUI.Label(new Rect(81, Screen.height - 40, 128, 32), ball_count.ToString());
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            ball_count++;

        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ball_count >= 10)
        {
            SceneManager.LoadScene("scene1", LoadSceneMode.Single);
        }
        
    }
}

