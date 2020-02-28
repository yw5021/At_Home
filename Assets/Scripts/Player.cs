using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    int now_hp;

    #region 체력 데이터 증감
    void Restore_hp(int Recovery)
    {
        now_hp += Recovery;
    }

    void Apply_damage(int Damage)
    {
        now_hp -= Damage;
    }
    #endregion

    #region 체력 데이터 출력
    void output_hp()
    {
        //임시
        Debug.Log("현재 hp = " + now_hp);
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


    #region 체력값 반환
    int return_player_hp()
    {
        return now_hp;
    }
    #endregion
}
