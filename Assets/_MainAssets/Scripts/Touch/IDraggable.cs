using UnityEngine;

namespace _MainAssets.Scripts.Touch
{
    public interface IDraggable
    {
        void OnBeginDrag(Vector2 position);
        void OnDrag(Vector2 position);
        void OnEndDrag();
    }
}