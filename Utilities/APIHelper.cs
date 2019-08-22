using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ObjectModels;
using OpenTidl;
using OpenTidl.Methods;
using OpenTidl.Models;
using OpenTidl.Models.Base;
using Utilities.ConfigurationFolder;

namespace Utilities
{
    public class APIHelper
    {
        public OpenTidlSession session { get; set; }
        public OpenTidlClient client { get; set; }



        public APIHelper()
        {
            ApiConn().Wait();
        }

        public async Task ApiConn()
        {
            Configuration config = new Configuration();
            client = new OpenTidlClient(ClientConfiguration.Default);
            Logger.Log($"Loggin In.", "API Tests");
            session = await client.LoginWithUsername(config.UserName, config.Password);
        }



        public async Task<PlaylistModel> CreateUserPlay(string title)
        {
            Logger.Log($"Method {nameof(CreateUserPlay)} is triggering", "API Tests");
            var res = await session.CreateUserPlaylist(title);
            return res;
        }



        public async Task<EmptyModel> AddPlaylistTracks(PlaylistObj play)
        {
            Logger.Log($"Method {nameof(AddPlaylistTracks)} is triggering", "API Tests");
            var res = await session.AddPlaylistTracks(play.playlistUuid, play.playlistETag, play.trackIds);
            return res;
        }



        public async Task<string> GetUserPlaylist(string title)
        {
            Logger.Log($"Method {nameof(GetUserPlaylists)} is triggering", "API Tests");
            var res = await session.GetUserPlaylists();
            return res.Items.Where(x => x.Title == title).FirstOrDefault().Title;
        }



        public async Task<JsonList<PlaylistModel>> GetUserPlaylists()
        {
            Logger.Log($"Method {nameof(GetUserPlaylists)} is triggering", "API Tests");
            var res = await session.GetUserPlaylists();
            return res;
        }



        public async Task<PlaylistModel> GetUserPlaylistObj(string title)
        {
            Logger.Log($"Method {nameof(GetUserPlaylists)} is triggering", "API Tests");
            var res = await session.GetUserPlaylists();
            return res.Items.Where(x => x.Title == title).FirstOrDefault();
        }



        public async Task<JsonList<TrackModel>> GetPlaylistTracks(string playUud)
        {
            Logger.Log($"Method {nameof(GetPlaylistTracks)} is triggering", "API Tests");
            var res = await session.GetPlaylistTracks(playUud);
            return res;
        }



        public async Task<JsonList<PlaylistModel>> DeleteAllPlaylists()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Logger.Log($"Method {nameof(GetUserPlaylists)} is triggering", "API Tests");
            var res = await session.GetUserPlaylists();
            res.Items.ToList().ForEach(p => dic.Add(p.Uuid, p.ETag));
            List<string> response = new List<string>();

            dic.ForEach(async e => 
            {
                await session.DeletePlaylist(e.Key, e.Value);
            });

            Logger.Log($"Method {nameof(GetUserPlaylists)} is triggering", "API Tests");
            var result = await session.GetUserPlaylists();
            return result;
        }
    }
}
