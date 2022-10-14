using System.Collections.Generic;

namespace Ardalis.SmartEnum.JsonNet
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class SmartArrayEnumNameConverter<TEnum, TValue>:JsonConverter<IEnumerable<TEnum>>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, IEnumerable<TEnum> value, JsonSerializer serializer)
        {
            if (value is null)
                writer.WriteNull();
            else
            {
                try
                {
                    writer.WriteStartArray();
                    foreach (TEnum item in value)
                    {
                        writer.WriteValue(item.Name);
                    }
                    writer.WriteEndArray();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }

        public override IEnumerable<TEnum> ReadJson(JsonReader reader, Type objectType, IEnumerable<TEnum> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartArray:

                    JToken token = JToken.Load(reader);
                    List<string> items = token.ToObject<List<string>>();
                    return GetFromName(items);

                default:
                    throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing a smart enum.");
            }

            static IEnumerable<TEnum> GetFromName(IEnumerable<string> names)
            {
                var list = new List<TEnum>();
                foreach (var name in names)
                {
                    try
                    {
                        var value = SmartEnum<TEnum, TValue>.FromName(name, false);
                        list.Add(value);
                    }
                    catch (Exception ex)
                    {
                        throw new JsonSerializationException($"Error converting value '{name}' to a smart enum.", ex);
                    }
                }
                return list;
            }
        }
    }
}