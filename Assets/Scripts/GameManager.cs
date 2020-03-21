using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    enum game_phase
    {
        none = 0,
        enter = 1,  //진입
        action = 2, //행동
        maintenence = 3,    //정비
        departure = 4   //이탈
    }

    game_phase now_game_phase;
    game_phase next_game_phase;

    Map map;

    void phase_progress()
    {
        switch (now_game_phase)
        {
            case game_phase.enter:
                //방에 입장 (값 초기화)

                next_game_phase = game_phase.action;
                break;

            case game_phase.action:
                //방 효과에 맞추어서 카드를 사용하게 함 (방에 적용된 카드 실행, 카드 사용 실행)
                StartCoroutine("action_phase");
                break;

            case game_phase.maintenence:
                //카드 3장을 드로우 한 뒤 손패에 4장만 남아있도록 나머지를 버림 (카드 3장 드로우 이벤트, 버리기 이벤트)
                StartCoroutine("maintenence_phase");
                break;

            case game_phase.departure:
                //다음 방으로 이동 (이동할 방향 선택, 맵에서 좌표 이동)

                next_game_phase = game_phase.enter;
                break;
        }
    }

    IEnumerator action_phase()
    {
        Room now_room = map.return_now_room();

        now_room.SendMessage("active_apply_card");
        //핸드쪽에 카드 사용 가능하게 해줌


        next_game_phase = game_phase.maintenence;
        yield return null;
    }

    IEnumerator maintenence_phase()
    {

        next_game_phase = game_phase.departure;
        yield return null;
    }

    void next_phase()
    {
        now_game_phase = next_game_phase;

        phase_progress();
    }

    void gameover()
    {
        Debug.Log("게임 오버");
    }
}
