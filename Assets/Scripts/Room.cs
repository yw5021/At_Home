using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    #region 룸 데이터 구조체
    struct room_info
    {
        int room_idx;
        Card apply_card;    //방에 적용된 카드
        int[] connect_room_idx_arr;    //연결된 방

        int return_room_idx()
        {
            return room_idx;
        }
    }

    #endregion

    #region 룸 데이터 값
    room_info info;

    #endregion

    #region 룸 함수
    void room_init()
    {

    }

    void move_room(int now_room_idx)
    {
        //맵쪽에서 좌표값을 바꿔주고 이쪽에선 ui값 바꿔주는걸로

    }

    #endregion

}
