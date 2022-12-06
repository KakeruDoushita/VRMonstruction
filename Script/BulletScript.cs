using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //弾のエフェクト
    [SerializeField]
    [Header("弾のエフェクト")]
    public GameObject Bulleteffect;

    //プールマネージャークラスのプールマネージャーを追加する
    ObjectPoolManager poolManager;

    //弾のスピード
    [SerializeField]
    [Header("弾のスピード")]
    private float Spead;

    //リジットボディー
    private Rigidbody myRb;

    // Start is called before the first frame update
    void Start()
    {
        //リジットボディを取得
        myRb = GetComponent<Rigidbody>();
        //オブジェクトプールマネージャーをゲットしていく
        poolManager = transform.parent.GetComponent<ObjectPoolManager>();
        //ここでfarceにしている
        gameObject.SetActive(false);
    }


    //弾の貸出処理
    public void ShowInStage(Vector3 _pos, Vector3 _vec)
    {
        transform.position = _pos;

        myRb.velocity = _vec * Spead;
    }

    //弾を返す処理
    public void HideFromStage()
    {
        poolManager.bulletCollect(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        //タグで限定（他のオブジェクトに衝突した場合は呼び出さない
        {
            Instantiate(Bulleteffect, transform.position, Quaternion.identity);//ぶつかった位置にBulleteffectというprefabを配置する
            HideFromStage();
        }
    }
}
