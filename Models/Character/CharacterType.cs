using System.Text.Json.Serialization;

namespace RPGClient.Models.Character;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CharacterType
{
    Celtic,
    Knight,
    Mage,
    Healer
}