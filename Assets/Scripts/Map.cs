using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    #region 맵 데이터 값
    public GameObject[] go_room_arr;

    //Room[] map_arr;
    int finish_room_idx;
    int now_user_room_idx;

    #endregion
    
    //맵 구성해주는 함수
    void map_init()
    {
        //일단 임시로 켬
        go_room_arr[0].SetActive(true);

        now_user_room_idx = 0;
    }

    void move_user_pos(int room_idx)
    {
        Debug.Log(room_idx + "번 방으로 이동");

        GameObject go_prev_room = go_room_arr[now_user_room_idx];
        GameObject go_now_room = go_room_arr[room_idx];

        GameObject[] go_temp_room_arr = new GameObject[2] { go_now_room, go_prev_room};

        //맵에서 현재 좌표값 옮겨주고
        now_user_room_idx = room_idx;

        check_finish_room();

        //룸쪽에 룸이동 함수 사용
        Room now_room = go_prev_room.GetComponent<Room>();

        now_room.SendMessage("change_room_output", go_temp_room_arr);
    }

    void check_finish_room()
    {
        if(finish_room_idx == now_user_room_idx)
        {
            Debug.Log("게임 클리어");
        }
    }

    public Room return_now_room()
    {
        GameObject go_now_room = go_room_arr[now_user_room_idx];

        Room now_room = go_now_room.GetComponent<Room>();

        return now_room;
    }

    private void Awake()
    {
        map_init();
    }
}
