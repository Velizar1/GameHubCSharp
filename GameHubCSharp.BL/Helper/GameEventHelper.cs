using GameHubCSharp.BL.Services.IServices;

namespace GameHubCSharp.BL.Helper
{
    public static class GameEventHelper
    {
        public static void SetGameViewData(dynamic ViewData, IGameService gameService)
        {
            ViewData["GameNames"] = gameService.FindAllSelectList();

        }
    }
}
