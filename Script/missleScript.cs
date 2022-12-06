using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class missleScript : MonoBehaviour
{
    //速度
    [SerializeField]
    [Header("ミサイルのスピード")]
    float MiSpeed;

    //曲がるときの速度
    [SerializeField]
    [Header("曲がるときの速度")]
    float LpSpeed;

    //軌道が変わる距離  yの値が150上がるなら250上げる
    [SerializeField]
    [Header("軌道が変わる距離  yの値が150上がるなら250上げる")]
    float Distance;

    //衝突後のエフェクト入れる場所
    [Header("衝突後のエフェクト入れる場所")]
    public GameObject explosion;//effectにはunity上でprefabを関連付けます

    //Rigidbodyを入れる
    Rigidbody myRb;

    //プールマネージャークラスのプールマネージャーを追加する
    ObjectPoolManager poolManager;

    //二点間の距離を入れる
    private float distance_two;

    //ミサイルリスポーン地点を入れる場所
    MissleReSpwon misslepoot;

    //目標の座標を入れる場所
    private Transform PlayerHead;

    ////プレイヤーの腕の威力を入れる
    Vector3 playerArm;

    void Start()
    {
        //RIgidbodyを設定
        myRb = GetComponent<Rigidbody>();

        //オブジェクトプールマネージャーをゲットしていく
        poolManager = transform.parent.GetComponent<ObjectPoolManager>();

        //ここでfarceにしている
        gameObject.SetActive(false);
    }

    private void FixedUpdate()//フィックスドアップデートは一定間隔で呼ばれているように見せる処理だから物理計算や移動などの計算に用いる
    {
        //プレイヤーの頭の座標を入れる
        PlayerHead = GameObject.FindGameObjectWithTag("playerhead").transform;

        // 追従対象オブジェクトのTransformから、目的地を算出
        Vector3 targetPos = PlayerHead.TransformPoint(new Vector3(0f, 0f, 0f));

        //二点間の距離を代入(スピード調整に使う)
        distance_two = Vector3.Distance(transform.position, targetPos);

        // 現在の位置(ミサイルのスピードを変える処理)
        float present_Location = (Time.deltaTime * LpSpeed) / distance_two;

        if (distance_two > Distance)//領域に入ったら
        {
            Vector3 dir = new Vector3(PlayerHead.transform.position.x, transform.position.y, PlayerHead.transform.position.z);
            //特定のオブジェクトの方向を向かせる
            transform.LookAt(dir);//自分の向かっているベクトルを向かせるようにする

            myRb.velocity = MiSpeed * dir;
        }
        else//領域に入る前
        {
            //ここでAddForceの力を切る
            myRb.velocity = Vector3.zero;

            //特定のオブジェクトの方向を向かせる
            transform.LookAt(targetPos);//自分の向かっているベクトルを向かせるようにする
            transform.position = Vector3.Lerp(transform.position, targetPos, present_Location);
        }
    }

    public void Init(MissleReSpwon ReSpown)
    {
        misslepoot = ReSpown;
    }

    //ミサイルの貸出処理
    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;

        //myRb.velocity = _vec * MiSpeed;
    }

    //ミサイルを返す処理
    public void HideFromStage()
    {
        poolManager.missleCollect(this);
    }


    void OnTriggerEnter(Collider other) //衝突時の処理
    {
        if (other.gameObject.tag == "Player")
        //タグで限定（他のオブジェクトに衝突した場合は呼び出さない
        {
            //ここで壊れたという指示をReSpwonに送る
            misslepoot.MissleDesCount();

            HideFromStage();

            Instantiate(explosion, transform.position, Quaternion.identity);//ぶつかった位置にexplosionというprefabを配置する
        }
        //ミサイルがはじかれた時処理
        if (other.gameObject.tag == "PlayerArm")
        {
            //otherをヌルチェックする
            //ここでplayerArmの中に何か入ってないかチェックする
            var playerArmScript = other.GetComponent<PlayerArm>();

            if (playerArmScript != null)
            {
                playerArm = playerArmScript.CtrlVelocity;
            }
            myRb.velocity = playerArm;
        }
    }
}