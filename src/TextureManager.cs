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
                 GetTexture("src/assets/Enemies/poubelle0.png"),
                GetTexture("src/assets/Enemies/poubelle1.png"),
                GetTexture("src/assets/Enemies/poubelle2.png"),
                GetTexture("src/assets/Enemies/poubelle3.png"),
                GetTexture("src/assets/Enemies/poubelle4.png"),
                GetTexture("src/assets/Enemies/poubelle5.png"),
                GetTexture("src/assets/Enemies/poubelle6.png"),
                GetTexture("src/assets/Enemies/poubelle7.png"),
                GetTexture("src/assets/Enemies/poubelle8.png"),
                GetTexture("src/assets/Enemies/poubelle9.png")
             ];

       
  

            EnemyTextureGroup[2] = [
                GetTexture("src/assets/Enemies/poub0.png"),
                GetTexture("src/assets/Enemies/poub1.png"),
            ];

            EnemyTextureGroup[3]=[
                GetTexture("src/assets/Enemies/rat0.png"),
                GetTexture("src/assets/Enemies/rat1.png")
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