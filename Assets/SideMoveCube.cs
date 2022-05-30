using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMoveCube : MonoBehaviour
{

    [SerializeField, Label("端から端までの時間")] private float TurnTime;
    [SerializeField, Label("移動スピード")] private float MoveSpeed;

    [SerializeField, Label("x軸初期設定")] private float StartPositionX;
    [SerializeField, Label("z軸初期設定")] private float StartPositionZ;

    private float m_time = 0;
    private Rigidbody rgb;

    float px = 0;

    bool Rightflag = true;
    bool Leftflag = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(StartPositionX, transform.position.y, StartPositionZ);
        rgb = GetComponent<Rigidbody>();
        StartCoroutine(SideMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RightMove();
        LeftMove();
    }

    
    //右にスライド
    void RightMove()
    {
        if (Rightflag)
        {
            px = Mathf.Min(m_time / 2f, 1f);
            float easeinout = (px * px) * (3f - (2f * px));
            rgb.MovePosition(transform.position + transform.right * MoveSpeed * Time.deltaTime);
            m_time = Mathf.Clamp(m_time += 0.2f * Time.deltaTime, 0, 2);
        }
        else
        {
            return;
        }
    }

    //左にスライド
    void LeftMove()
    {
        if (Leftflag)
        {
            px = Mathf.Min(m_time / 2f, 1f);
            float easeinout = (px * px) * (3f - (2f * px));
            rgb.MovePosition(transform.position - transform.right * MoveSpeed * Time.deltaTime);
            m_time = Mathf.Clamp(m_time += 0.2f * Time.deltaTime, 0, 2);
        }
        else
        {
            return;
        }
    }

    //左右のスライドを繰り返す。スライドが完了した後に少し止まる時間を作る
    IEnumerator SideMove()
    {
        yield return new WaitForSeconds(TurnTime);
        px = 0;
        m_time = 0;
        Rightflag = false;
        Leftflag = true;

        yield return new WaitForSeconds(TurnTime);
        px = 0;
        m_time = 0;
        Leftflag = false;
        Rightflag = true;

        StartCoroutine(SideMove());
    }
}
