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

    public static GameManager gameManager;

    game_phase now_game_phase;
    game_phase next_game_phase;

    Map map;
    InHand inhand;
    CardEffect cardEffect;

    bool progress_waiting = false;

    void Awake()
    {
        gameManager = this;

        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        inhand = GameObject.FindGameObjectWithTag("InHand").GetComponent<InHand>();
        cardEffect = GameObject.FindGameObjectWithTag("CardEffect").GetComponent<CardEffect>();
    }

    public void test_game_start()
    {
        now_game_phase = game_phase.enter;

        phase_progress();
    }

    void phase_progress()
    {
        switch (now_game_phase)
        {
            case game_phase.enter:
                //방에 입장 (값 초기화)
                next_game_phase = game_phase.action;

                //일단 임시로
                next_phase();
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
                StartCoroutine("departure_phase");
                break;
        }
    }

    #region 각 페이즈별 코루틴
    IEnumerator action_phase()
    {
        next_game_phase = game_phase.maintenence;

        //방에 적용된 카드 사용
        Room now_room = map.return_now_room();

        now_room.SendMessage("active_apply_card");

        //핸드쪽에 카드 사용 가능하게 해줌
        inhand.SendMessage("select_card_start");

        yield return StartCoroutine("progress_wait");

        cardEffect.SendMessage("active_card");

        yield return null;
    }

    IEnumerator maintenence_phase()
    {
        next_game_phase = game_phase.departure;

        //3장 드로우
        for (int i = 0; i < 4; i++)
        {
            inhand.SendMessage("draw_card_inHand");
        }

        //버리기 이벤트 실행
        inhand.SendMessage("abandon_card_start_event");

        yield return null;
    }

    IEnumerator departure_phase()
    {
        next_game_phase = game_phase.enter;

        //방 이동 가능하게 해줌
        map.SendMessage("move_map_start");
        yield return null;
    }
    #endregion


    #region 코루틴 도중에 신호대기 함수
    IEnumerator progress_wait()
    {
        progress_waiting = true;
        yield return new WaitWhile(() => progress_waiting);
    }

    void progress_wait_end()
    {
        Debug.Log("대기 끝");
        progress_waiting = false;
    }
    #endregion

    void next_phase()
    {
        now_game_phase = next_game_phase;

        Debug.Log("페이즈 시작 - " + now_game_phase);

        phase_progress();
    }

    void gameover()
    {
        Debug.Log("게임 오버");
    }
}
