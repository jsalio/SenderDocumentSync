using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DocumentSender.RestManagers
{
    /// <summary>
    ///
    /// </summary>
    public sealed class JsonHandlerAdapter
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        /// <summary>
        /// Creates a instance of an implementation of <see cref="IJsonHandlerPort"/>
        /// </summary>
        public JsonHandlerAdapter()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        /// <summary>
        /// Deserializes a passed json.
        /// </summary>
        /// <typeparam name="T">Represents a type of value which a json will become.</typeparam>
        /// <param name="json">Represents a json value.</param>
        /// <returns>The deserialized json.</returns>
        public T Deserialize<T>(string json)
            => JsonConvert.DeserializeObject<T>(json);

        /// <summary>
        /// Serializes a passed value into a json.
        /// </summary>
        /// <typeparam name="T">Represents a type of value which a json will represents.</typeparam>
        /// <param name="entity">Represents an capture model.</param>
        /// <returns>The deserialized json.</returns>
        public string Serialize<T>(T entity)
            => JsonConvert.SerializeObject(entity, _jsonSerializerSettings);

        string Serialize<T>(T entity, bool useCurrentSettings)
        {
            if (useCurrentSettings)
            {
                return JsonConvert.SerializeObject(entity, _jsonSerializerSettings);
            }

            return JsonConvert.SerializeObject(entity);
        }
    }
}
