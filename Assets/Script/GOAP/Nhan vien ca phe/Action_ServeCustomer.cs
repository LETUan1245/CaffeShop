using UnityEngine;

// Nhân viên thấy có khách đang đợi thì ra phục vụ
public class Action_ServeCustomer : GAction
{
    GameObject targetCustomer;

    public override bool PrePerform()
    {
        // 1. Kéo một khách hàng đang đợi ra khỏi hàng chờ của GWorld
        targetCustomer = GWorld.Instance.RemoveCustomer();

        if (targetCustomer == null)
        {
            // Không có khách nào đợi -> Hủy hành động
            return false;
        }

        // 2. Lấy tọa độ của Khách hàng làm mục tiêu di chuyển
        target = targetCustomer;
        return true;
    }

    public override bool PostPerform()
    {
        // 1. Trừ đi 1 khách đang đợi trong WorldStates khi đã phục vụ xong
        GWorld.Instance.GetWorld().ModifyState("WaitingCustomer", -1);

        // 2. Giao tiếp với Khách hàng: Bật cờ "hasCoffee" (viết thường đồng bộ)
        if (targetCustomer != null)
        {
            GAgent customerAgent = targetCustomer.GetComponent<GAgent>();
            if (customerAgent != null)
            {
                customerAgent.beliefs.ModifyState("hasCoffee", 1);
            }
        }

        // 3. Dọn dẹp biến tạm
        targetCustomer = null;
        return true;
    }
}