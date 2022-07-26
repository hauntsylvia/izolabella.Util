using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using izolabella.Storage.Objects.Structures;
using izolabella.Util.People.Inner;

namespace izolabella.Util.People
{
    public class Person : IDataStoreEntity
    {
        public Person(string DisplayName, PronounSet[] Pronouns, ulong? Id = null)
        {
            this.DisplayName = DisplayName;
            this.Pronouns = Pronouns;
            this.Id = Id ?? IdGenerator.CreateNewId();
        }

        [JsonPropertyName("DisplayName")]
        public string DisplayName { get; }

        [JsonPropertyName("Pronouns")]
        public PronounSet[] Pronouns { get; }

        [JsonPropertyName("Id")]
        public ulong Id { get; }
    }
}
