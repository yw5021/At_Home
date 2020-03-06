using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    #region 룸 데이터 구조체
    struct room_info
    {
        Card apply_card;    //방에 적용된 카드
        int[] connect_room_idx_arr;    //연결된 방

        public Card return_apply_card()
        {
            return apply_card;
        }

        public int[] return_connect_room_idx_arr()
        {
            return connect_room_idx_arr;
        }
    }

    #endregion

    #region 룸 데이터 값
    int room_idx;
    room_info info;

    #endregion

    #region 룸 함수
    void room_init()
    {

    }

    void input_move_room(int path_num)
    {
        //어느 방으로 가는지 탐색 후
        int[] connect_room_idx_arr = info.return_connect_room_idx_arr();

        int room_idx = connect_room_idx_arr[path_num];

        if(room_idx == null)
        {
            Debug.Log("error 51261 - 연결된 방 인덱스값 오류");
        }

        //맵쪽에 이동하는 방 정보 전달
        GameObject.FindGameObjectWithTag("Obj_Map").SendMessage("move_user_pos", room_idx);
    }

    void change_room_output()
    {
        //맵쪽에서 좌표값을 바꿔주고 이쪽에선 ui값 바꿔주는걸로
        Debug.Log("룸 오브젝트 생성");
    }

    #endregion

}
