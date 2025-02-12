using System;
using Newtonsoft.Json;
using Passbook.Generator.Extensions;

namespace Passbook.Generator.Tags
{
    public abstract class SemanticTagBaseValue : SemanticTag
    {
        private readonly object _value;

        public SemanticTagBaseValue(string tag, string value) : base(tag)
        {
            _value = value;
        }

        public SemanticTagBaseValue(string tag, bool value) : base(tag)
        {
            _value = value;
        }

        public SemanticTagBaseValue(string tag, double value) : base(tag)
        {
            _value = value;
        }
        public SemanticTagBaseValue(string tag, DateTimeOffset value) : base(tag)
        {
            _value = value;
        }

        public override void WriteValue(JsonWriter writer)
        {
            if (_value is DateTimeOffset dateTimeOffset)
            {
                writer.WriteDateTimeValue(dateTimeOffset);
            }
            else
            {
                writer.WriteValue(_value);
            }
        }
    }
}