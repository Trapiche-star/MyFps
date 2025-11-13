using UnityEngine;

namespace Sample
{
    /// <summary>
    /// Ybot을 제어하는 클래스
    /// 인풋 시스템 - 옫드 인풋 시스템
    /// W키를 누르면 런 애니메이션 플레이
    /// Shift 키를 누르면 걷기로 전환 (선생님은 누르고 있으면 런상태로,기본w는 걷기로) 애니메이션 플레이
    /// 대기 ->걷기만 변환 가능, 걷기에서 뛰기로 변환 가능
    /// (그냥 내가 원하는건 w를 누르면 3초간 걷다가 바로 런이동 이후 쉬프키를 눌러야 걷기하게 하고싶다)
    /// </summary>
    public class YBotController : MonoBehaviour
    {
        #region Variables
        //참조
        private Animator animator;

        //애니메이터 파라미터 이름
        private string IsWalk = "IsWalk ";
        private string IsRun = "IsRun";

        //걷기 → 뛰기 전환 타이머
        [SerializeField] private float walkToRunDelay = 3f;
        private float walkTimer = 0f;

        //현재 상태
        private bool isWalking = false;
        private bool isRunning = false;
        #endregion

        #region Custom Method  
        private void Awake()
        {
            //참조
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            HandleMovement();
        }


        private void HandleMovement()
        {
            //===========================================================
            // W 키 누름
            //===========================================================
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartWalk();
            }

            // W 키를 누르고 있는 동안 (걷기 → 타이머 증가)
            if (Input.GetKey(KeyCode.W))
            {
                // Shift 누르면 강제 Walk
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StartWalk();
                    walkTimer = 0f;     //걷기 타이머 초기화
                    return;
                }

                // Walk 상태라면 경과시간 증가
                if (isWalking)
                {
                    walkTimer += Time.deltaTime;

                    // 3초 지나면 자동 Run
                    if (walkTimer >= walkToRunDelay)
                    {
                        StartRun();
                    }
                }
            }

            //===========================================================
            // W 키를 떼면 Idle
            //===========================================================
            if (Input.GetKeyUp(KeyCode.W))
            {
                StartIdle();
            }
        }
        #endregion

        #region Animation Control
        private void StartIdle()
        {
            isWalking = false;
            isRunning = false;

            animator.SetBool(IsWalk, false);
            animator.SetBool(IsRun, false);

            walkTimer = 0f;
        }

        private void StartWalk()
        {
            isWalking = true;
            isRunning = false;

            animator.SetBool(IsWalk, true);
            animator.SetBool(IsRun, false);
        }

        private void StartRun()
        {
            isWalking = false;
            isRunning = true;

            animator.SetBool(IsWalk, false);
            animator.SetBool(IsRun, true);
        }
        #endregion
       
    }

}
/*AI
        #region Variables
        private Animator animator;

        private bool isWalking = false;
        private bool isRunning = false;

        private float walkTimer = 0f;            // W 눌렀을 때부터 3초 측정용
        [SerializeField] private float walkToRunDelay = 3f; // 3초 after walk → run
        #endregion

        #region Unity Event
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            HandleMovement();
        }
        #endregion

        #region Custom Method
        private void HandleMovement()
        {
            bool wKey = Input.GetKey(KeyCode.W);
            bool shiftKey = Input.GetKey(KeyCode.LeftShift);

            //===========================================================
            // W 키를 누른 상태
            //===========================================================
            if (wKey)
            {
                // 1) Shift 누르면 걷기 강제 유지
                if (shiftKey)
                {
                    SetWalk();
                    walkTimer = 0f; // 다시 걷기 시작
                    return;
                }

                // 2) Shift 안 눌렀을 때: Walk 상태 진입
                if (!isWalking && !isRunning)
                {
                    SetWalk();
                    walkTimer = 0f; // 걷기 타이머 초기화
                }

                // 3) Walk 중이면 타이머 증가
                if (isWalking)
                {
                    walkTimer += Time.deltaTime;

                    // 3초 경과 후 자동 Run 전환
                    if (walkTimer >= walkToRunDelay)
                    {
                        SetRun();
                    }
                }

                return;
            }

            //===========================================================
            // W 키를 떼면 Idle(대기)
            //===========================================================
            if (!wKey)
            {
                SetIdle();
                walkTimer = 0f;
            }
        }

        //===========================================================
        // 상태 설정 함수들
        //===========================================================
        private void SetIdle()
        {
            isWalking = false;
            isRunning = false;

            animator.SetBool("IsWalk", false);
            animator.SetBool("IsRun", false);
        }

        private void SetWalk()
        {
            isWalking = true;
            isRunning = false;

            animator.SetBool("IsWalk", true);
            animator.SetBool("IsRun", false);
        }

        private void SetRun()
        {
            isWalking = false;
            isRunning = true;

            animator.SetBool("IsWalk", false);
            animator.SetBool("IsRun", true);
        }
        #endregion*/