using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGimmick : MonoBehaviour
{

    [Header("リスト番号は１～４まで")]
    [SerializeField, Label("ギミックのリスト番号")] private int ListNumber;

#if UNITY_EDITOR
    [NamedArrayAttribute(new string[] { "MoveCube", "SideMoveCube", "Wind", "Rocket", "RotetionRod", "RandomRocket", "Enemy", "Fire" })]
#endif
    [SerializeField, Label("ギミック")] private GameObject[] Gimmick;


    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        //ステージの番号によって置くギミックを変える
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
