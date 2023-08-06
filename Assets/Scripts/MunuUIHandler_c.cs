using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// �⺻ ��ũ��Ʈ�� ��� ����� ���Ŀ� �� ��ũ��Ʈ�� ����ǵ��� ���� 
// UI�� �����ϱ� ���� �ٸ� �׸��� �ʱ�ȭ�ؾ� �� �� �����Ƿ� UI�� ����
[DefaultExecutionOrder(1000)]
public class MunuUIHandler_c : MonoBehaviour
{
    [SerializeField] private InputField InputPlayerName;
    public TextMeshProUGUI BeforeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartNewGame()
    {
        if (!string.IsNullOrEmpty(InputPlayerName.text))
        {
            MenuManager.Instance.PlayerName = InputPlayerName.text;
            SceneManager.LoadScene(1);
        }
        else
        {
            BeforeText.gameObject.SetActive(true);
        }
    }
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
