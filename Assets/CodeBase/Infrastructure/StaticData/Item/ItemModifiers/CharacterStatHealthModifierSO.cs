using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Item.ItemModifiers
{
    [CreateAssetMenu(fileName = "CharactersHealthStatModifierData", menuName = "StaticData/ItemModifier/Health")]
    public class CharacterStatHealthModifierSO : CharactersStatModifierSO
    {
        public override void AffectCharacter(GameObject character, int value, bool head)
        {
            HeroHealth health = character.GetComponent<HeroHealth>();
            if (health != null)
                health.UseHealth(value);
        }
    }
}