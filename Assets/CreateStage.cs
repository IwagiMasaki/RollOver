using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStage : MonoBehaviour
{

    [SerializeField] private GameObject[] Stage;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject NullObject;
    
    private List<GameObject> NullObjectList = new List<GameObject>();

    float createPosition = 35f;
    float createPoint = 40;

    int listnumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        RandomCreate();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.z > createPoint)
        {
            createPosition += 100;
            RandomCreate();
            createPoint += 100;
        }
    }

    void RandomCreate()
    {
        //ステージ格納用の空オブジェクトを生成
        NullObjectList.Add(Instantiate(NullObject, Vector3.zero, Quaternion.identity));
        

        for (int i = 0; i < 2; i++)
        {
            //ランダムでステージを選出
            int randomCreate = Random.Range(0, 4);

            //選出されたステージを指定の位置に生成（ステージサイズ50）
            var Obj = Instantiate(Stage[randomCreate], new Vector3(0, 0, i * 50 + createPosition), Quaternion.identity);

            //生成したステージを空のobjectに格納
            Obj.transform.parent = NullObjectList[listnumber].transform;
        }

        if (listnumber > 1)
        {
            //リストに登録した空オブジェクトを削除していく
            Destroy(NullObjectList[listnumber - 2]);
        }
        listnumber += 1;
    }
}
