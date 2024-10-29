using Exerussus._1Gears.Core;
using Exerussus._1Gears.Custom;
using UnityEngine.EventSystems;

namespace Exerussus._1Gears.MonoBehaviours
{
    public class Highlighter : GearComponent, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isActivated) return;
            GearsCore.Pooler.InvokeStartHighlight(EntityPack.Id);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isActivated) return;
            GearsCore.Pooler.InvokeStopHighlight(EntityPack.Id);
        }

        protected override void OnActivate(int entity, GearsPooler pooler)
        {
            GearsCore.Pooler.Highlighter.Add(entity);
        }

        protected override void OnDeactivate(int entity, GearsPooler pooler)
        {
            if (IsQuitting) return;
            GearsCore.Pooler.Highlighter.Del(entity);
        }
    }
}
