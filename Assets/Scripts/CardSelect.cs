using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour {

    List<Card> card_list;

    public Button confirm_but;
    public Button[] select_card_but_arr;
    public Button[] deselect_card_but_arr;

    List<int> select_card_num_list;
    int select_card_cnt;

    int now_select_card_cnt = 0;

    void card_image_insert()
    {
        //카드 선택창이 켜질때 이미지값 조정  
        
    }

    public void test_start()
    {
        card_select_start(test_result, 3);

        for(int i = 0; i < select_card_but_arr.Length; i++)
        {
            select_card_but_arr[i].gameObject.SetActive(true);
        }
        for (int j = 0; j < select_card_but_arr.Length; j++)
        {
            deselect_card_but_arr[j].gameObject.SetActive(false);
        }

    }

    void test_result()
    {
        string temp_string = "선택된 카드 : ";

        for(int i = 0; i < select_card_num_list.Count; i++)
        {
            temp_string += select_card_num_list[i] + " ";
        }
        Debug.Log(temp_string);
    }

    #region 선택 완료 버튼 함수
    void delete_listener()
    {
        confirm_but.onClick.RemoveAllListeners();
    }

    void but_func_insert(System.Action func)
    {
        //버튼에 받은 함수포인터로 클릭 이벤트 지정
        confirm_but.onClick.AddListener(() => func());
        confirm_but.onClick.AddListener(() => delete_listener());
    }
    #endregion

    public void card_select_start(System.Action func, int card_cnt)
    {
        //시작할 때 데이터값 정리
        select_card_cnt = card_cnt;

        //select_card_num_arr = new int[select_card_cnt];
        select_card_num_list = new List<int>();

        //선택 완료 버튼에 이벤트 넣어줌
        but_func_insert(func);
    }

    public void select_card(int select_card_num)
    {
        Debug.Log(select_card_num + "번 선택됨");
        if (now_select_card_cnt >= select_card_cnt)
        {
            Debug.Log("error 72157 - 더 이상 선택할 수 없음");
            return;
        }
        select_card_num_list.Add(select_card_num);

        now_select_card_cnt++;

        deselect_card_but_arr[select_card_num].gameObject.SetActive(true);
        select_card_but_arr[select_card_num].gameObject.SetActive(false);
    }
    
    public void deselect_card(int select_card_num)
    {
        Debug.Log(select_card_num + "번 선택취소됨");
        if (now_select_card_cnt <= 0)
        {
            Debug.Log("error 98321 - 취소할 대상이 없음");
            return;
        }

        now_select_card_cnt--;

        select_card_num_list.Remove(select_card_num);

        deselect_card_but_arr[select_card_num].gameObject.SetActive(false);
        select_card_but_arr[select_card_num].gameObject.SetActive(true);
    }

    public List<int> return_select_card_num_list()
    {
        return select_card_num_list;
    }
}
