using UnityEngine;
using UnityEngine.Events;           // �ƥ�
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �^�X���
    /// </summary>
    public Turn turn = Turn.My;

    [Header("�Ĥ�^�X�ƥ�")]
    public UnityEvent onEnemyTurn;
    [Header("�Ǫ��}�C")]
    public GameObject[] goEnemys;
    [Header("�u�]")]
    public GameObject goMarble;
    [Header("�ѽL�s��")]
    public Transform traCheckboard;
    [Header("�ͦ��ƶq�̤p�̤j��")]
    public Vector2Int v2RandomEnemyCount = new Vector2Int(1, 7);
    [SerializeField]
    private Transform[] traCheckboards;
    [SerializeField]
    /// <summary>
    /// �ĤG�C�G�ͦ��Ǫ����ѽL
    /// </summary>
    private Transform[] traColumnSecond;
    /// <summary>
    /// �ѽL���ƶq
    /// </summary>
    private int countRow = 8;
    /// <summary>
    /// �ĤG�C�����ޭȡG�B�z�Ǫ��ͦ�������
    /// </summary>
    [SerializeField]
    private List<int> indexColumnSecond = new List<int>();

    private void Awake()
    {
        // �ѽL�}�C = �ѽL�s��.���o�l���󪺤���<�ܧΤ���>()
        traCheckboards = traCheckboard.GetComponentsInChildren<Transform>();

        // ��l�ĤG�C�ƶq
        traColumnSecond = new Transform[countRow];
        // ���o�ĤG�C���ѽL
        for (int i = 9; i < 9 + countRow; i++)
        {
            traColumnSecond[i - countRow - 1] = traCheckboards[i];
        }

        SpawnEnemy();
    }

    /// <summary>
    /// �ͦ��ĤH�G�H���ƶq v2RandomEnemyCount
    /// </summary>
    private void SpawnEnemy()
    {
        int countEnemy = Random.Range(v2RandomEnemyCount.x, v2RandomEnemyCount.y);

        indexColumnSecond.Clear();                                                          // �M���W���Ѿl�����

        for (int i = 0; i < 8; i++) indexColumnSecond.Add(i);                               // ��l�Ʀr 0 ~ 7

        for (int i = 0; i < countEnemy; i++)
        {
            int randomEnemy = Random.Range(0, goEnemys.Length);                             // 0 ~ 2 - �H�� 0 �� 1

            int randomColumnSecond = Random.Range(0, indexColumnSecond.Count);              // �H���ĤG�C�����ޭȡG�Ĥ@�� 0 ~ 7 �A�ĤG����Ѿl���ƶq�H����

            Instantiate(goEnemys[randomEnemy], traColumnSecond[indexColumnSecond[randomColumnSecond]].position, Quaternion.identity);

            indexColumnSecond.RemoveAt(randomColumnSecond);                                 // �R���w�g��m�Ǫ����ĤG�C�ѽL
        }

        int randomMarble = Random.Range(0, indexColumnSecond.Count);                        // �Ѿl���ѽL ���o�H���@��
        Instantiate(
            goMarble,
            traColumnSecond[indexColumnSecond[randomMarble]].position + Vector3.up,
            Quaternion.identity);                                                           // �ͦ��u�]�b�ѽL�W
    }

    /// <summary>
    /// �����^�X
    /// </summary>
    /// <param name="isMyTurn">�O�_���a�^�X</param>
    public void SwitchTurn(bool isMyTurn)
    {
        if (isMyTurn) turn = Turn.My;
        else
        {
            turn = Turn.Enemy;
            onEnemyTurn.Invoke();
        }
    }
}

/// <summary>
/// �^�X�G�ڤ�P�Ĥ�
/// </summary>
public enum Turn
{
    My, Enemy
}