using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Entitas;
using FixMath;

public class InfoDisplayCmp : MonoBehaviour
{
    [SerializeField]
    private Text _infoTxt;

    private GameContext _context;
    private IGroup<GameEntity> _playerGroup;

    private void Start()
    {
        _context = Contexts.sharedInstance.game;
        _playerGroup = _context.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Direction));
    }

    private void Update()
    {
        string txt = "";
        foreach(GameEntity entity in _playerGroup.GetEntities())
        {
            int playerId = entity.playerId.value;
            FixVec2 pos = entity.position.value;
            Fix64 dir = entity.direction.value;
            txt += playerId + " " + pos.ToVector2().ToString(Config.PrecisionFormat) + " " + ((float)dir).ToString(Config.PrecisionFormat) + "\n";
        }
        _infoTxt.text = txt;
    }

}