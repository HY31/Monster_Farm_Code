using UnityEngine;
using UnityEngine.EventSystems;

public class TimeScaleBtn : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UIManager uiManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            switch(this.name){
                case "TimeScale*0":
                    uiManager.ChangeTimeScale(TimeScale.TimeScaleValue.PAUSE);
                    break;
                case "TimeScale*1":
                    uiManager.ChangeTimeScale(TimeScale.TimeScaleValue.ONE);
                    break;
                case "TimeScale*2":
                    uiManager.ChangeTimeScale(TimeScale.TimeScaleValue.TWO);
                    break;
                case "TimeScale*3":
                    uiManager.ChangeTimeScale(TimeScale.TimeScaleValue.THREE);
                    break;
                default:
                    Debug.Log("타임스케일 버튼 문제");
                    break;
            }
        }
    }
}
