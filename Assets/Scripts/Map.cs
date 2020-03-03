using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    #region 맵 데이터 값
    Room[,,] map_arr = new Room[4,4,2]; //일단 임시로 442 사이즈
    int[] finish_room_pos_arr = new int[3];
    int[] now_user_pos_arr = new int[3];

    #endregion
    
    //맵 구성해주는 함수
    void map_init()
    {

    }

    #region 맵 데이터 값 반환 함수
    public int[] return_now_user_pos()
    {
        return now_user_pos_arr;
    }

    public int[] return_finish_room_pos()
    {
        return finish_room_pos_arr;
    }

    #endregion


}
