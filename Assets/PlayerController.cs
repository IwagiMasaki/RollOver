using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float PlayerSpeed; //プレイヤーのスピード
    [SerializeField] private Rigidbody rgb;     //Rigidbodyを取得

    [SerializeField, Label("死亡時エフェクト")] private ParticleSystem EndParticle;
    [SerializeField, Label("リスタート時エフェクト")] private ParticleSystem StartParticle;
    [SerializeField, Label("プレイヤーのメッシュ")] private MeshRenderer PlayerMR;

    [SerializeField, Label("EndScreen")] private EndScreen EndScript;
    [SerializeField, Label("Pause")] private TimeStop Pause;

    [SerializeField, Label("BGM")] private BGM Bgm;
    [SerializeField, Label("SE")] private SoundEfect SE;

    public bool InputFlag;
    public int PlayerHP; //プレイヤーの体力　参照先 → Restart.cs

    private bool ClearFalg;

    Vector3 reStartPosition;

    public int ReSourcePoint;

    // Start is called before the first frame update
    void Start()
    {

        ClearFalg = false;
        ReSourcePoint = 0;
        InputFlag = false;
        PlayerMR.enabled = false;
        Instantiate(StartParticle, gameObject.transform.position, Quaternion.Euler(90, 0, 0f));
        Invoke("InputOn", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(InputFlag) PlayerMove();
        if(InputFlag) PlayerMoveDeb();

        if (PlayerHP == 0)
        {
            Instantiate(EndParticle, gameObject.transform.position, Quaternion.Euler(90,0,0f));
            Bgm.SoundVolumeDown();
            SE.GameOverSound();
            gameObject.SetActive(false);
            EndScript.GameOver();
        }

        if (ClearFalg)
        {
            EndScript.GameClear();
        }
    }

    void PlayerMove()
    {
        var m_GyroVector = Vector3.zero;

        //ジャイロでx,y軸を取得
        m_GyroVector.x = Input.acceleration.x;
        m_GyroVector.z = Input.acceleration.y;

        if (m_GyroVector.sqrMagnitude > 1)
            m_GyroVector.Normalize();

        //取得した値を加速度に代入する
        //rgb.velocity = m_GyroVector * PlayerSpeed * Time.deltaTime;

        rgb.AddForce(m_GyroVector * PlayerSpeed * Time.deltaTime, ForceMode.Force);
    }

    void PlayerMoveDeb()
    {
        var m_GyroVector = Vector3.zero;

        //ジャイロでx,y軸を取得
        m_GyroVector.x = Input.GetAxis("Horizontal") / 2;
        m_GyroVector.z = Input.GetAxis("Vertical") / 2;

        if (m_GyroVector.sqrMagnitude > 1)
            m_GyroVector.Normalize();

        //取得した値を加速度に代入する
        //rgb.velocity = m_GyroVector * PlayerSpeed * Time.deltaTime;

        //速度に制限をかける
        m_GyroVector.x = Mathf.Clamp(m_GyroVector.x, -0.5f, 0.5f);
        m_GyroVector.y = Mathf.Clamp(m_GyroVector.y, -0.5f, 0.5f);

        rgb.AddForce(m_GyroVector * PlayerSpeed * Time.deltaTime, ForceMode.Force);
    }

    //スタート用
    public void InputOn()
    {
        InputFlag = true;
        PlayerMR.enabled = true;
    }

    public void HPDown()
    {
        if(ReSourcePoint >= 3)
        {
            ReSourcePoint -= 3;
        }
        else
        {
            PlayerHP--;
            if (PlayerHP != 0)
            {
                SE.DamageSound();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //砲台に触れたら自身に力を加えて飛ばす
        if (collision.gameObject.tag == "Cannon")
        {
            if (InputFlag)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                rgb.AddForce(new Vector3(0, 35, 35), ForceMode.Impulse);
                InputFlag = false;
                StartCoroutine(CanonCoroutine());
            }
        }

        //敵に触れたらHPを減らす
        if(collision.gameObject.tag == "Enemy")
        {
            if(PlayerHP > 0)
            {
                HPDown();
            }
        }

        //ゴールについたらクリアのフラグをTrueにする(ステージモード用）
        if(collision.gameObject.tag == "Goal")
        {
            InputFlag = false;
            ClearFalg = true;

            Bgm.SoundVolumeDown();

            //一時停止ボタンを消しておく（UI重複を避けるため）
            Pause.ButtonOff();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //動く床に乗った時
        if (collision.gameObject.name == "SideMoveCube")
        {
            rgb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //動く床から離れたとき
        if (collision.gameObject.name == "SideMoveCube")
        {
            rgb.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fire")
        {
            HPDown();
        }

        if (other.gameObject.tag == "Resource")
        {
            ReSourcePoint++;
            Destroy(other.gameObject);
        }
    }

    IEnumerator CanonCoroutine()
    {
        yield return new WaitForSeconds(7);
        InputFlag = true;
    }
}
