using Newtonsoft.Json;

namespace KrakenBot
{
    public struct JsonConfig
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("prefix")]
        public string Prefix { get; private set; }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                JsonConfig jc = (JsonConfig)obj;
                return (Token == jc.Token) && (Prefix == jc.Prefix);
            }
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Token, Prefix);
        }

        public static bool operator ==(JsonConfig left, JsonConfig right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(JsonConfig left, JsonConfig right)
        {
            return !(left == right);
        }
    }
}
