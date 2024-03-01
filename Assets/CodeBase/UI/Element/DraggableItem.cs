using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.UI.Element
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Item _item;

        [HideInInspector] public Transform parentAfterDrag;

        private InventorySlot _slot;
        public InventorySlot Slot => _slot;

        private void Start()
        {
            _slot = _item.Slot;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            parentAfterDrag = transform.parent;
            _slot = parentAfterDrag.GetComponent<InventorySlot>();
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            _icon.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(parentAfterDrag);
            _slot = parentAfterDrag.GetComponent<InventorySlot>();
            _item.UpdateSlot(_slot);
            _icon.raycastTarget = true;
        }
    }
}
