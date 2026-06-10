using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Deteleschuer.nl.Helpers;

public static class SessionExtensions // static omdat ik hem nooit aan hoef te maken met new ik roep de methods gewoon rechtsreeks aan 
{
    public static void Set<T>(this ISession session, string key, T value) // t staat voor ik weet nog niet welk type dat word bepaald welke methode ik aanroep
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}