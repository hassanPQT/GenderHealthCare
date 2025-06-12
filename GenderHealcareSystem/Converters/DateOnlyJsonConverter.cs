using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GenderHealcareSystem.Converters
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly?>
    {
        private readonly string[] _formats = new[] { "d-M-yyyy", "dd-MM-yyyy" };

        public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();

            if (string.IsNullOrWhiteSpace(str))
                return null;

            if (DateOnly.TryParseExact(str, _formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;

            throw new JsonException($"Invalid date format. Use one of: {string.Join(", ", _formats)}");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString("dd-MM-yyyy")); // chỉ chọn 1 format để ghi ra
            else
                writer.WriteNullValue();
        }
    }
}
