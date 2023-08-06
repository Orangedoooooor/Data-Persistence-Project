using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string PlayerName;
    private void Awake() //������Ʈ�� �������ڸ��� ȣ���
    {
        if (Instance != null) // �̱���: ���� �ν��Ͻ��� �����ϵ�����
        {
            Destroy(gameObject);
            return;
        }
        Instance = this; // �ٸ� ��ũ��Ʈ���� MainManager.Instance ȣ���ϰ� Ư���ν��Ͻ��� ���� ��ũ�� �����ü� ����
        DontDestroyOnLoad(gameObject); //���� ����ɶ� �̽�ũ��Ʈ�� ����� MainManager ���� ������Ʈ�� �ı����� ����
    }
}
