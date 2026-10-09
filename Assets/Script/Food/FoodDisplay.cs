using UnityEngine;

public class FoodDisplay : MonoBehaviour
{
    [Tooltip("Kéo thả 4 model đồ ăn tàng hình vào đây theo đúng thứ tự")]
    public GameObject[] foodItems;

    // Hàm gọi khi muốn hiện một món ăn cụ thể (ID từ 0 đến 3)
    public void ShowFood(int foodID)
    {
        HideAllFood(); // Tắt hết đồ cũ cho chắc chắn

        if (foodID >= 0 && foodID < foodItems.Length)
        {
            foodItems[foodID].SetActive(true); // Bật model đồ ăn lên
        }
    }

    // Hàm gọi khi Waiter lấy đồ ăn đi, hoặc khi Customer ăn xong đứng dậy
    public void HideAllFood()
    {
        foreach (GameObject food in foodItems)
        {
            if (food != null)
            {
                food.SetActive(false); // Ẩn đi
            }
        }
    }
}
