using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

namespace MyBird
{

    public class Player : MonoBehaviour
    {
        #region Variables
        //����
        private bool KeyJump = false; //���� Ű �� ǲ üũ. ����� Ű�� ������������ false��, Ű�� ������ true�� ��

        //���� ���
        private Rigidbody2D rb2D;
        //���� ��
        [SerializeField] private float jumpPower = 5f;

        //ȸ��
        private Vector3 birdRotation;
        //������ ����
        private Vector3 movefront;
        //ȸ�� �ӵ�
        private float turnspeed;
        //���� �ö� �� ȸ�� �ӵ�
        [SerializeField] private float UpturnSpeed = 5f;
        //�Ʒ��� ������ �� ȸ�� �ӵ�
        [SerializeField] private float DownturnSpeed = -5f;

        //�̵�
        //�̵��ӵ� - Translate �����ϸ� �ڵ����� ���������� �̵�
        private float movespeed = 5f;
        #endregion

        #region Unity Event Method
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //����
            rb2D = GetComponent<Rigidbody2D>();

            MoveRightBrid();
        }
        #endregion
        // Update is called once per frame
        void Update()
        {
            //�� ǲ ó��
            InputBird();

            //���� ȸ��
            RotateBird();

            //���� �̵� 
            //this.transform.Translate(���� * Time.deltaTime * �ӵ�, ����);
        }

        private void FixedUpdate() //InputBird();�� true��
        {
            if (KeyJump)
            {
                JumpBird();

                KeyJump = false;
            }
 
            if (true)
            {
                RotateBird();
            }

        }

        //�� ǲ ó��
        void InputBird()
        {
            //�����̽�Ű �Ǵ�("|=") ���콺 ���� Ŭ�� �Է� �ޱ�
            KeyJump |= Input.GetKeyDown(KeyCode.Space); //�����̽� Ű�� ������(true) ������Ʈ�� ��ǲ ó���� �̵��ǰ� �ٽ� flase�� ��
            KeyJump |= Input.GetMouseButton(0);


        }

        //���� �����ϱ�
        void JumpBird()
        {
            //�Ʒ� �ʿ��� �� ������ ���� �ش�
            //rb.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse); ����Ƽ6 ���� ����
            rb2D.linearVelocity = Vector3.up * jumpPower; //����Ƽ6 �ֽŹ���

        }
        //���� ȸ���ϱ�
        void RotateBird()
        {
            //�ö� �� �ִ� +30�� ���� ȸ�� : turnspeed = 2.5; (upRorate)
            //������ �� �ִ� -90�� ���� ȸ�� : turnspeed = -5; (downRorate)

            float turnspeed = 0f;

            if (rb2D.linearVelocity.y > 0f) //�ö� ��
            {

                turnspeed = UpturnSpeed;
               
            }
            else if(rb2D.linearVelocity.y < 0f) //������ ��
            {
                turnspeed = DownturnSpeed;
            }

            birdRotation = new Vector3(0f, 0f, Mathf.Clamp((birdRotation.z + turnspeed), -90f, 30f)); //��������. �ִ밪 : -90, �ּҰ� : 30
            this.transform.eulerAngles = birdRotation; //ȸ���� ����
        }

        void MoveRightBrid()
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * movespeed, Space.World);
        }



    }
}

