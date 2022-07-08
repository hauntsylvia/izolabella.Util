using izolabella.Storage.Objects.DataStores;
using izolabella.Util.Controllers.Profiles;

namespace izolabella.Util.Controllers
{
    public abstract class Controller
    {
        public Controller()
        {
            this.OnMessageAsync += this.MessagePumpedAsync;
        }

        private Task MessagePumpedAsync(string Message)
        {
            IzolabellaConsole.IzolabellaConsole.Write(this.Alias, Message);
            return Task.CompletedTask;
        }

        public abstract string Alias { get; }

        public bool Enabled { get; private set; }

        public ControllerProfile? LastProfile { get; private set; }

        public async Task StartAsync(ControllerProfile Profile)
        {
            this.LastProfile = Profile;
            this.Enabled = true;
            await this.StartProtectedAsync(Profile);
        }

        public async Task StopAsync()
        {
            this.Enabled = false;
            await this.StopProtectedAsync();
        }

        protected abstract Task StartProtectedAsync(ControllerProfile Profile);

        protected abstract Task StopProtectedAsync();

        public void Update(string Message)
        {
            this.OnMessageAsync?.Invoke(Message);
        }

        public static async Task UpdateProfileAsync(DataStore SavesTo, ControllerProfile? P, Action<ControllerProfile> AOnP)
        {
            if(P != null)
            {
                AOnP.Invoke(P);
                await SavesTo.SaveAsync(P);
            }
        }

        public delegate Task OnControllerMessageAsyncHandler(string Message);
        public event OnControllerMessageAsyncHandler? OnMessageAsync;
    }
}
