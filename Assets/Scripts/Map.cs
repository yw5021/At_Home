using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    #region 맵 데이터 값
    Room[] map_arr;
    int finish_room_idx;
    int now_user_room_idx;

    #endregion
    
    //맵 구성해주는 함수
    void map_init()
    {

    }

    void move_user_pos(int room_idx)
    {
        //맵에서 현재 좌표값 옮겨주고
        now_user_room_idx = room_idx;

        check_finish_room();

        //룸쪽에 룸이동 함수 사용
        Room now_room = map_arr[now_user_room_idx];

        now_room.SendMessage("change_room_output");
    }

    void check_finish_room()
    {
        if(finish_room_idx == now_user_room_idx)
        {
            Debug.Log("게임 클리어");
        }
    }
}
