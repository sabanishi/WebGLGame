using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameManager gm;
    public ItemEnum itemEnum;
    public Vector2 direction;
    public float speed;
    void Start()
    {
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            itemEnum = ItemEnum.ScoreUp;
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            itemEnum = ItemEnum.ScoreDown;
            GetComponent<Renderer>().material.color = Color.blue;
        }
        direction.y = -1.0f;
        speed = 5;
        gm.Items.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z - transform.localScale.z / 2 <= gm.WallB.transform.position.z + gm.WallB.transform.localScale.z / 2)
        {
            this.Effect();
        }

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
            gm.audioSource.PlayOneShot(gm.hit2);
            if (this.itemEnum.Equals(ItemEnum.ScoreDown))
            {
                itemEnum = ItemEnum.ScoreUp;
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                itemEnum = ItemEnum.ScoreDown;
                GetComponent<Renderer>().material.color = Color.blue;
            }
        }
        Vector3 temPos = this.transform.position;
        temPos.x += direction.x * speed * Time.deltaTime;
        temPos.z += direction.y * speed * Time.deltaTime;
        this.transform.position = temPos;


    }
    private void Effect()
    {
        if (this.itemEnum.Equals(ItemEnum.ScoreUp))
        {
            gm.score *= 2;
            gm.audioSource.PlayOneShot(gm.scoreUp);
        }
        else
        {
            gm.score /= 2;
            gm.audioSource.PlayOneShot(gm.scoreDown);
        }
        this.gameObject.SetActive(false);
    }

    private void CalculateAngle()
    {
        float pX = gm.Paddle.transform.position.x;
        float mX = this.transform.position.x;
        float x = mX - pX;
        Debug.Log(gm.Paddle.transform.localScale.x + "  " + x);
        float Angle = Mathf.Atan2(gm.Paddle.transform.localScale.x / 2, x);
        this.direction.x = Mathf.Cos(Angle);
        this.direction.y = Mathf.Sin(Angle);
    }
}
