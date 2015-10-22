using Discord.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discord
{
	internal sealed class Servers : AsyncCollection<Server>
	{
		public Servers(DiscordClient client, object writerLock)
			: base(client, writerLock, x => x.OnCached(), x => x.OnUncached()) { }

		public Server GetOrAdd(string id)
			=> base.GetOrAdd(id, () => new Server(_client, id));
	}

	public class ServerEventArgs : EventArgs
	{
		public Server Server { get; }
		public string ServerId => Server.Id;

		internal ServerEventArgs(Server server) { Server = server; }
	}

	public partial class DiscordClient
	{
		public event EventHandler<ServerEventArgs> ServerCreated;
		private void RaiseServerCreated(Server server)
		{
			if (ServerCreated != null)
				RaiseEvent(nameof(ServerCreated), () => ServerCreated(this, new ServerEventArgs(server)));
		}
		public event EventHandler<ServerEventArgs> ServerDestroyed;
		private void RaiseServerDestroyed(Server server)
		{
			if (ServerDestroyed != null)
				RaiseEvent(nameof(ServerDestroyed), () => ServerDestroyed(this, new ServerEventArgs(server)));
		}
		public event EventHandler<ServerEventArgs> ServerUpdated;
		private void RaiseServerUpdated(Server server)
		{
			if (ServerUpdated != null)
				RaiseEvent(nameof(ServerUpdated), () => ServerUpdated(this, new ServerEventArgs(server)));
		}
		public event EventHandler<ServerEventArgs> ServerUnavailable;
		private void RaiseServerUnavailable(Server server)
		{
			if (ServerUnavailable != null)
				RaiseEvent(nameof(ServerUnavailable), () => ServerUnavailable(this, new ServerEventArgs(server)));
		}
		public event EventHandler<ServerEventArgs> ServerAvailable;
		private void RaiseServerAvailable(Server server)
		{
			if (ServerAvailable != null)
				RaiseEvent(nameof(ServerAvailable), () => ServerAvailable(this, new ServerEventArgs(server)));
		}

		/// <summary> Returns a collection of all servers this client is a member of. </summary>
		public IEnumerable<Server> AllServers => _servers;
		internal Servers Servers => _servers;
		private readonly Servers _servers;

		/// <summary> Returns the server with the specified id, or null if none was found. </summary>
		public Server GetServer(string id) => _servers[id];

		/// <summary> Returns all servers with the specified name. </summary>
		/// <remarks> Search is case-insensitive. </remarks>
		public IEnumerable<Server> FindServers(string name)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));

			return _servers.Where(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
		}

		/// <summary> Creates a new server with the provided name and region (see Regions). </summary>
		public async Task<Server> CreateServer(string name, string region)
		{
			CheckReady();
			if (name == null) throw new ArgumentNullException(nameof(name));
			if (region == null) throw new ArgumentNullException(nameof(region));

			var response = await _api.CreateServer(name, region).ConfigureAwait(false);
			var server = _servers.GetOrAdd(response.Id);
			server.Update(response);
			return server;
		}

		/// <summary> Edits the provided server, changing only non-null attributes. </summary>
		public Task EditServer(string serverId, string name = null, string region = null, ImageType iconType = ImageType.Png, byte[] icon = null)
			=> EditServer(_servers[serverId], name: name, region: region, iconType: iconType, icon: icon);
		/// <summary> Edits the provided server, changing only non-null attributes. </summary>
		public async Task EditServer(Server server, string name = null, string region = null, ImageType iconType = ImageType.Png, byte[] icon = null)
		{
			CheckReady();
			if (server == null) throw new ArgumentNullException(nameof(server));

			var response = await _api.EditServer(server.Id, name: name ?? server.Name, region: region, iconType: iconType, icon: icon);
			server.Update(response);
		}

		/// <summary> Leaves the provided server, destroying it if you are the owner. </summary>
		public Task<Server> LeaveServer(Server server)
			=> LeaveServer(server?.Id);
		/// <summary> Leaves the provided server, destroying it if you are the owner. </summary>
		public async Task<Server> LeaveServer(string serverId)
		{
			CheckReady();
			if (serverId == null) throw new ArgumentNullException(nameof(serverId));

			try { await _api.LeaveServer(serverId).ConfigureAwait(false); }
			catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.NotFound) { }
			return _servers.TryRemove(serverId);
		}
	}
}