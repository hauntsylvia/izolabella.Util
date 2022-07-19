using System.Reflection;
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

        /// <summary>
        /// The inner method for displaying messages to the console.
        /// </summary>
        /// <param name="Message">The message to display.</param>
        /// <returns></returns>
        private Task MessagePumpedAsync(string Message, bool IsLower = true)
        {
            IzolabellaConsole.IzolabellaConsole.Write(this.Name, Message, IsLower);
            return Task.CompletedTask;
        }

        /// <summary>
        /// The alias of this controller.
        /// </summary>
        public abstract string Name { get; }

        public virtual bool NeedsProfileToken { get; } = true;

        /// <summary>
        /// Whether the active state of this controller is enabled.
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// The last profile this controller was started with.
        /// </summary>
        public ControllerProfile? LastProfile { get; private set; }

        /// <summary>
        /// Starts the controller.
        /// </summary>
        /// <param name="Profile">The profile used to start this controller.</param>
        /// <returns></returns>
        public Task StartAsync(ControllerProfile Profile)
        {
            this.LastProfile = Profile;
            this.Enabled = true;
            return this.StartProtectedAsync(Profile);
        }

        /// <summary>
        /// Stops the controller.
        /// </summary>
        /// <returns></returns>
        public Task StopAsync()
        {
            this.Enabled = false;
            return this.StopProtectedAsync();
        }

        /// <summary>
        /// The inner method used for implementing classes to start this controller.
        /// </summary>
        /// <param name="Profile">The profile used to start this controller.</param>
        /// <returns></returns>
        protected abstract Task StartProtectedAsync(ControllerProfile Profile);

        /// <summary>
        /// The inner method used for implementing classes to stop this controller.
        /// </summary>
        /// <returns></returns>
        protected abstract Task StopProtectedAsync();

        /// <summary>
        /// Send a message to whomever is listening . . .
        /// </summary>
        /// <param name="Message">The message to send.</param>
        public void Update(string Message)
        {
            this.OnMessageAsync?.Invoke(Message);
        }

        /// <summary>
        /// A method used for updating controller profiles.
        /// </summary>
        /// <param name="SavesTo">The <see cref="DataStore"/> the profile will save to.</param>
        /// <param name="P">The profile to save.</param>
        /// <param name="AOnP">The action to take on the profile before saving.</param>
        /// <returns></returns>
        public static async Task UpdateProfileAsync(DataStore SavesTo, ControllerProfile? P, Action<ControllerProfile> AOnP)
        {
            if(P != null)
            {
                AOnP.Invoke(P);
                await SavesTo.SaveAsync(P);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public delegate Task OnControllerMessageAsyncHandler(string Message, bool IsLower = true);

        /// <summary>
        /// When a message is received, this event will fire.
        /// </summary>
        public event OnControllerMessageAsyncHandler? OnMessageAsync;

        private static List<Controller> CachedInstances { get; } = new();

        public static IEnumerable<Controller> GetControllers(Assembly?[]? BelongingTo = null)
        {
            List<Controller> NewInstances = BaseImplementationUtil.GetItems<Controller>(BelongingTo ?? AppDomain.CurrentDomain.GetAssemblies());
            CachedInstances.AddRange(NewInstances.Where(NewInstance => !CachedInstances.Any(CachedInstance => CachedInstance.Name == NewInstance.Name)));
            return CachedInstances;
        }
    }
}
