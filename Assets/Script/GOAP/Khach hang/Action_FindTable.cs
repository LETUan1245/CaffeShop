using UnityEngine;

public class Action_FindTable : GAction
{
    public GameObject currentTable;

    public override bool PrePerform()
    {
        // 1. Lấy bàn từ GWorld làm đích đến
        currentTable = GWorld.Instance.RemoveTable();
        if (currentTable == null) return false;

        target = currentTable;

        // 2. Tìm khối "SitPoint" để khách đến đúng ghế
        foreach (Transform child in currentTable.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("SitPoint"))
            {
                target = child.gameObject;
                break;
            }
        }

        // TUYỆT ĐỐI KHÔNG GỌI NHÂN VIÊN Ở ĐÂY
        return true;
    }

    public override bool PostPerform()
    {
        // 3. ĐÃ ĐẾN BÀN VÀ NGỒI XUỐNG -> BÂY GIỜ MỚI GỌI NHÂN VIÊN
        GWorld.Instance.CallWaiter(this.gameObject);
        GWorld.Instance.GetWorld().ModifyState("NeedsOrder", 1);

        return true;
    }
}