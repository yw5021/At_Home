using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Text hp_text;

    int now_hp;
    public int _now_hp;

    bool is_ban_move = false;
    int ban_move_turn_cnt;

    void turn_start()
    {
        GameManager.gameManager.SendMessage("now_player_state", is_ban_move);
    }

    void turn_end()
    {
        if (is_ban_move)
        {
            ban_move_turn_cnt--;

            if(ban_move_turn_cnt <= 0)
            {
                Debug.Log("이동 불가 해제");
                is_ban_move = false;
            }
        }
    }

    void ban_move(int turn)
    {
        Debug.Log("이동불가 " + turn + "턴 효과 적용");
        is_ban_move = true;

        ban_move_turn_cnt = turn;
        GameManager.gameManager.SendMessage("now_player_state", is_ban_move);
    }

    #region 체력 데이터 증감
    void Restore_hp(int Recovery)
    {
        now_hp += Recovery;

        output_hp();
    }

    void Apply_damage(int Damage)
    {
        now_hp -= Damage;
        
        check_hp();

        output_hp();
    }
    #endregion
    
    #region 체력 데이터 출력
    void output_hp()
    {
        Debug.Log("현재 hp = " + now_hp);

        hp_text.text = "현재 hp = " + now_hp;
    }
    #endregion

    #region 체력 0 판정
    void check_hp()
    {
        if(now_hp <= 0)
        {
            //임시
            Debug.Log("게임 오버");
        }
    }
    #endregion

    void player_init()
    {
        now_hp = _now_hp;

        output_hp();
    }

    private void Awake()
    {
        player_init();
    }
}
