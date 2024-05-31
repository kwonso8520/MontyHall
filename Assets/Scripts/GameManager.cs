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
        correctTxt.text = "���� Ƚ�� : " + correctCount;
        wrongTxt.text = "Ʋ�� Ƚ�� : " + wrongCount;
        if(tryCount > 0)
        PercentageTxt.text = "���� Ȯ�� : " + ((correctCount / tryCount) * 100) + "%";
        tryCount++;
        countTxt.text = "�õ� Ƚ�� : " + tryCount;
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
        //������ �� ����
        do
        {
            randOpenNum = Random.Range(0, 3);
        }
        while (randOpenNum == rand || randOpenNum == choose);
        informaitionTxt.text = randOpenNum + "�� ������ ���ΰ� �����ϴ�.\n";
        doors[randOpenNum].SetActive(false);
        informaitionTxt.text = "���� ������ ��ȣ�� �Է����ּ���.\n �״�� �����Ͻ÷��� ���� �����ߴ� ��ȣ�� �Է����ּ���.";
        selected = false;
        second = true;
        
    }
    public void ReChoose()
    {
        doors[choose - 1].SetActive(false);
        if (poroPos[choose - 1])
        {
            informaitionTxt.text = choose + "�� ������ ���ΰ� �ֽ��ϴ�";
            correctCount++;
            correctTxt.text = "���� Ƚ�� : " + correctCount;
        }
        else
        {
            informaitionTxt.text = choose + "�� ������ ���ΰ� �����ϴ�\n���ΰ� �ִ� ����" + (rand + 1) + "�� ���Դϴ�";
            wrongCount++;
            wrongTxt.text = "Ʋ�� Ƚ�� : " + wrongCount;
        }
        selected = true;
        PercentageTxt.text = "���� Ȯ�� : " + ((correctCount / tryCount) * 100) + "%";
        retryBtn.gameObject.SetActive(true);
    }
}
