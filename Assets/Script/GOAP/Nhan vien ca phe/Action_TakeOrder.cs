using UnityEngine;

public class Action_TakeOrder : GAction
{
    GameObject targetCustomer;

    public override bool PrePerform()
    {
        targetCustomer = GWorld.Instance.GetCustomerToOrder();
        if (targetCustomer == null) return false;

        // Trích xuất cái bàn từ trí nhớ của khách để làm đích đến
        Action_FindTable findTable = targetCustomer.GetComponent<Action_FindTable>();
        if (findTable != null && findTable.currentTable != null)
        {
            target = findTable.currentTable; // Nhân viên sẽ đi đến Bàn thay vì đâm vào khách
            return true;
        }
        return false;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("NeedsOrder", -1);

        // Lấy trực tiếp cái bàn từ trí nhớ của khách, an toàn tuyệt đối
        Action_FindTable findTable = targetCustomer.GetComponent<Action_FindTable>();
        if (findTable == null || findTable.currentTable == null)
        {
            Debug.LogError("Không tìm thấy bàn!");
            return false;
        }

        GameObject myTable = findTable.currentTable;
        int randomFoodID = Random.Range(0, 4);

        // Ghi Order vào hệ thống
        GWorld.Instance.AddOrder(targetCustomer, myTable, randomFoodID);
        return true;
    }
}