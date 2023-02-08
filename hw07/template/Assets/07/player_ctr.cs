using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_ctr : MonoBehaviour
{

    public static float ACCELRATION = 10.0f;
    public static float SPEED_MIN = 4.0f;
    public static float SPEED_MAX = 8.0f;
    public static float JUMP_HIEGHT_MAX = 3.0f;
    public static float JUMP_POWER_REDUCE = 0.5f;
    public static float FAIL_LIMIT = -5.0f;
    
    public static float level_timer = 0.0f;
    public static float timer = 0.0f;

    public enum STEP
    {
        NONE = -1,
        RUN = 0,
        JUMP,
        MISS,
        NUM
    };

    public STEP step = STEP.NONE;
    public STEP next_step = STEP.NONE;

    public float step_timer = 0.0f;
    private bool is_landed = false;
    private bool is_collided = false;
    private bool is_key_released = false;
    // Start is called before the first frame update
    void Start()
    {
        next_step = STEP.RUN;
        transform.Translate(new Vector3(0.0f, 0.0f, 3.0f * Time.deltaTime));
        
    }

    private void CheckLanded()
    {
        is_landed = false;
        do
        {
            Vector3 current_position = transform.position;
            Vector3 down_position = current_position + Vector3.down * 1.0f;

            RaycastHit hit;
            if (!Physics.Linecast(current_position, down_position, out hit))
                break;

            if (step == STEP.JUMP)
            {
                if (step_timer < Time.deltaTime * 3.0f)
                    break;
            }
            is_landed = true;
        } while (false);
    }

    // Update is called once per frame
    void Update()
    {
        level_timer += Time.deltaTime;
        timer += Time.deltaTime;


        if (player_ctr.level_timer >= 0 && player_ctr.level_timer < 15)
            SPEED_MAX = 5;
        else if (player_ctr.level_timer >= 15 && player_ctr.level_timer < 30)
            SPEED_MAX = 6;
        else if (player_ctr.level_timer >= 30 && player_ctr.level_timer < 45)
            SPEED_MAX = 7;
        else if (player_ctr.level_timer >= 45 && player_ctr.level_timer < 60)
            SPEED_MAX = 9;
        else if (player_ctr.level_timer >= 60 && player_ctr.level_timer < 75)
            SPEED_MAX = 8;
        else
        { 
            SPEED_MAX = 5;
        }
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        CheckLanded();
        step_timer += Time.deltaTime;


        switch (step)
        {
            case STEP.RUN:
            case STEP.JUMP:
                if (transform.position.y < FAIL_LIMIT)
                    next_step = STEP.MISS;
                break;
        }
        step_timer += Time.deltaTime;
        
        switch (step)
        {
            case STEP.RUN:
                velocity.x += player_ctr.ACCELRATION * Time.deltaTime;
                if (Mathf.Abs(velocity.x) > player_ctr.SPEED_MAX)
                    velocity.x = player_ctr.SPEED_MAX;
                break;

            case STEP.JUMP:
                do
                {
                    if (!Input.GetMouseButtonUp(0))
                        break;
                    if (is_key_released)
                        break;
                    if (velocity.y <= 0.0f)
                        break;

                    velocity.y *= JUMP_POWER_REDUCE;
                    is_key_released = true;

                } while (false);
                break;

        }
        GetComponent<Rigidbody>().velocity = velocity;


        step_timer += Time.deltaTime;

        if(next_step == STEP.NONE)
        {
            switch (step)
            {
                case STEP.RUN:
                    if (!is_landed)
                    {
                    }
                    else if (Input.GetMouseButtonDown(0))
                        next_step = STEP.JUMP;
                    break;
                case STEP.JUMP:
                    if (is_landed)
                        next_step = STEP.RUN;
                    break;

            }
        }
        while(next_step != STEP.NONE)
        {
            step = next_step;
            next_step = STEP.NONE;

            switch (step)
            {
                case STEP.JUMP:
                    velocity.y = Mathf.Sqrt(2.0f * 9.8f * JUMP_HIEGHT_MAX);
                    is_key_released = false;
                    break;
            }
            step_timer = 0.0f;
        }
        switch(step)
        {
            case STEP.RUN:
                velocity.x += ACCELRATION * Time.deltaTime;
                if (Mathf.Abs(velocity.x) > SPEED_MAX)
                    velocity.x = SPEED_MAX;
                break;

            case STEP.JUMP:
                do
                {
                    if (!Input.GetMouseButtonUp(0))
                        break;
                    if (is_key_released)
                        break;
                    if (velocity.y <= 0.0f)
                        break;

                    velocity.y *= JUMP_POWER_REDUCE;
                    is_key_released = true;
                } while (false);
                break;

            case STEP.MISS:
                velocity.x -= player_ctr.ACCELRATION * Time.deltaTime;
                if (velocity.x < 0.0f)
                {

                    velocity.x = 0.0f;
                    Application.Quit();

                }
                break;
        }
        GetComponent<Rigidbody>().velocity = velocity;

        
    }

    void OnGUI()
    {
        GUI.Label(new Rect(40, Screen.height - 40, 128, 32), "TIME:");
        GUI.Label(new Rect(81, Screen.height - 40, 128, 32), timer.ToString());
        
     



    }
}
