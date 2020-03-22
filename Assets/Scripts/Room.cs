using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region 룸 데이터 구조체
struct room_info
{
    Card apply_card;    //방에 적용된 카드
    int[] connect_room_idx_arr;    //연결된 방

    public room_info(Card _apply_card, int[] _connect_room_idx_arr)
    {
        apply_card = _apply_card;
        connect_room_idx_arr = _connect_room_idx_arr;
    }

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

public class Room : MonoBehaviour {

    #region 룸 데이터 값
    int room_idx;
    public int _room_idx;

    room_info info;

    #endregion

    #region 룸 함수
    void room_init()
    {
        room_idx = _room_idx;

        Card apply_card;
        int[] connect_room_idx_arr;
        switch (room_idx)
        {
            case 0:
                apply_card = null;
                connect_room_idx_arr = new int[2] { 1 , 2 };

                info = new room_info(apply_card, connect_room_idx_arr);

                break;
            case 1:
                apply_card = null;
                connect_room_idx_arr = new int[2] { 0, 2 };

                info = new room_info(apply_card, connect_room_idx_arr);
                break;

            case 2:
                apply_card = null;
                connect_room_idx_arr = new int[2] { 0, 1 };

                info = new room_info(apply_card, connect_room_idx_arr);
                break;


            default:
                Debug.Log("error 62351 - 룸 인덱스값 설정 이상");
                break;
        }
    }

    public void move_room(int path_num)
    {
        Debug.Log(path_num + "번 통로 선택");

        //어느 방으로 가는지 탐색 후
        int[] connect_room_idx_arr = info.return_connect_room_idx_arr();

        int room_idx = connect_room_idx_arr[path_num];

        if(room_idx == null)
        {
            Debug.Log("error 51261 - 연결된 방 인덱스값 오류");
        }

        //맵쪽에 이동하는 방 정보 전달
        GameObject.FindGameObjectWithTag("Map").SendMessage("move_user_pos", room_idx);
    }

    void change_room_output(GameObject[] go_room_arr)
    {
        GameObject go_now_room = go_room_arr[0];
        GameObject go_prev_room = go_room_arr[1];

        go_prev_room.SetActive(false);
        go_now_room.SetActive(true);
    }

    void active_apply_card()
    {
        Card card = info.return_apply_card();

        if(card == null)
        {
            Debug.Log("방에 적용된 카드 없음");
            return;
        }

        card.SendMessage("use_card");
    }

    #endregion
    private void Awake()
    {
        room_init();
    }

}
