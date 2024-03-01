using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Item
{
    public interface IItemAction
    {
        public string ActionName { get; }
        public bool PerformAction(GameObject character);
    }
}