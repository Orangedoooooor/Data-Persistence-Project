using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public string PlayerName;
    public string BestPlayerName;
    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int BestPoint;
    
    private bool m_GameOver = false;

    public static MainManager Instance;

    private void Awake()
    {
        LoadBestPlayer();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerName = MenuManager.Instance.PlayerName;
        ScoreText.text = PlayerName+ " Score : 0";
        if(BestPlayerName != null)
        {
            BestScoreText.text = "Best Score :" + BestPlayerName + " : " + BestPoint;
        }
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        if(PlayerName != null)
        {
            ScoreText.text = PlayerName + $" Score : {m_Points}";
        }
        else
        {
            ScoreText.text = $"Player Score : {m_Points}";
        }
        
    }

    public void GameOver()
    {
        if(m_Points>BestPoint)
        {
            SaveBestPlayer();
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
    [System.Serializable]
    class SaveData
    {
        public int maxPoints;
        public string BestPlayerName;
    }
    public void SaveBestPlayer()
    {
        SaveData data = new SaveData();
        data.maxPoints = m_Points;
        data.BestPlayerName = PlayerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json",json);
    }
    public void LoadBestPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestPoint = data.maxPoints;
            BestPlayerName = data.BestPlayerName;
        }
    }
}
