using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    #region 맵 데이터 값
    public GameObject[] go_room_arr;

    RoomCreator roomCreator;
    Room now_room;

    //Room[] map_arr;
    int finish_room_idx = 100;
    int now_user_room_idx;

    bool is_active_map = false;

    bool is_forced_move = false;

    List<int> user_path_room_idx_list = new List<int>();
    #endregion

    void move_map_start()
    {
        Debug.Log("이동 시작");
        is_active_map = true;
    }

    //맵 구성해주는 함수
    void map_init()
    {
        roomCreator = GameObject.FindGameObjectWithTag("RoomCreator").GetComponent<RoomCreator>();

        now_user_room_idx = 0;

        now_room = roomCreator.Room_Create(now_user_room_idx);

        user_path_room_idx_list.Add(now_user_room_idx);
    }

    void move_user_pos(int room_idx)
    {
        if (!is_active_map)
        {
            Debug.Log("현재 이동 불가");
            return;
        }

        Debug.Log(room_idx + "번 방으로 이동");

        //맵에서 현재 좌표값 옮겨주고
        now_user_room_idx = room_idx;

        //움직인 경로 저장
        user_path_room_idx_list.Add(now_user_room_idx);

        //이전 방 지워주고
        Destroy(now_room.gameObject);

        //새방 생성
        now_room = roomCreator.Room_Create(now_user_room_idx);

        check_finish_room();


        is_active_map = false;

        if (is_forced_move)
        {
            GameManager.gameManager.SendMessage("forced_phase", game_phase.turn_end);
        }
        else
        {
            GameManager.gameManager.SendMessage("next_phase", "map");
        }
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
        return now_room;
    }

    void forced_move_room(int turn)
    {
        Debug.Log(turn + "턴 전 방으로 이동");
        is_forced_move = true;

        int path_room_idx_list_cnt = user_path_room_idx_list.Count;
        int turn_cnt_calc = turn + 1;

        if (path_room_idx_list_cnt - turn_cnt_calc > 0)
        {
            int forced_move_room_idx = user_path_room_idx_list[path_room_idx_list_cnt - turn_cnt_calc];

            for(int i = 0; i < turn_cnt_calc; i++)
            {
                user_path_room_idx_list.RemoveAt(path_room_idx_list_cnt - turn_cnt_calc + i);
            }

            Debug.Log(forced_move_room_idx + "번 방으로 왔음");
            is_active_map = true;
            move_user_pos(forced_move_room_idx);
        }
        else
        {
            user_path_room_idx_list.Clear();

            Debug.Log("첫방으로 왔음");
            is_active_map = true;
            move_user_pos(0);
        }
    }

    private void Awake()
    {
        map_init();
    }
}
