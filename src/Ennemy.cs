using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Shared;

namespace src
{
    public class Enemy
    {
        public RectangleShape Shape;
        public float MaxHealth;
        public float CurrentHealth;
        public bool IsAlive;
        public bool IsBoss;
        


        //rendering
        private Sprite sprite;

        private Texture[] animTex;

        private float animationTimer = 0f;
        private float animationSpeed = 0.1f;

        private int currentFrame = 0;

        



        

        private Text EnemyHpUi;


        public Enemy(bool isBoss = false)
        {   
            IsBoss = isBoss;
            IsAlive = true;
            Shape = new RectangleShape(new Vector2f(200, 200));
            Shape.FillColor = IsBoss ? Color.Red : Color.Green;
            Shape.Position = new Vector2f(540, 260);
            EnemyHpUi = new Text("",GameUI.font, 24);
            EnemyHpUi.Position = Shape.Position + new Vector2f(Shape.Size.X /2 -20,-40);
            EnemyHpUi.FillColor = Color.White;


            TextureManager.LoadEnemyTextures();
            if (!IsBoss){
                animTex = TextureManager.GetRandomEnemyList();
            }else{
                animTex = [
                    TextureManager.GetTexture("src/assets/Enemies/"),
                    TextureManager.GetTexture,
                ];
            }
            
            

            sprite = new();

            sprite.Texture = animTex[0];

            float scaleX = 256f / sprite.TextureRect.Width;
            float scaleY = 256f / sprite.TextureRect.Height;
            sprite.Scale = new Vector2f(Math.Abs(scaleX), scaleY);
            sprite.Position = Shape.Position;



            MaxHealth = IsBoss ? 300f * (LevelManager.level *LevelManager.ratio) : 100f  * (LevelManager.level *LevelManager.ratio);
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float dmg)
        {
            CurrentHealth -= dmg;
            if (CurrentHealth <= 0){
                IsAlive = false;
                if (IsBoss){
                    LevelManager.level ++;
                }
            }
        }

        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }

        public void Update(float deltatime){
            if(IsAlive){
                UpdateHp();
                UpdateSpriteTex(deltatime);
            }
            
        }

        private void UpdateHp(){
            EnemyHpUi.DisplayedString = $"{Math.Abs((int)CurrentHealth)} HP";
        }


        private void UpdateSpriteTex(float deltatime){
            if (IsAlive){
                animationTimer += deltatime;
                if (animationTimer >= animationSpeed){
                    animationTimer = 0f;

                    currentFrame = (currentFrame +1 ) % animTex.Length;
                }
                sprite.Texture = animTex[currentFrame];
            }
        }

        public void Draw(RenderWindow window)
        {   
            window.Draw(EnemyHpUi);
            sprite.Position = Shape.Position;
            window.Draw(sprite);
            //window.Draw(Shape);

        }
    }







}

