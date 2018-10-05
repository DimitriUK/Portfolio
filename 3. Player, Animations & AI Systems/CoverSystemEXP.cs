using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoverSystemEXP : MonoBehaviour
{
    public EXPAnim player;
    public Animator anim;

    //This script works as a controller for when the player has interacted with a wall to provide cover. The player will then be able to use the navigation keys to move side to side, or go into crouch and lean around the wall and fire their weapon.

    void Update()
    {
        if (anim.GetBool("isCover") == true)
        {
            if (!player.isCrouching)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    anim.SetTrigger("Left");
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    anim.SetTrigger("Right");
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    anim.SetTrigger("Up");
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    anim.SetTrigger("Down");
                }
            }
        }
    }
}
