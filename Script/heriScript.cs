using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heriScript : MonoBehaviour
{
    //ヘリの爆発エフェクト
    [Header("ヘリの爆発エフェクト")]
    public GameObject herieffect;

    [SerializeField]
    [Header("右の弾の発射場所")]
    private GameObject RightFiringPoint;

    [SerializeField]
    [Header("左の弾の発射場所")]
    private GameObject LeftFiringPoint;

    //ヘリのスピード
    [SerializeField]
    [Header("ヘリのスピード")]
    float Speed = 50;

    [SerializeField]
    [Header("弾の発射間隔")]
    float Bulletfiringinterval = 100;

    //目標の座標を入れる場所
    private Transform playerHead;

    //弾のインターバル
    private int count;

    //リスポーン用のスクリプト
    AttackHelicopterScript AttackPoot;

    //Rigidbodyの入れる場所
    private Rigidbody myRb;

    //オブジェクトプールの取得場所変数
    ObjectPoolManager objectPool;

    //簡略化
    [SerializeField]
    private GameObject pool;

    //SEを取得する変数
    YoshigaSoundManager pySE;

    //ヒエラルキーのサウンドマネージャー
    private GameObject soundManager;

    //距離感をランダムで入れる場所
    float Distance = 0;

    bool isFollowStarted = false;

    //プレイヤーの頭
    Transform target;   // 向かう場所

    private void Awake()
    {
        //RIgidbodyの入れるところ
        myRb = GetComponent<Rigidbody>();

        //ObjectPoolの名前を持つアクティブなオブジェクトの中から探して取得する
        //指定した名前のオブジェクトを存在するすべてのアクティブなオブジェクトの中から探し出して取得する
        pool = GameObject.Find("Doushita_ObjectPool");

        //オブジェクトプールを取得
        objectPool = pool.GetComponent<ObjectPoolManager>();
    }

    void Start()
    {
        //YoshigaSoundManagerを取得する
        soundManager = GameObject.Find("SoundManager");
        pySE = soundManager.GetComponent<YoshigaSoundManager>();

        //プレイヤーの頭の座標を入れる
        playerHead = GameObject.FindGameObjectWithTag("playerhead").transform;

        //ここでfarceにしている
        gameObject.SetActive(false);
    }

    //戦闘ヘリを進ませる処理
    private void FixedUpdate()
    {
        // プレイヤーの方向
        Vector3 pDir = new Vector3(playerHead.position.x, transform.position.y, playerHead.position.z);
        //二点間の距離を求める
        float distance_two = Vector3.Distance(transform.position, playerHead.transform.position);
        
        //プレイヤーの方を向いておく
        transform.LookAt(pDir);
        
        // プレイヤーの頭との距離が離れている場合には、プレイヤーの頭に近く。
        if (distance_two >= Distance)
        {
            //特定のオブジェクトの方向を向かせる
            Vector3 DIS = Vector3.Normalize(playerHead.position - transform.position);//ヘリから見たプレイヤーとの距離
            //transform.LookAt(rDir);//自分の向かっているベクトルを向かせるようにする
            myRb.velocity = DIS * Speed;
        }
        else //if (distance_two < Distance)//一定の距離間になったら回りながら、弾を出す
        {
            myRb.velocity = Vector3.zero;//Veloctyをゼロにする

            //ここでfollow処理を呼ぶ
            if (!isFollowStarted)
            {
                gameObject.GetComponent<Follow>().SetFollowActive(true);
                isFollowStarted = true;
            }

            // 攻撃開始
            count += 1;
            if (count % Bulletfiringinterval == 0)
            {
                // 弾を発射する
                LauncherShot();
            }
        }
    }

    //ReSpownのInitのやつ
    public void Init(AttackHelicopterScript ReSpown)
    {
        AttackPoot = ReSpown;
    }

    //ヘリの貸出処理
    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;
    }

    //ヘリを返す処理
    public void HideFromStage()
    {
        //gameObject.SetActive(false);
        objectPool.heriCollect(this);
    }

    //弾を打ち出す処理
    public void LauncherShot()
    {
        // 弾を発射する場所を取得
        Vector3 bulletPosition = RightFiringPoint.transform.position;

        //オブジェクトプールで呼び出した玉を打つ
        objectPool.bulletLaunch(bulletPosition, Vector3.Normalize(playerHead.position - bulletPosition));

        // 弾を発射する場所を取得
        Vector3 bulletPosition1 = LeftFiringPoint.transform.position;

        //オブジェクトプールで呼び出した玉を打つ
        objectPool.bulletLaunch(bulletPosition1, Vector3.Normalize(playerHead.position - bulletPosition1));

    }

    //ヘリは破壊用スクリプト
    public void heriDestoroy()
    {
        //ヘリの回収処理
        HideFromStage();
        Instantiate(herieffect, this.transform.position, Quaternion.identity);//ぶつかった位置を爆発させる
        TelopManager.Instance.BreakNormalObject(TelopManager.BREAKABLE_OBJECTS.HELICOPTER);//おーちゃんの処理の奴
    }

    ////IdxをSetする
    //public void SetIdx(int idx)
    //{
    //    myIdx = idx;
    //}

    //target
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    //setDisance距離感を維持する
    public void SetDistance(float val)
    {
        Distance = val;
    }

    void OnTriggerEnter(Collider other)//壊された時の処理
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "HeatBeam")
        {
            //ヘリの回収処理
            AttackPoot.AttackDesCount();

            gameObject.GetComponent<Follow>().SetFollowActive(false);
            isFollowStarted = false;
            Instantiate(herieffect, this.transform.position, Quaternion.identity);//ぶつかった位置を爆発させる
            TelopManager.Instance.BreakNormalObject(TelopManager.BREAKABLE_OBJECTS.HELICOPTER);//おーちゃんの処理の奴
            HideFromStage();
        }
    }
}