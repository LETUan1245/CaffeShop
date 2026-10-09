using System.Collections.Generic;
using UnityEngine;

public class OrderInfo
{
    public GameObject customer;
    public GameObject table;
    public int foodID;
}
// Dùng 'sealed' để ngăn class khác kế thừa nó, đảm bảo tính độc nhất
public sealed class GWorld
{
    // Áp dụng mẫu thiết kế Singleton: Chỉ có duy nhất 1 GWorld tồn tại trong game[cite: 6]
    private static readonly GWorld instance = new GWorld();
    // 2. Từ điển lưu trữ trạng thái chung (World States)
    private static WorldStates world;
    // 3. Hàng đợi tài nguyên cho Tiệm Cà Phê
    private static Queue<GameObject> tables;      // Quản lý Bàn trống
    private static Queue<GameObject> customers;   // Quản lý Khách đang chờ gọi món
    public OrderInfo readyOrder;
    static GWorld()
    {
        world = new WorldStates();
        tables = new Queue<GameObject>();
        customers = new Queue<GameObject>();
        GameObject[] tbs = GameObject.FindGameObjectsWithTag("Table");
        foreach (GameObject t in tbs)
        {
            tables.Enqueue(t); // Đưa từng bàn tìm được vào hàng đợi
        }
        // Nếu tìm thấy bàn, cập nhật trạng thái "FreeTable" vào WorldStates
        if (tables.Count > 0)
        {
            world.ModifyState("FreeTable", tables.Count);
        }
    }

    private GWorld() { } // Hàm khởi tạo private để không ai có thể tạo thêm GWorld thứ 2

    // Biến toàn cục để các Agent gọi: GWorld.Instance...
    public static GWorld Instance
    {
        get { return instance; }
    }

    // Hàm lấy ra danh sách trạng thái
    public WorldStates GetWorld()
    {
        return world;
    }
    // ==========================================
    // CÁC HÀM TƯƠNG TÁC TÀI NGUYÊN (RESOURCES)
    // ==========================================

    // --- XỬ LÝ KHÁCH HÀNG (CUSTOMERS) ---
    // Gọi hàm này khi một khách hàng bước vào quán và đứng xếp hàng
    public void AddCustomer(GameObject p)
    {
        customers.Enqueue(p);
    }

    // Barista sẽ gọi hàm này để lấy một khách hàng ra phục vụ
    public GameObject RemoveCustomer()
    {
        if (customers.Count == 0) return null;
        return customers.Dequeue();
    }

    // --- XỬ LÝ BÀN GHẾ (TABLES) ---
    // Gọi hàm này khi Khách hàng uống xong và rời đi (Trả bàn lại cho quán)
    public void AddTable(GameObject t)
    {
        tables.Enqueue(t);
    }
    // Thêm dòng này ở phần khai báo biến (gần chỗ khai báo Queue customers)
    private static Queue<OrderInfo> pendingOrders = new Queue<OrderInfo>();

    // Thêm 2 hàm này vào phần CÁC HÀM TƯƠNG TÁC TÀI NGUYÊN
    public void AddOrder(GameObject customer, int foodID)
    {
        pendingOrders.Enqueue(new OrderInfo { customer = customer, foodID = foodID });
    }

    public OrderInfo GetNextOrder()
    {
        if (pendingOrders.Count == 0) return null;
        return pendingOrders.Dequeue();
    }
    // Gọi hàm này khi Khách hàng muốn lấy một bàn để ngồi (Giữ chỗ/Reserve)
    // Nếu trả về null nghĩa là quán đã hết bàn.
    public GameObject RemoveTable()
    {
        if (tables.Count == 0) return null;
        return tables.Dequeue();
    }
    // Khách hàng gọi hàm này để gửi hóa đơn
    public void AddOrder(GameObject customer, GameObject table, int foodID)
    {
        pendingOrders.Enqueue(new OrderInfo { customer = customer, table = table, foodID = foodID });
    }
    // Khách vẫy tay chờ nhân viên ra bàn lấy Order
    private static Queue<GameObject> customersWaitingToOrder = new Queue<GameObject>();

    public void CallWaiter(GameObject c) { customersWaitingToOrder.Enqueue(c); }
    public GameObject GetCustomerToOrder()
    {
        if (customersWaitingToOrder.Count == 0) return null;
        return customersWaitingToOrder.Dequeue();
    }
}