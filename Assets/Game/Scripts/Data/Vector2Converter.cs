using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace StbData {
public class Vector2Converter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Vector2);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jObject = JObject.Load(reader);
        var x = jObject["x"].Value<float>();
        var y = jObject["y"].Value<float>();
        return new Vector2(x, y);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var vector = (Vector2)value;
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(vector.x);
        writer.WritePropertyName("y");
        writer.WriteValue(vector.y);
        writer.WriteEndObject();
    }
}
}