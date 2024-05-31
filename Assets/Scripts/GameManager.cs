using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public enum DoorNum
{
    one = 1,
    two = 2,
    three = 3
}
public class GameManager : MonoBehaviour
{
    private static float tryCount = 0;
    private static float correctCount = 0;
    private static float wrongCount = 0;

    public static GameManager Instance;

    [SerializeField]
    private Transform[] spawnPoint;
    [SerializeField]
    private GameObject poro;
    [SerializeField]
    private Text informaitionTxt;
    [SerializeField]
    private Text countTxt;
    [SerializeField]
    private Text correctTxt;
    [SerializeField]
    private Text wrongTxt;
    [SerializeField]
    private Text PercentageTxt;

    [SerializeField]
    private GameObject[] doors;


    private bool[] poroPos = new bool[4];
    private bool gameEnd;
    private int rand;
    private int randOpenNum;


    public bool selected = false;
    public bool second;
    public int choose;
    public Button retryBtn;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        correctTxt.text = "맞은 횟수 : " + correctCount;
        wrongTxt.text = "틀린 횟수 : " + wrongCount;
        if(tryCount > 0)
        PercentageTxt.text = "맞춘 확률 : " + ((correctCount / tryCount) * 100) + "%";
        tryCount++;
        countTxt.text = "시도 횟수 : " + tryCount;
        rand = Random.Range(0, 3);
        poroPos[rand] = true;
        poro.transform.position = spawnPoint[rand].position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameReset()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void RandomDoorOpen()
    {
        //랜덤한 문 열기
        do
        {
            randOpenNum = Random.Range(0, 3);
        }
        while (randOpenNum == rand || randOpenNum == choose);
        informaitionTxt.text = randOpenNum + "번 문에는 포로가 없습니다.\n";
        doors[randOpenNum].SetActive(false);
        informaitionTxt.text = "새로 선택할 번호를 입력해주세요.\n 그대로 진행하시려면 원래 선택했던 번호를 입력해주세요.";
        selected = false;
        second = true;
        
    }
    public void ReChoose()
    {
        doors[choose - 1].SetActive(false);
        if (poroPos[choose - 1])
        {
            informaitionTxt.text = choose + "번 문에는 포로가 있습니다";
            correctCount++;
            correctTxt.text = "맞은 횟수 : " + correctCount;
        }
        else
        {
            informaitionTxt.text = choose + "번 문에는 포로가 없습니다\n포로가 있는 문은" + (rand + 1) + "번 문입니다";
            wrongCount++;
            wrongTxt.text = "틀린 횟수 : " + wrongCount;
        }
        selected = true;
        PercentageTxt.text = "맞춘 확률 : " + ((correctCount / tryCount) * 100) + "%";
        retryBtn.gameObject.SetActive(true);
    }
}
