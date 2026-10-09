using UnityEngine;

public class CustomerSittingState : CustomerBaseState
{
    // Đảm bảo bạn có State tên là "Sit" trong Animator Controller của Khách hàng
    private readonly int SitHash = Animator.StringToHash("Sitting");

    public CustomerSittingState(CustomerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        // KIỂM TRA: Nếu đã chạm ghế từ trước khi State này kịp bật lên -> "Hút" vào tư thế ngồi luôn!
        if (stateMachine.SitDetector.currentSitPoint != null)
        {
            Transform sitObj = stateMachine.SitDetector.currentSitPoint;
            HandleSitSnap(sitObj.position, sitObj.forward);
        }
        else
        {
            // Nếu chưa chạm thì mới đăng ký lắng nghe sự kiện
            stateMachine.SitDetector.OnSitPositionDetected += HandleSitSnap;
        }
    }

    private void HandleSitSnap(Vector3 exactPosition, Vector3 exactForward)
    {
        // 1. Tắt vật lý
        stateMachine.Controller.enabled = false;

        // 2. Ép tọa độ thẳng vào tâm ghế (không trừ Y lằng nhằng nữa)
        stateMachine.transform.position = exactPosition;
        stateMachine.transform.rotation = Quaternion.LookRotation(exactForward);

        // 3. Bật lại vật lý
        stateMachine.Controller.enabled = true;

        // 4. Xóa đường đi để NavMesh không giật khách ra ngoài
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;

        // 5. Phát hoạt ảnh ngồi
        stateMachine.Animator.CrossFadeInFixedTime(SitHash, 0.1f);
    }
    

    public override void Tick(float deltaTime)
    {
        // Khách ngồi yên, chờ đợi (Quá trình này do Action_WaitAndGoHome của GOAP quản lý thời gian
        if (stateMachine.Agent.hasPath && stateMachine.Agent.desiredVelocity.sqrMagnitude > 0.1f)
        {
            // 1. "RÚT ĐIỆN" RADAR: Tắt Collider của SitDetector để khách không bị hút lại vào ghế
            if (stateMachine.SitDetector != null)
            {
                stateMachine.SitDetector.GetComponent<Collider>().enabled = false;
                stateMachine.SitDetector.ClearSitPoint();
            }

            // 2. Chuyển FSM sang trạng thái chạy về nhà
            stateMachine.SwitchState(new CustomerMoveState(stateMachine));
        }
    }

    public override void Exit()
    {
        // Dọn dẹp sự kiện khi khách đứng dậy ra về
        if (stateMachine.SitDetector != null)
        {
            stateMachine.SitDetector.ClearSitPoint();
        }
        stateMachine.SitDetector.OnSitPositionDetected -= HandleSitSnap;
       
    }
}