using UnityEngine;

// Khách hàng bắt đầu hành trình: Đi từ điểm sinh ra (Spawner) vào cửa quán
public class Action_GoToCafe : GAction
{
    public override bool PrePerform()
    {
        // Tự tìm điểm Start ngay tại đây để không bị null
        target = GameObject.FindWithTag("Start");
        if (target == null) return false;

        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
}