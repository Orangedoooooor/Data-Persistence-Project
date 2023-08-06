using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string PlayerName;
    private void Awake() //오브젝트가 생성되자마자 호출됨
    {
        if (Instance != null) // 싱글톤: 단일 인스턴스만 존재하도록함
        {
            Destroy(gameObject);
            return;
        }
        Instance = this; // 다른 스크립트에서 MainManager.Instance 호출하고 특정인스턴스에 대한 링크를 가져올수 있음
        DontDestroyOnLoad(gameObject); //씬이 변경될때 이스크립트에 연결된 MainManager 게임 오브젝트를 파괴하지 않음
    }
}
