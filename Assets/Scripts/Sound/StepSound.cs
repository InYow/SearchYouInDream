using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour
{
    public void Text()
    {
        Collider2D collider2D = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("StepSoundTrigger"));
        if (collider2D != null)
        {
            Debug.Log(collider2D.gameObject.name);

            string theName = collider2D.gameObject.name;
            if (theName == "Grass")
            {
                //TODO 草地脚步声
                SoundManager_New.Play("草地脚步声");
            }
            else if (theName == "Stone")
            {
                //TODO 石板
                SoundManager_New.Play("石板脚步声");
            }
            else if (theName == "RoughStone")
            {
                //TODO 石板
                SoundManager_New.Play("粗糙石质脚步声");
            }
            else if (theName == "Wood")
            {
                //TODO 
            }
        }
        else
        {
            //TODO播放默认脚步声
        }
        Debug.Log("发出声音");
    }
}
