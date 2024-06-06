using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AiFirst
{
    internal abstract class State
    {
        internal abstract void LoadContent(ContentManager content);
        internal abstract void Update(GameTime gameTime);
        internal abstract void Draw(SpriteBatch spriteBatch);
    }
}
