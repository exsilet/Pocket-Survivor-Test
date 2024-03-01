using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Item
{
    public abstract class CharactersStatModifierSO : ScriptableObject
    {
        public abstract void AffectCharacter(GameObject character, int value, bool head);
    }
}