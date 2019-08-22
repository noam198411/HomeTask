using BusinessLogic.ObjectModels;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace TestSection
{
    //Please note in the the tests below I used all Method' which were requested in the task.
    //The logs are created on the desktop of your local machine under "Logs" folder.

    [TestFixture]
    public class VerifyPlaylistCreation_Test
    {
        [Test]
        public async Task VerifyPlaylistCreation() //This test Creates playlist and Gets user playlist.
        {
            Logger.Log($"{Environment.NewLine} Test {nameof(VerifyPlaylistCreation)} is starting", "API Tests");
            string playlistExpected ="Playlist "+ ExtensionCommonFunc.GetRandomNumber();
            var apiTidl = new APIHelper();
            var playlistActual = await apiTidl.CreateUserPlay(playlistExpected);
            Assert.AreEqual(playlistExpected, playlistActual.Title, "Playlist was not created.");
            Logger.Log($"Getting User PlayList", "API Tests");
            var myNewPlayList = await apiTidl.GetUserPlaylist(playlistActual.Title);
            Assert.AreEqual(myNewPlayList, playlistActual.Title, "Expected playlist was not returned.");
            Logger.Log($"Test {nameof(VerifyPlaylistCreation)} Finished", "API Tests");
        }





        [TestFixture]
        public class AddTracksToPlayList_Test
        {
            [Test]
            public async Task AddTracksToPlayList() // This test create playlist, adding track to the list, getting the tracks
            {
                string playlistExpected = "Playlist " + ExtensionCommonFunc.GetRandomNumber();
                Logger.Log($"{Environment.NewLine}Test {nameof(AddTracksToPlayList)} is starting", "API Tests");
                var apiTidl = new APIHelper();
                var playlistActual = await apiTidl.CreateUserPlay(playlistExpected);
                var playlistModel = await apiTidl.GetUserPlaylistObj(playlistExpected);

                var playlistObj = new PlaylistObj()
                {
                    playlistUuid = playlistModel.Uuid,
                    playlistETag = "*",
                    trackIds = new int[] { 115839864 }
                };

                var res = await apiTidl.AddPlaylistTracks(playlistObj);
                var ress = await apiTidl.GetPlaylistTracks(playlistModel.Uuid);
                Assert.AreEqual(playlistObj.trackIds.FirstOrDefault().ToString(), ress.Items[0].Id.ToString());

                Logger.Log($"Test {nameof(AddTracksToPlayList)} Finished", "API Tests");
            }
        }

        [TestFixture]
        public class GetUserPlaylists_Test
        {
            [Test]
            public async Task VerifyPlaylistDelete() //This test delete all playlists than creating one and verifying that it created.
            {
                Logger.Log($"{Environment.NewLine}Test {nameof(VerifyPlaylistDelete)} is starting", "API Tests");

                var apiTidl = new APIHelper();
                var res = await apiTidl.DeleteAllPlaylists();
                Assert.AreEqual(0, res.TotalNumberOfItems,"Not all playlists were deleted.");

                string playlistExpected = "Playlist " + ExtensionCommonFunc.GetRandomNumber();
                var playlistActual = await apiTidl.CreateUserPlay(playlistExpected);
                var allplaylists = await apiTidl.GetUserPlaylists();
                Assert.AreEqual(1, allplaylists.Items.Count(),"Suppose to be only one user but actualy it's not.");
                Logger.Log($"Test {nameof(VerifyPlaylistDelete)} Finished", "API Tests");
            }
        }
       
    }
}