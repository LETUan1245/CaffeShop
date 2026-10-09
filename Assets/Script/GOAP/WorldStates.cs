using System.Collections.Generic;
using UnityEngine;


public class WorldStates
{
    
    public Dictionary<string, int> states;

    public WorldStates()
    {
        states = new Dictionary<string, int>();
    }

    // Kiểm tra xem có tồn tại trạng thái này không
    public bool HasState(string key) => states.ContainsKey(key);

    // Thêm một trạng thái mới
    public void AddState(string key, int value)
    {
        states.Add(key, value);
    }

    // Xóa một trạng thái
    public void RemoveState(string key)
    {
        if (HasState(key))
        {
            states.Remove(key);
        }
    }

    // Thay đổi giá trị của một trạng thái đã có (+ hoặc -)
    public void ModifyState(string key, int value)
    {
        if (HasState(key))
        {
            states[key] += value;
            // Nếu giá trị tụt xuống <= 0, ta coi như trạng thái đó không còn tồn tại nữa và xóa đi
            if (states[key] <= 0)
            {
                RemoveState(key);
            }
        }
        else
        {
            AddState(key, value);
        }
    }

    // Ghi đè (Set) chính xác một giá trị mới cho trạng thái
    public void SetState(string key, int value)
    {
        if (HasState(key))
        {
            states[key] = value;
        }
        else
        {
            AddState(key, value);
        }
    }

    // Lấy ra toàn bộ từ điển
    public Dictionary<string, int> GetStates() => states;
}