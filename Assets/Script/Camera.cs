using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera : MonoBehaviour
{
    public Transform target; // �÷��̾��� Transform�� ������ ����
    public Vector3 offset;   // ī�޶�� �÷��̾� ������ �Ÿ�

    public float turnSpeed = 4.0f; // ���콺 ȸ�� �ӵ�    
    private float xRotate = 0.0f; // ���� ����� X�� ȸ������ ���� ���� ( ī�޶� �� �Ʒ� ���� )

    void Update()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = desiredPosition;
        }

        // �¿�� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� �¿�� ȸ���� �� ���
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        // ���� y�� ȸ������ ���� ���ο� ȸ������ ���
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // ���Ʒ��� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� ȸ���� �� ���(�ϴ�, �ٴ��� �ٶ󺸴� ����)
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        // ���Ʒ� ȸ������ ���������� -45�� ~ 80���� ���� (-45:�ϴù���, 80:�ٴڹ���)
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        // ī�޶� ȸ������ ī�޶� �ݿ�(X, Y�ุ ȸ��)
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        // �÷��̾� ������Ʈ�� ȸ���� ī�޶�� ���� ���� (ī�޶� ȸ������ �÷��̾ ����)
        target.rotation = Quaternion.Euler(0, yRotate, 0);
    }
}
