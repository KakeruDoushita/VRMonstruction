using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBullet : MonoBehaviour
{
    // 追尾対象
    [Tooltip("追尾対象")]
    public GameObject target;

    // 本体情報
    Transform bodyTF;
    Rigidbody bodyRB;
    Collider bodyCol;

    // 進行方向(ワールド空間)
    [SerializeField]
    [Tooltip("進行方向(ワールド空間)")]
    Vector3 movevec;

    // 加速度の大きさ
    [Tooltip("加速度の大きさ")]
    public float acceleration;

    // 最高速度
    [Tooltip("最高速度")]
    public float maxSpeed;

    //衝突後のエフェクト入れる場所
    [Tooltip("衝突後のエフェクト入れる場所")]
    public GameObject explosion;//effectにはunity上でprefabを関連付けます

    [SerializeField]
    [Tooltip("回転するスピード")]
    float Rotationspeed;


    // Start is called before the first frame update
    void Start()
    {
        bodyTF = transform.parent;//親のオブジェクト
        bodyRB = transform.parent.GetComponent<Rigidbody>();
        bodyCol = transform.parent.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // 追尾対象がいたら追尾する
        if (target)
        {
            // 進行方向を取得する
            Vector3 distance = target.transform.position - bodyTF.position;
            movevec = Vector3.Normalize(distance);

            // ジョジョに回転させましょう
            //transform.rotation = Quaternion.RotateTowards(transform.rotation,//自分の回転位置
                                             //Quaternion.Euler(movevec),//回転する角度
                                             //Rotationspeed);// 回転するスピード
            bodyTF.LookAt(movevec);

        }
        // 追尾対象がいなかったらそのまま進む
        else
        {
            // 進行方向を取得する
            movevec = bodyTF.forward;
        }
        // 加速させる
        bodyRB.AddForce(movevec * acceleration);

        // 速すぎたら速度を補正する
        float mag = bodyRB.velocity.magnitude;
        if (mag > maxSpeed)
        {
            bodyRB.velocity *= maxSpeed / mag;
        }
    }

    // 索敵範囲にプレイヤーが入ったら追跡する
    void OnTriggerEnter(Collider other)
    {
        if (other == bodyCol || other.tag != "playerhead") return;
        target = other.gameObject;
    }

    // 追跡対象が索敵範囲から外れたら追跡しない
    void OnTriggerExit(Collider other)
    {
        target = null;
    }

    void OnCollisionEnter(Collision collision) //衝突時の処理
    {

        //Destroy(this.gameObject);
        if (collision.gameObject.tag == "playerhead")
        //タグで限定（他のオブジェクトに衝突した場合は呼び出さない
        {
            Debug.Log("atata");
            Destroy(gameObject);//ミサイル

            Instantiate(explosion, transform.position, Quaternion.identity);//ぶつかった位置にexplosionというprefabを配置する

        }
    }

}
