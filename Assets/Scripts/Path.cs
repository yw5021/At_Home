using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Path_info
{
    int Path_idx;
    int Connect_room_idx;
    int Path_limit_idx;

    public Path_info(int _Path_idx, int _Connect_room_idx, int _Path_limit_idx)
    {
        Path_idx = _Path_idx;
        Connect_room_idx = _Connect_room_idx;
        Path_limit_idx = _Path_limit_idx;
    }

    public int return_Path_idx()
    {
        return Path_idx;
    }

    public int return_Connect_room_idx()
    {
        return Connect_room_idx;
    }

    public int return_Path_limit_idx()
    {
        return Path_limit_idx;
    }
}

public class Path : MonoBehaviour {

	public Path_info return_path_info(int Path_idx)
    {
        if(Path_idx == 0)
        {
            Debug.Log("error 74829 - 통로 인덱스값 설정 오류");
        }

        Path_info Info;
        int Connect_room_idx = 0;
        int Path_limit_idx = 0;

        switch (Path_idx)
        {
            case 0:
                Connect_room_idx = -5;
                Path_limit_idx = 0;
                break;

            case 1:
                Connect_room_idx = 0;
                Path_limit_idx = 0;
                break;

            case 2:
                Connect_room_idx = 1;
                Path_limit_idx = 0;
                break;

            case 3:
                Connect_room_idx = 2;
                Path_limit_idx = 0;
                break;
        }

        Info = new Path_info(Path_idx, Connect_room_idx, Path_limit_idx);

        return Info;
    }

    void active_path_limit(int limit_idx)
    {
        switch (limit_idx)
        {
            case 0:
                Debug.Log("0번 제한사항");
                break;
        }
    }
}
