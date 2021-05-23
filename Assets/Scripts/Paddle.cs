using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    public bool isStart;
    public GameManager gm;

    // Update is called once per frame
    void Update()
    {
        Vector3 temPos = this.transform.position;
        if (Input.GetKey(KeyCode.D))
        {
            temPos.x += speed * Time.deltaTime;
        }else if (Input.GetKey(KeyCode.A))
        {
            temPos.x -= speed * Time.deltaTime;
        }
        if (temPos.x < -8)
        {
            temPos.x = -8;
        }else if(temPos.x>8)
        {
            temPos.x = 8;
        }
        this.transform.position = temPos;

        if (!isStart)
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                isStart = true;
                gm.ballScript.direction.y = -1.0f;
            }

            temPos.z += 1;
            gm.ballScript.transform.position = temPos;
        }
    }
}
