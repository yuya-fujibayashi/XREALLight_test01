using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;

public class DrawManager : MonoBehaviour
{
    //InspectorでLineObjectを設定する。
    [SerializeField] GameObject LineObject;

    //描画中のLineObject;
    private GameObject CurrentLineObject = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //右手のHandStateを返す
        HandState handState = NRInput.Hands.GetHandState(HandEnum.RightHand);
        //人差し指の先端のPose(位置と 3D 空間での回転)を返す
        Pose handJoint = handState.GetJointPose(HandJointID.IndexTip);

        if (handJoint == null)
        {
            Debug.Log("handJoint not defiend");
            return;
        }

        //人差し指を立てるジェスチャー間(Pointing)
        if (handState.isPointing)
        {
            if (CurrentLineObject == null)
            {
                //LineObjectを生成
                CurrentLineObject = Instantiate(LineObject, new Vector3(0, 0, 0), Quaternion.identity);
            }
            //ゲームオブジェクトからLineRendererコンポーネントを取得
            LineRenderer render = CurrentLineObject.GetComponent<LineRenderer>();

            //LineRendererからPositionsのサイズを取得
            int NextPositionIndex = render.positionCount;

            //LineRendererのPositionsのサイズを増やす
            render.positionCount = NextPositionIndex + 1;

            //LineRendererのPositionsに人差し指の先端の位置情報を追加
            render.SetPosition(NextPositionIndex, handJoint.position);
        }
        else//Pointingポーズ以外のとき
        {
            //描画中の線があったらnullにする
            if (CurrentLineObject != null)
            {
                CurrentLineObject = null;
            }
        }
    }
}