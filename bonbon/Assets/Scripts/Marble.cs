using UnityEngine;

/// <summary>
/// �u�]�t��
/// </summary>
public class Marble : MonoBehaviour
{
    /// <summary>
    /// �����O
    /// </summary>
    public float attack;

    private void Awake()
    {
        // ���z.�����ϼh�I��(A �ϼh�AB�ϼh) ���� A B �ϼh�I��
        Physics.IgnoreLayerCollision(6, 6);
    }
}