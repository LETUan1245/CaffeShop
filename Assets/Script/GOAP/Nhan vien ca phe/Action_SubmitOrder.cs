using UnityEngine;

public class Action_SubmitOrder : GAction
{
    public override bool PrePerform()
    {
        // Chạy về quầy pha chế
        target = GameObject.FindWithTag("Counter");
        if (target == null) return false;
        return true;
    }
    public override bool PostPerform()
    {
        // Rung chuông báo cho Đầu bếp bắt đầu nấu!
        GWorld.Instance.GetWorld().ModifyState("hasOrdered", 1);
        return true;
    }
}