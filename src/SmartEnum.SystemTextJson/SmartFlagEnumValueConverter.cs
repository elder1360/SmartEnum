namespace Ardalis.SmartEnum.SystemTextJson
{
    using System;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class SmartFlagEnumValueConverter<TEnum, TValue> : JsonConverter<TEnum>
    where TEnum : SmartFlagEnum<TEnum, TValue>
    where TValue: struct, IEquatable<TValue>, IComparable<TValue>
    {
        public override bool HandleNull => true;

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                throw new JsonException($"Error converting value 'Null' to a smart flag enum.");

            return GetFromValue(ReadValue(ref reader));
        }

        private TEnum GetFromValue(TValue value)
        {
            try
            {
                return SmartFlagEnum<TEnum, TValue>.DeserializeValue(value);
            }
            catch (Exception ex)
            {
                throw new JsonException($"Error converting value '{value}' to a smart flag enum.", ex);
            }
        }

        private TValue ReadValue(ref Utf8JsonReader reader)
        {
            try
            {
                if (typeof(TValue) == typeof(bool))
                    return (TValue)(object)reader.GetBoolean();
                if (typeof(TValue) == typeof(byte))
                    return (TValue)(object)reader.GetByte();
                if (typeof(TValue) == typeof(sbyte))
                    return (TValue)(object)reader.GetSByte();
                if (typeof(TValue) == typeof(short))
                    return (TValue)(object)reader.GetInt16();
                if (typeof(TValue) == typeof(ushort))
                    return (TValue)(object)reader.GetUInt16();
                if (typeof(TValue) == typeof(int))
                    return (TValue)(object)reader.GetInt32();
                if (typeof(TValue) == typeof(uint))
                    return (TValue)(object)reader.GetUInt32();
                if (typeof(TValue) == typeof(long))
                    return (TValue)(object)reader.GetInt64();
                if (typeof(TValue) == typeof(ulong))
                    return (TValue)(object)reader.GetUInt64();
                if (typeof(TValue) == typeof(float))
                    return (TValue)(object)reader.GetSingle();
                if (typeof(TValue) == typeof(double))
                    return (TValue)(object)reader.GetDouble();
                if (typeof(TValue) == typeof(string))
                    return (TValue)(object)reader.GetString();
            }
            catch (InvalidOperationException ex)
            {
                throw new JsonException($"Error converting token '{reader.TokenType}' to a smart flag enum.", ex);
            }

            throw new ArgumentOutOfRangeException(typeof(TValue).ToString(), $"{typeof(TValue).Name} is not supported.");
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteNullValue();
            else if (typeof(TValue) == typeof(bool))
                writer.WriteBooleanValue((bool)(object)value.Value);
            else if (typeof(TValue) == typeof(short))
                writer.WriteNumberValue((int)(short)(object)value.Value);
            else if (typeof(TValue) == typeof(int))
                writer.WriteNumberValue((int)(object)value.Value);
            else if (typeof(TValue) == typeof(double))
                writer.WriteNumberValue((double)(object)value.Value);
            else if (typeof(TValue) == typeof(decimal))
                writer.WriteNumberValue((decimal)(object)value.Value);
            else if (typeof(TValue) == typeof(ulong))
                writer.WriteNumberValue((ulong)(object)value.Value);
            else if (typeof(TValue) == typeof(uint))
                writer.WriteNumberValue((uint)(object)value.Value);
            else if (typeof(TValue) == typeof(float))
                writer.WriteNumberValue((float)(object)value.Value);
            else if (typeof(TValue) == typeof(long))
                writer.WriteNumberValue((long)(object)value.Value);
            else
                writer.WriteStringValue(value.Value.ToString());
        }
    }
}
