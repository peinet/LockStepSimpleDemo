using Protocol;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlDirCmp : MonoBehaviour
{
    [SerializeField]
    private GameObject _controlBtn;

    private GameContext _context;

    private void Start()
    {
        _context = Contexts.sharedInstance.game;

        EventTrigger eventTrigger = _controlBtn.AddComponent<EventTrigger>();
        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerDown;
        clickEntry.callback = new EventTrigger.TriggerEvent();
        clickEntry.callback.AddListener(OnClick);
        eventTrigger.triggers.Add(clickEntry);

        EventTrigger.Entry moveEntry = new EventTrigger.Entry();
        moveEntry.eventID = EventTriggerType.Drag;
        moveEntry.callback = new EventTrigger.TriggerEvent();
        moveEntry.callback.AddListener(OnMove);
        eventTrigger.triggers.Add(moveEntry);

        EventTrigger.Entry upEntry = new EventTrigger.Entry();
        upEntry.eventID = EventTriggerType.PointerUp;
        upEntry.callback = new EventTrigger.TriggerEvent();
        upEntry.callback.AddListener(OnPointUp);
        eventTrigger.triggers.Add(upEntry);
    }

    private void OnClick(BaseEventData arg0)
    {
        PointerEventData eventData = (PointerEventData)arg0;
        //GameEntity entity = _context.CreateEntity();
        //entity.AddPlayerId(Config.SelfId);
        //entity.AddSteerPosition(eventData.position);
        // 发送到帧同步服务器
        SteerPositionReq req = new SteerPositionReq();
        req.X = eventData.position.x;
        req.Y = eventData.position.y;
        LockStepClientMgr.GetInstance().SendMsg(MsgID.SteerPositionReq, req);
    }

    private void OnMove(BaseEventData arg0)
    {
        OnClick(arg0);
    }

    private void OnPointUp(BaseEventData arg0)
    {
        //GameEntity entity = _context.CreateEntity();
        //entity.AddPlayerId(Config.SelfId);
        //entity.AddSteerPosition(Vector2.zero);
        // 发送到帧同步服务器
        SteerPositionReq req = new SteerPositionReq();
        req.X = 0;
        req.Y = 0;
        LockStepClientMgr.GetInstance().SendMsg(MsgID.SteerPositionReq, req);
    }

}