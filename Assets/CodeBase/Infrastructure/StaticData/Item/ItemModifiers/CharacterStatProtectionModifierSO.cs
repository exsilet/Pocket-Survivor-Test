using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Item.ItemModifiers
{
    [CreateAssetMenu(fileName = "CharactersProtectionStatModifierData", menuName = "StaticData/ItemModifier/Protection")]
    public class CharacterStatProtectionModifierSO : CharactersStatModifierSO
    {
        public override void AffectCharacter(GameObject character, int value, bool head)
        {
            HeroHealth health = character.GetComponent<HeroHealth>();
            if (health != null)
                health.AddProtection(value, head);
        }
    }
}