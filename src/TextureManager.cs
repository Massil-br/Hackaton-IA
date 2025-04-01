using SFML.Graphics;
using System.Collections.Generic;

namespace Shared
{
    public static class TextureManager
    {   

        private static Random rand = new();
        private static Dictionary<string, Texture> textureCache = new();
        
        public  static Dictionary<int, Texture[]> EnemyTextureGroup = new();


        public static Texture[] GetRandomEnemyList(){
            int maxKey = EnemyTextureGroup.Keys.Max() +1;
            int randint = rand.Next(1,maxKey);
            return EnemyTextureGroup.TryGetValue(randint, out var textures) ? textures : Array.Empty<Texture>();
        }


        public static void LoadEnemyTextures(){
             EnemyTextureGroup[1] = [
                 GetTexture("enemy1_1.png"),
                GetTexture("enemy1_2.png"),
                GetTexture("enemy1_3.png")
             ];

       
  

            EnemyTextureGroup[2] = [
                GetTexture("enemy2_1.png"),
                GetTexture("enemy2_2.png")
            ];
    
        }


        public static Texture GetTexture(string path)
        {
            if (!textureCache.ContainsKey(path))
            {
                textureCache[path] = new Texture(path);
            }
            return textureCache[path];
        }

        public static void ClearCache()
        {
            foreach (var texture in textureCache.Values)
            {
                texture.Dispose();
            }
            textureCache.Clear();
        }
    }
} 