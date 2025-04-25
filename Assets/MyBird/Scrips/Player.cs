using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

namespace MyBird
{

    public class Player : MonoBehaviour
    {
        #region Variables
        //점프
        private bool KeyJump = false; //점프 키 인 풋 체크. 현재는 키를 누르지않으니 false고, 키를 누르면 true가 됨

        //점프 기능
        private Rigidbody2D rb2D;
        //점프 힘
        [SerializeField] private float jumpPower = 5f;

        //회전
        private Vector3 birdRotation;
        //앞으로 전진
        private Vector3 movefront;
        //회전 속도
        private float turnspeed;
        //위로 올라갈 때 회전 속도
        [SerializeField] private float UpturnSpeed = 5f;
        //아래로 내려갈 때 회전 속도
        [SerializeField] private float DownturnSpeed = -5f;

        //이동
        //이동속도 - Translate 시작하면 자동으로 오른쪽으로 이동
        private float movespeed = 5f;
        #endregion

        #region Unity Event Method
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //참조
            rb2D = GetComponent<Rigidbody2D>();

            MoveRightBrid();
        }
        #endregion
        // Update is called once per frame
        void Update()
        {
            //인 풋 처리
            InputBird();

            //버드 회전
            RotateBird();

            //버드 이동 
            //this.transform.Translate(방향 * Time.deltaTime * 속도, 공간);
        }

        private void FixedUpdate() //InputBird();가 true면
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

        //인 풋 처리
        void InputBird()
        {
            //스페이스키 또는("|=") 마우스 왼쪽 클릭 입력 받기
            KeyJump |= Input.GetKeyDown(KeyCode.Space); //스페이스 키를 누르면(true) 업데이트의 인풋 처리로 이동되고 다시 flase가 됨
            KeyJump |= Input.GetMouseButton(0);


        }

        //버드 점프하기
        void JumpBird()
        {
            //아래 쪽에서 위 쪽으로 힘을 준다
            //rb.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse); 유니티6 이전 버전
            rb2D.linearVelocity = Vector3.up * jumpPower; //유니티6 최신버전

        }
        //버드 회전하기
        void RotateBird()
        {
            //올라갈 때 최대 +30도 까지 회전 : turnspeed = 2.5; (upRorate)
            //내려갈 때 최대 -90도 까지 회전 : turnspeed = -5; (downRorate)

            float turnspeed = 0f;

            if (rb2D.linearVelocity.y > 0f) //올라갈 때
            {

                turnspeed = UpturnSpeed;
               
            }
            else if(rb2D.linearVelocity.y < 0f) //내려갈 때
            {
                turnspeed = DownturnSpeed;
            }

            birdRotation = new Vector3(0f, 0f, Mathf.Clamp((birdRotation.z + turnspeed), -90f, 30f)); //누적연산. 최대값 : -90, 최소값 : 30
            this.transform.eulerAngles = birdRotation; //회전값 연산
        }

        void MoveRightBrid()
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * movespeed, Space.World);
        }



    }
}

