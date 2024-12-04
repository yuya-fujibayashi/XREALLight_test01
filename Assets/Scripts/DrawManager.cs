using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;

public class DrawManager : MonoBehaviour
{
    //Inspector��LineObject��ݒ肷��B
    [SerializeField] GameObject LineObject;

    //�`�撆��LineObject;
    private GameObject CurrentLineObject = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //�E���HandState��Ԃ�
        HandState handState = NRInput.Hands.GetHandState(HandEnum.RightHand);
        //�l�����w�̐�[��Pose(�ʒu�� 3D ��Ԃł̉�])��Ԃ�
        Pose handJoint = handState.GetJointPose(HandJointID.IndexTip);

        if (handJoint == null)
        {
            Debug.Log("handJoint not defiend");
            return;
        }

        //�l�����w�𗧂Ă�W�F�X�`���[��(Pointing)
        if (handState.isPointing)
        {
            if (CurrentLineObject == null)
            {
                //LineObject�𐶐�
                CurrentLineObject = Instantiate(LineObject, new Vector3(0, 0, 0), Quaternion.identity);
            }
            //�Q�[���I�u�W�F�N�g����LineRenderer�R���|�[�l���g���擾
            LineRenderer render = CurrentLineObject.GetComponent<LineRenderer>();

            //LineRenderer����Positions�̃T�C�Y���擾
            int NextPositionIndex = render.positionCount;

            //LineRenderer��Positions�̃T�C�Y�𑝂₷
            render.positionCount = NextPositionIndex + 1;

            //LineRenderer��Positions�ɐl�����w�̐�[�̈ʒu����ǉ�
            render.SetPosition(NextPositionIndex, handJoint.position);
        }
        else//Pointing�|�[�Y�ȊO�̂Ƃ�
        {
            //�`�撆�̐�����������null�ɂ���
            if (CurrentLineObject != null)
            {
                CurrentLineObject = null;
            }
        }
    }
}