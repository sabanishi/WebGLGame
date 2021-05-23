using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public Vector2 GetDirection()
    {
        return direction;
    }
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
    public GameManager gm;

    void Start()
    {

        direction.x = 0;
        direction.y = 0;
    }

    void Update()
    {
        if (gm.paddleScript.isStart)
        {
            if (this.transform.position.x + this.transform.localScale.x / 2 >= gm.WallR.transform.position.x - gm.WallR.transform.localScale.x / 2
                         && direction.x > 0)
            {
                direction.x *= -1.0f;
                gm.audioSource.PlayOneShot(gm.wall);
            }
            if (this.transform.position.x - this.transform.localScale.x / 2 <= gm.WallL.transform.position.x + gm.WallL.transform.localScale.x / 2
                && direction.x < 0)
            {
                direction.x *= -1.0f;
                gm.audioSource.PlayOneShot(gm.wall);
            }
            if (this.transform.position.z + this.transform.localScale.z / 2 >= gm.WallT.transform.position.z - gm.WallT.transform.localScale.z / 2
                 && direction.y > 0)
            {
                direction.y *= -1.0f;
                gm.audioSource.PlayOneShot(gm.wall);
            }
            if (
                 this.transform.position.x <= gm.Paddle.transform.position.x + gm.Paddle.transform.localScale.x / 2
                && this.transform.position.x >= gm.Paddle.transform.position.x - gm.Paddle.transform.localScale.x / 2
                && this.transform.position.z - this.transform.localScale.z / 2 <= gm.Paddle.transform.position.z + gm.Paddle.transform.localScale.z / 2
                && this.transform.position.z - this.transform.localScale.z / 2 >= gm.Paddle.transform.position.z + gm.Paddle.transform.localScale.z / 2 - 0.5
                && direction.y < 0
                )
            {
                CalculateAngle();
                gm.audioSource.PlayOneShot(gm.hit);

            }
        }

        if (this.transform.position.z - this.transform.localScale.z / 2 <= gm.WallB.transform.position.z + gm.WallB.transform.localScale.z / 2
           )
        {
            //ボール破壊処理
            gm.blockCnt=35;
        }

        Vector3 temPos = this.transform.position;
        temPos.x += direction.x * speed * Time.deltaTime;
        temPos.z += direction.y * speed * Time.deltaTime;
        this.transform.position = temPos;
    }

    private void CalculateAngle()
    {
        float pX = gm.Paddle.transform.position.x;
        float mX = this.transform.position.x;
        float x = mX - pX;
        float Angle= Mathf.Atan2(gm.Paddle.transform.localScale.x/2, x);
        this.direction.x = Mathf.Cos(Angle);
        this.direction.y = Mathf.Sin(Angle);
    }
}
