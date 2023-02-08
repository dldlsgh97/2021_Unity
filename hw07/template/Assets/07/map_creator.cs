using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_creator : MonoBehaviour
{
    public static float BLOCK_WIDTH = 1.0f;
    public static float BLOCK_HEIGHT = 0.2f;
    public static float BLOCK_HEIGHT_X = 0.2f;
    public static int BLOCK_NUM_IN_SCREEN = 24;

    private level_ctr lev_ctr = null;

    private struct FloorBlock
    {
        public bool is_created;
        public Vector3 position;
    };

    private FloorBlock last_block;
    private player_ctr player = null;
    private block_creator block;
    // Start is called before the first frame update
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_ctr>();
        last_block.is_created = false;
        block = gameObject.GetComponent<block_creator>();

        lev_ctr = new level_ctr();
        lev_ctr.Initialize();
    }

   
    private void CreateFloorBlock()
    {
        Vector3 block_position;

        if(!last_block.is_created)
        {
            block_position = player.transform.position;
            block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
            block_position.y = 0.0f;
        }
        else
        {
            block_position = last_block.position;
        }
        block_position.x += BLOCK_WIDTH;

        //block.CreateBlock(block_position);
        lev_ctr.UpdateStatus();

        block_position.y = lev_ctr.current_block.height * BLOCK_HEIGHT;
        level_ctr.CreateInfo current = lev_ctr.current_block;

        if (current.block_type == level_ctr.Block.TYPE.FLOOR)
            block.CreateBlock(block_position);

        last_block.position = block_position;
        last_block.is_created = true;
    }
    public bool IsGone(GameObject block_object)
    {
        bool result = false;
        float left_limit = player.transform.position.x - BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
        if (block_object.transform.position.x < left_limit)
            result = true;
        return result;
    }

   
    // Update is called once per frame
    void Update()
    {
        if (player_ctr.level_timer >= 0 && player_ctr.level_timer < 5)
            BLOCK_HEIGHT = 0.2f;
        else if (player_ctr.level_timer >= 5 && player_ctr.level_timer < 10)
            BLOCK_HEIGHT = BLOCK_HEIGHT * Random.Range(-1, 1);
        else if (player_ctr.level_timer >= 10 && player_ctr.level_timer < 15)
            BLOCK_HEIGHT = BLOCK_HEIGHT_X * Random.Range(-2, 2);
        else if (player_ctr.level_timer >= 15 && player_ctr.level_timer < 20)
            BLOCK_HEIGHT = BLOCK_HEIGHT_X * Random.Range(-1, 1);
        else
        {
            BLOCK_HEIGHT = 0.2f;
        }
        float block_generate_x = player.transform.position.x;
        block_generate_x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f;
        while (last_block.position.x < block_generate_x)
        {
            CreateFloorBlock();
        }
    }
}
