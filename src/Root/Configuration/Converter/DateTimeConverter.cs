using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.DateTimeKind;

namespace Root.Configuration.Converter
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, Utc).AddSeconds(reader.GetInt64());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(new DateTimeOffset(value).ToUnixTimeSeconds());
        }
    }
}