using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class bamgenerator : MonoBehaviour
{
    int shoot_count;
    public GameObject bamsongi_prefab;
    void OnGUI()
    {
        GUI.Label(new Rect(81, Screen.height - 40, 128, 32), ("score", bamctrl.score).ToString());
        GUI.Label(new Rect(81, Screen.height - 25, 128, 32), ("shooot_count", shoot_count).ToString());
        GUI.Label(new Rect(81, Screen.height - 60, 128, 32), ("x_wind", bamctrl.x).ToString());
        GUI.Label(new Rect(81, Screen.height - 80, 128, 32), ("y_wind", bamctrl.y).ToString());
        if (shoot_count >= 5)
        {
            GUI.Label(new Rect(Screen.width/2-30,80,100,300),"Press 'R'to continue");
        }

    }
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (shoot_count < 5)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(GameObject.Find(bamsongi_prefab.name + "(Clone)"));
                GameObject bamsongi = Instantiate(bamsongi_prefab) as GameObject;
                Ray screen_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 shooting_ray = screen_ray.direction;
                bamsongi.GetComponent<bamctrl>().Shoot(shooting_ray * 1000);
                shoot_count = shoot_count + 1;

            }
        }
        else if (shoot_count >= 5)
        {

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("scene", LoadSceneMode.Single);
                bamctrl.score = 0;
            }
        }

    }
  
}
