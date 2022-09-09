using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using izolabella.Storage.Objects.Structures;
using izolabella.Util.People.Inner;
using Newtonsoft.Json;

namespace izolabella.Util.People;

public class Person : IDataStoreEntity
{
    [JsonConstructor]
    public Person(string DisplayName, PronounSet[] Pronouns, ulong? Id = null)
    {
        this.displayName = DisplayName;
        this.Pronouns = Pronouns;
        this.Id = Id ?? IdGenerator.CreateNewId();
    }

    private string displayName;
    [JsonProperty("DisplayName")]
    public string DisplayName { get => this.displayName; set => this.displayName = value.Length >= 32 ? value[..32] : value; }

    [JsonProperty("Pronouns")]
    public PronounSet[] Pronouns { get; set; }

    [JsonProperty("Id")]
    public ulong Id { get; }
}
