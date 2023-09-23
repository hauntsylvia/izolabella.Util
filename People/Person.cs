using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using izolabella.Storage.Objects.Structures;
using izolabella.Util.People.Inner;
using Newtonsoft.Json;

namespace izolabella.Util.People
{
    [method: JsonConstructor]
    public class Person(string DisplayName, PronounSet[] Pronouns, ulong? Id = null) : IDataStoreEntity
    {
        private string displayName = DisplayName;
        [JsonProperty(nameof(DisplayName))]
        public string DisplayName { get => this.displayName; set => this.displayName = value.Length >= 32 ? value[..32] : value; }

        [JsonProperty(nameof(Pronouns))]
        public PronounSet[] Pronouns { get; set; } = Pronouns;

        [JsonProperty(nameof(Id))]
        public ulong Id { get; } = Id ?? IdGenerator.CreateNewId();
    }
}