using izolabella.Storage.Objects.DataStores;
using izolabella.Storage.Objects.Structures;
using izolabella.Util;
using Newtonsoft.Json;

namespace izolabella.Util.Controllers.Profiles;

public class ControllerProfile : IDataStoreEntity
{
    public ControllerProfile(string Alias, string Token, bool ControllerEnabled, ulong? Id = null)
    {
        this.Alias = Alias;
        this.Token = Token;
        this.ControllerEnabled = ControllerEnabled;
        this.Id = Id ?? IdGenerator.CreateNewId();
    }

    internal ControllerProfile(string Alias, string Token, string DiscordBotToken, bool ControllerEnabled, ulong? Id = null)
    {
        this.Alias = Alias;
        this.Token = Token;
        this.DiscordBotToken = DiscordBotToken;
        this.ControllerEnabled = ControllerEnabled;
        this.Id = Id ?? IdGenerator.CreateNewId();
    }

    /// <summary>
    /// The alias of this profile. Must match the alias of the controller to be passed.
    /// </summary>
    public string Alias { get; }

    /// <summary>
    /// The secret token of this profile.
    /// </summary>
    public string Token { get; private set; }

    [JsonProperty("DiscordBotToken")]
    private string DiscordBotToken { set => this.Token = value; }

    /// <summary>
    /// Whether the profile should be enabled.
    /// </summary>
    public bool ControllerEnabled { get; set; }

    /// <summary>
    /// The unique <see cref="IDataStoreEntity"/> identifier for <see cref="DataStore"/>s.
    /// </summary>
    public ulong Id { get; }
}
