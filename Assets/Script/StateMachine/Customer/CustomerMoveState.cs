using UnityEngine;

public class CustomerMoveState : CustomerBaseState
{
    private readonly int BlendHash = Animator.StringToHash("Blend");
    private const float AnimatorDampTime = 0.1f;

    public CustomerMoveState(CustomerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        // Nếu cần khởi tạo gì khi bắt đầu chạy thì viết ở đây
        stateMachine.Animator.CrossFadeInFixedTime(Animator.StringToHash("LocomotionHash"), 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.SitDetector != null && stateMachine.SitDetector.currentSitPoint != null)
        {
            stateMachine.SwitchState(new CustomerSittingState(stateMachine));
            return;
        }
        // 1. Tính toán % tốc độ và truyền vào Blend Tree để phát hoạt ảnh
        float speedPercent = stateMachine.Agent.velocity.magnitude / stateMachine.Agent.speed;
        stateMachine.Animator.SetFloat(BlendHash, speedPercent, AnimatorDampTime, deltaTime);

        // 2. Kéo cơ thể vật lý đi theo NavMesh (Nếu bạn vẫn đang bật CharacterController)
        if (stateMachine.Controller.enabled)
        {
            // 1. Lấy vận tốc thông minh (đã tính toán né vật cản) từ NavMeshAgent
            Vector3 agentVelocity = stateMachine.Agent.velocity;

            // 2. Truyền vận tốc đó cho CharacterController di chuyển
            Move(agentVelocity, deltaTime);

            // 3. Ép NavMeshAgent bám sát theo vị trí thực tế của cơ thể[cite: 19]
            stateMachine.Agent.nextPosition = stateMachine.transform.position;

            // 4. Ép nhân vật xoay mặt theo hướng đang đi (nếu đang di chuyển)
            if (agentVelocity.sqrMagnitude > 0.1f)
            {
                stateMachine.transform.rotation = Quaternion.Slerp(
                    stateMachine.transform.rotation,
                    Quaternion.LookRotation(agentVelocity.normalized),
                    deltaTime * 10f
                );
            }
        }

        // 3. KIỂM TRA ĐẾN ĐÍCH: Nếu Agent đã dừng lại -> Trở về trạng thái đứng im
        if (!stateMachine.Agent.pathPending && stateMachine.Agent.remainingDistance <= stateMachine.Agent.stoppingDistance)
        {
            stateMachine.SwitchState(new CustomerIdleState(stateMachine));
        }
    }

    public override void Exit() { }
}
