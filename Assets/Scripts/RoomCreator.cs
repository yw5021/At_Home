using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreator : MonoBehaviour {

    public GameObject[] Go_Room_Prefab_Arr;

    public Room Room_Create(int idx)
    {
        //카드 오브젝트 생성해서
        GameObject Go_Room = Instantiate(Go_Room_Prefab_Arr[idx],GameObject.Find("Room").transform);

        Room Now_Room = Go_Room.GetComponent<Room>();

        //카드쪽에서 init해주는 부분 가져올 것
        Now_Room._room_idx = idx;

        Now_Room._info = Room_Info_Init(idx);

        Now_Room.SendMessage("room_init");

        return Now_Room;
    }

    room_info Room_Info_Init(int room_idx)
    {
        Card apply_card = null;
        int[] Path_idx_arr = new int[0];
        switch (room_idx)
        {
            case 0:
                apply_card = null;
                Path_idx_arr = new int[2] { 2, 3 };
                break;

            case 1:
                apply_card = null;
                Path_idx_arr = new int[2] { 1, 3 };
                break;

            case 2:
                apply_card = null;
                Path_idx_arr = new int[2] { 1, 2 };
                break;


            default:
                Debug.Log("error 62351 - 룸 인덱스값 설정 이상");
                break;
        }
        room_info info = new room_info(apply_card, Path_idx_arr);

        return info;
    }
}
