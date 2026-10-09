using UnityEngine;

public class Action_OrderFood : GAction
{
    public override bool PrePerform()
    {
        // 1. Khách chọn ngẫu nhiên 1 con số từ 0 đến 3 (tương ứng 4 món đồ ăn)
        int randomFoodID = Random.Range(0, 4);

       
        // 2. Tìm cái bàn (Table) mà Khách đang ngồi để ghi vào hóa đơn
        // Lưu ý: SitDetector phải nằm bên trong GameObject Khách hàng
        SitDetector sitDetector = this.GetComponentInChildren<SitDetector>();

        if (sitDetector == null || sitDetector.currentSitPoint == null)
        {
            Debug.LogWarning("Khách chưa ngồi vào ghế, không thể gọi món!");
            return false;
        }

        GameObject myTable = sitDetector.currentSitPoint.parent.gameObject;

        // 3. Gửi hóa đơn vào hệ thống GWorld (Khách hàng, Bàn, Mã món ăn)
        GWorld.Instance.AddOrder(this.gameObject, myTable, randomFoodID);
        // THÊM LẠI DÒNG NÀY: Rung chuông báo cho Đầu bếp chạy ra nấu!
        GWorld.Instance.GetWorld().ModifyState("hasOrdered", 1);

        // Khách ngồi im tại chỗ gọi món
        target = this.gameObject;
        return true;
    }

    public override bool PostPerform()
    {
        // Khách gọi xong thì chờ đồ ăn mang ra (đổi cờ chờ phục vụ)
        this.GetComponent<GAgent>().beliefs.ModifyState("waitingForFood", 1);
        return true;
    }
}
