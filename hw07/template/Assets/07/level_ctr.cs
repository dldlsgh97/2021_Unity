using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_ctr : MonoBehaviour
{
    public class Block
    {
        public enum TYPE
        {
            NONE = -1,
            FLOOR = 0,
            HOLE,
            NUM
        };
    };
    
    public struct CreateInfo
    {
        public Block.TYPE block_type;
        public int max_count;
        public int height;
        public int current_count;
    };
    public CreateInfo previos_block;
    public CreateInfo current_block;
    public CreateInfo next_block;

    public int level = 0;

    private void ClearNextBlock(ref CreateInfo block)
    {
        block.block_type = Block.TYPE.FLOOR;
        block.max_count = 15;
        block.height = 0;
        block.current_count = 0;

    }
    public void Initialize()
    {
        ClearNextBlock(ref previos_block);
        ClearNextBlock(ref current_block);
        ClearNextBlock(ref next_block);
    }

    private void UpdateLevel(ref CreateInfo current, CreateInfo privious)
    {
        
        

        switch (privious.block_type)
        {
            case Block.TYPE.FLOOR:
                current.block_type = Block.TYPE.HOLE;
 
                if (player_ctr.level_timer >= 0 && player_ctr.level_timer < 30)
                    current.max_count = Random.Range(1, 2);
                else if (player_ctr.level_timer >= 30 && player_ctr.level_timer < 60)
                    current.max_count = Random.Range(2, 3);
                else if (player_ctr.level_timer >= 60 && player_ctr.level_timer < 75)
                    current.max_count = Random.Range(1, 2);
                else
                {
                    player_ctr.level_timer = 0.0f;
                    current.max_count = Random.Range(1, 2);
                }
                current.height = privious.height;
                break;
            case Block.TYPE.HOLE:
                current.block_type = Block.TYPE.FLOOR;
                if (player_ctr.level_timer >= 0 && player_ctr.level_timer < 15)
                    current.max_count = Random.Range(9, 10);
                else if (player_ctr.level_timer >= 15 && player_ctr.level_timer < 30)
                    current.max_count = Random.Range(8, 9);
                else if (player_ctr.level_timer >= 30 && player_ctr.level_timer < 45)
                    current.max_count = Random.Range(7, 8);
                else if (player_ctr.level_timer >= 45 && player_ctr.level_timer < 60)
                    current.max_count = Random.Range(5, 6);
                else if (player_ctr.level_timer >= 60 && player_ctr.level_timer < 75)
                    current.max_count = Random.Range(6, 7);
                else
                {
                    player_ctr.level_timer = 0.0f;
                    current.max_count = Random.Range(9, 10);
                }

                break;
        }
    }

    public void UpdateStatus()
    {
        current_block.current_count++;

        if(current_block.current_count >= current_block.max_count)
        {
            previos_block = current_block;
            current_block = next_block;

            ClearNextBlock(ref next_block);
            UpdateLevel(ref next_block, current_block);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    }
