using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGimmick : MonoBehaviour
{

    [Header("���X�g�ԍ��͂P�`�S�܂�")]
    [SerializeField, Label("�M�~�b�N�̃��X�g�ԍ�")] private int ListNumber;

#if UNITY_EDITOR
    [NamedArrayAttribute(new string[] { "MoveCube", "SideMoveCube", "Wind", "Rocket", "RotetionRod", "RandomRocket", "Enemy", "Fire" })]
#endif
    [SerializeField, Label("�M�~�b�N")] private GameObject[] Gimmick;


    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�W�̔ԍ��ɂ���Ēu���M�~�b�N��ς���
        switch (ListNumber)
        {
            case 1:
                SetGimmickStageOne();
                break;

            case 2:
                SetGimmickStageTwo();
                break;

            case 3:
                SetGimmickStageThree();
                break;

            case 4:
                SetGimmickStageFour();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetGimmickStageOne()
    {
        obj = Instantiate(Gimmick[Random.Range(0, 2)], transform.position, transform.rotation);
        obj.transform.parent = gameObject.transform;
    }

    void SetGimmickStageTwo()
    {
        obj = Instantiate(Gimmick[Random.Range(2, 4)], transform.position, transform.rotation);
        obj.transform.parent = gameObject.transform;
    }

    void SetGimmickStageThree()
    {
        obj = Instantiate(Gimmick[Random.Range(4, 6)], transform.position, transform.rotation);
        obj.transform.parent = gameObject.transform;
    }

    void SetGimmickStageFour()
    {
        obj = Instantiate(Gimmick[Random.Range(6, 8)], transform.position, transform.rotation);
        obj.transform.parent = gameObject.transform;
    }
}
