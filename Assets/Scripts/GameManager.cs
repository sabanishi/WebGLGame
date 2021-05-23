using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject WallR;
    public GameObject WallL;
    public GameObject WallT;
    public GameObject WallB;
    public GameObject Paddle;
    public GameObject Ball;

    public GameObject BlockPrefab;
    public GameObject ItemPrefab;

    public Ball ballScript;
    public Paddle paddleScript;

    public Text scoreText;
    public long score;

    public GameObject result;
    public Text resultText;
    public int blockCnt;

    public GameObject Pause;
    public Text PauseText;
    private long pauseCount;
    private bool isPause;

    public List<Item> Items = new List<Item>();

    private bool isFinish;

    public AudioClip scoreUp;
    public AudioClip scoreDown;
    public AudioClip hit;
    public AudioClip hit2;
    public AudioClip finish;
    public AudioClip wall;
    public AudioClip block;
    public AudioSource audioSource;

    private void Start()
    {
        ballScript = Ball.GetComponent<Ball>();
        paddleScript = Paddle.GetComponent<Paddle>();
        float w = BlockPrefab.transform.localScale.x * 1.1f;
        float h = BlockPrefab.transform.localScale.z * 1.3f;

        for(int i = -3; i <= 3; i++)
        {
            for(int j = -2; j <= 2; j++)
            {
                GameObject obj=Instantiate(BlockPrefab, new Vector3(i * w,0, j * h+10.0f),Quaternion.identity);
                obj.GetComponent<Block>().gm = this;
            }
        }
        Time.timeScale = 1.0f;

       
    }

    private void Update()
    {
        scoreText.text = score.ToString();

        if (blockCnt >= 35&&!isFinish)
        {
            resultText.text = "Your Score is " + score.ToString();
            result.SetActive(true);
            foreach(var obj in Items)
            {
                Destroy(obj);    
            }
            isFinish = true;
            audioSource.PlayOneShot(finish);
        }

        if (isFinish)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                Pause.SetActive(true);
                this.isPause = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                Pause.SetActive(false);
                this.isPause = false;
            }
        }

        if (isPause)
        {
            if (pauseCount % 10 == 0)
            {
                if (PauseText.color == Color.red)
                {
                    PauseText.color = Color.blue;
                }
                else
                {
                    PauseText.color = Color.red;
                }
            }
            pauseCount++;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}

