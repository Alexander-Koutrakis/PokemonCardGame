using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

//Load Texture from URL
//Create a Sprite from Texture
public static class ImageLoader 
{
    private static async Task<Sprite> GetSpriteAsync(string ImageURL)
    {
        Sprite sprite = null;

        while (sprite == null)
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(ImageURL);
            webRequest.SendWebRequest();
            while (!webRequest.isDone)
            {
                await Task.Yield();
            }

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError(ImageURL + ": ConnectionError Error: " + webRequest.error);
                    break ;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(ImageURL + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(ImageURL + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    sprite = WebSprite(webRequest);
                    break;
            }
        }
        return sprite; 
    }

    public static async Task SetSpriteAsync(string ImageURL,PokemonCardData pokemonCardData)
    {
        Sprite sprite = await GetSpriteAsync(ImageURL);
        pokemonCardData.SetSprite(sprite);
    }

    private static Sprite WebSprite(UnityWebRequest webRequest)
    {
        Texture2D texture = DownloadHandlerTexture.GetContent(webRequest) as Texture2D;
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        return Sprite.Create(texture, rect, pivot, 100);
    }


}
