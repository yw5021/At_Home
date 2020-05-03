using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region 룸 데이터 구조체
public struct room_info
{
    Card apply_card;    //방에 적용된 카드
    int[] Path_idx_arr;    //연결된 방

    public room_info(Card _apply_card, int[] _Path_idx_arr)
    {
        apply_card = _apply_card;
        Path_idx_arr = _Path_idx_arr;
    }

    public Card return_apply_card()
    {
        return apply_card;
    }

    public int[] return_path_idx_arr()
    {
        return Path_idx_arr;
    }
}
#endregion

public class Room : MonoBehaviour {

    #region 룸 데이터 값
    int room_idx;
    public int _room_idx;

    room_info info;
    public room_info _info;

    #endregion

    #region 룸 함수
    void room_init()
    {
        room_idx = _room_idx;
        info = _info;
    }

    public void move_room(int path_num)
    {
        Debug.Log(path_num + "번째 통로 선택");

        //어느 방으로 가는지 탐색 후
        int[] Path_idx_arr = info.return_path_idx_arr();

        int Select_path_idx = Path_idx_arr[path_num];

        Debug.Log(Select_path_idx + "번 통로가 선택됨");

        //이 부분 조금 수정 해야될듯
        Path path_comp = GameObject.FindGameObjectWithTag("Path").GetComponent<Path>();

        Path_info path_info = path_comp.return_path_info(Select_path_idx);

        int room_idx = path_info.return_Connect_room_idx();

        /*
        if(room_idx == null)
        {
            Debug.Log("error 51261 - 연결된 방 인덱스값 오류");
        }
        */

        //맵쪽에 이동하는 방 정보 전달
        GameObject.FindGameObjectWithTag("Map").SendMessage("move_user_pos", room_idx);
    }

    /*
    void change_room_output(GameObject[] go_room_arr)
    {
        GameObject go_now_room = go_room_arr[0];
        GameObject go_prev_room = go_room_arr[1];

        go_prev_room.SetActive(false);
        go_now_room.SetActive(true);
    }
    */

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
}
