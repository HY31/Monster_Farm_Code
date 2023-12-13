using System;
using System.Collections.Generic;
using UnityEngine;

// 단일 책임 원칙 (Single Responsibility Principle): 각 클래스는 하나의 주요 기능을 담당한다.
// FloorActivationStrategy는 특정 층을 활성화하는 전략을 담당하고, FloorController_Fix는 전략들을 초기화하고 사용하는 책임을 갖고 있다.
public interface IFloorActivationStrategy  // 인터페이스 : 전략들이 구현해야 하는 공통된 인터페이스
{
    void Activate();
}

public class FloorActivationStrategy : IFloorActivationStrategy  // 콘크리트 전략 클래스 : 각 전략을 실제로 구현하는 클래스
{
    private GameObject floor;

    public FloorActivationStrategy(GameObject floor)
    {
        this.floor = floor;
    }

    public void Activate()
    {
        floor.SetActive(true);
    }
}

public class FloorController_Fix : MonoBehaviour  // 콘텍스트 클래스 : 전략 객체를 사용하는 클래스로, 필요에 따라 전략을 동적으로 수정할 수 있다.
                                                  // 유연성: FloorController_Fix 클래스에서 floorStrategies 딕셔너리를 통해 다양한 조건에 따른 전략을 동적으로 추가할 수 있게 함.
                                                  // 이로써 몬스터 수에 따른 활성화 조건을 쉽게 변경할 수 있다.
{
    private Dictionary<Func<int, bool>, IFloorActivationStrategy> floorStrategies = new Dictionary<Func<int, bool>, IFloorActivationStrategy>();
    // Func<int, bool>을 샤옹하는 이유 : 몬스터가 몇 마리 존재하는지와 함께 '몇 마리 이상이면 실행'과 같은 조건을 부여하려고

    private int monsters;

    [Header("FirstFloor")]
    public GameObject firstFloor_First;
    public GameObject firstFloor_Second;

    [Header("SecondFloor")]
    public GameObject secondFloor_First;
    public GameObject secondFloor_Second;

    [Header("ThirdFloor")]
    public GameObject thirdFloor_First;
    public GameObject thirdFloor_Second;

    private void Awake()
    {
        Init();
    }
    void Start()
    {
        monsters = MonsterManager.Instance.GetCurMonsterData().Count;
        InitializeStrategies();

        foreach (var strategy in floorStrategies)
        {
            if (strategy.Key(monsters))
            {
                strategy.Value.Activate();
            }
        }
    }

    private void InitializeStrategies()
    {
        // monsterCount가 특정 몬스터 수 이상이면 전략 실행
        floorStrategies.Add(monsterCount => monsterCount >= 1, new FloorActivationStrategy(firstFloor_First));
        floorStrategies.Add(monsterCount => monsterCount >= 3, new FloorActivationStrategy(firstFloor_Second));
        floorStrategies.Add(monsterCount => monsterCount >= 5, new FloorActivationStrategy(secondFloor_First));
        floorStrategies.Add(monsterCount => monsterCount >= 8, new FloorActivationStrategy(secondFloor_Second));
        floorStrategies.Add(monsterCount => monsterCount >= 11, new FloorActivationStrategy(thirdFloor_First));
        floorStrategies.Add(monsterCount => monsterCount >= 13, new FloorActivationStrategy(thirdFloor_Second));
    }
    // 확장성: 코드는 현재 3개의 층에 대한 조건과 전략을 다루고 있지만, 나중에 더 많은 층이나 다른 조건이 추가되어도 유연하게 대처할 수 있다.

    private void Init()
    {
        firstFloor_First.SetActive(false);
        firstFloor_Second.SetActive(false);
        secondFloor_First.SetActive(false);
        secondFloor_Second.SetActive(false);
        thirdFloor_First.SetActive(false);
        thirdFloor_Second.SetActive(false);
    }
}



