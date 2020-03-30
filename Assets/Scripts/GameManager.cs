using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum game_phase
{
    none = 0,
    turn_start = 1,
    enter = 2,  //진입
    action = 3, //행동
    maintenence = 4,    //정비
    departure = 5,   //이탈
    turn_end = 6
}

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    game_phase now_game_phase;
    game_phase next_game_phase;

    Map map;
    InHand inhand;
    CardEffect cardEffect;
    Player player;
    Deck deck;

    bool progress_waiting = false;

    bool is_ban_move = false;
    bool is_ban_use_card = false;

    void Awake()
    {
        gameManager = this;

        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        inhand = GameObject.FindGameObjectWithTag("InHand").GetComponent<InHand>();
        cardEffect = GameObject.FindGameObjectWithTag("CardEffect").GetComponent<CardEffect>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent <Player>();
        deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
    }

    public void test_game_start()
    {
        now_game_phase = game_phase.turn_start;

        phase_progress();
    }

    void phase_progress()
    {
        switch (now_game_phase)
        {
            case game_phase.turn_start:
                StartCoroutine("turn_start_phase");
                break;

            case game_phase.enter:
                //방에 입장 (값 초기화)
                next_game_phase = game_phase.action;

                //일단 임시로
                next_phase("enter progress");
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

            case game_phase.turn_end:
                StartCoroutine("turn_end_phase");
                break;
        }
    }

    #region 각 페이즈별 코루틴

    IEnumerator turn_start_phase()
    {
        next_game_phase = game_phase.enter;

        player.SendMessage("turn_start");
        inhand.SendMessage("turn_start");

        //일단 임시로
        next_phase("turn_start_phase");
        yield return null;
    }

    IEnumerator action_phase()
    {
        next_game_phase = game_phase.maintenence;

        //방에 적용된 카드 사용
        Room now_room = map.return_now_room();

        now_room.SendMessage("active_apply_card");

        //핸드쪽에 카드 사용 가능하게 해줌
        if (!is_ban_use_card)
        {
            inhand.SendMessage("select_card_start");

            yield return StartCoroutine("progress_wait");
        }
        else
        {
            Debug.Log("카드 사용 불가로 인해 카드 사용이 금지됨");
        }

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
        next_game_phase = game_phase.turn_end;

        //방 이동 가능하게 해줌
        if (!is_ban_move)
        {
            map.SendMessage("move_map_start");
        }
        else
        {
            Debug.Log("이동불가로 인해 이동 할 수 없음");
            next_phase("departure_phase");
        }
        yield return null;
    }

    IEnumerator turn_end_phase()
    {
        next_game_phase = game_phase.turn_start;

        player.SendMessage("turn_end");
        inhand.SendMessage("turn_end");

        //일단 임시로
        next_phase("turn_end_phase");
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

    void next_phase(string test)
    {
        Debug.Log(test + "에서 시작");

        now_game_phase = next_game_phase;

        Debug.Log("페이즈 시작 - " + now_game_phase);

        phase_progress();
    }

    void forced_phase(game_phase game_Phase)
    {
        next_game_phase = game_Phase;

        now_game_phase = next_game_phase;

        Debug.Log("페이즈 시작 - " + now_game_phase);

        phase_progress();
    }

    void gameover()
    {
        Debug.Log("게임 오버");
    }

    void message_player_damage(int damage)
    {
        Debug.Log("메세지 받음 - 플레이어 " + damage + "데미지");
        player.SendMessage("Apply_damage", damage);
    }

    void message_player_heal(int heal)
    {
        Debug.Log("메세지 받음 - 플레이어 " + heal + "힐");
        player.SendMessage("Restore_hp", heal);
    }

    void message_deck_damage(int damage)
    {
        Debug.Log("메세지 받음 - 덱 " + damage + "데미지");
        deck.SendMessage("deck_apply_damage", damage);
    }

    void message_forced_move(int turn)
    {
        Debug.Log("메세지 받음 - 강제이동 " + turn + "턴 전");
        map.SendMessage("forced_move_room", turn);
    }

    void message_ban_move(int turn)
    {
        Debug.Log("메세지 받음 - 이동 불가 " + turn + "턴");
        player.SendMessage("ban_move", turn);
    }

    void message_ban_use_card(int turn)
    {
        Debug.Log("메세지 받음 - 카드 사용 불가 " + turn + "턴");
        inhand.SendMessage("ban_use_card", turn);
    }

    void now_player_state(bool move_ban)
    {
        if(move_ban)
            Debug.Log("이동 밴");
        is_ban_move = move_ban;
    }

    void now_inhand_state(bool use_card_ban)
    {
        if(use_card_ban)
            Debug.Log("카드 밴");
        is_ban_use_card = use_card_ban;
    }
}
