using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstRotate : MonoBehaviour
{
    //プレイヤーの頭の座標
    [Header("プレイヤーの頭の座標")]
    private Transform PlayerHead;

    //プレイヤーの頭との距離
    [SerializeField]
    [Header("プレイヤーの頭との距離")]
    float Distance = 200;

    //角度
    [SerializeField]
    [Header("角度")]
    float Angle = 0;

    //回る速度の大きさの幅小さいほう
    [SerializeField]
    [Header("回る速度の大きさの幅小さいほう")]
    float AddMin = 60;

    //回る速度の大きさの幅大きいほう
    [SerializeField]
    [Header("回る速度の大きさの幅大きいほう")]
    float AddMax = 80;

    private float AddDeg = 80;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHead = GameObject.FindWithTag("playerhead").transform;
        AddDeg = Random.Range(AddMin, AddMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (Angle >= 360)
        {
            Angle = 0;
        }
        else
        {
            Angle += AddDeg * Time.deltaTime;
        }

        float rad = Angle * Mathf.Deg2Rad;

        transform.position = PlayerHead.position + new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * Distance;
    }

    //SetActiveするところ
    public void SetAngle(float val)
    {
        Angle = val;
    }

    //GetDistanceするところ
    public float GetDistance()
    {
        return Distance;
    }
}