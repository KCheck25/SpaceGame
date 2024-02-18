using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostSpace.Things
{
    internal class Rocket
    {
        Vector2 velocity;
        Vector2 position;
        float angle;
        float mass;

        Texture2D texture;

        public Rocket(Texture2D texture, float mass)
        {
            this.texture = texture;
            this.mass = mass;
            this.angle = 0f;
            velocity = new Vector2();
            position = new Vector2();
        }

        Vector2 computeForce()
        {
            float massPlanet = 4E13f;
            float xDist = (position.X - 1920 / 2);
            float yDist = -(position.Y - 1080 / 2);
            float distToCenter = (float)System.Math.Pow(System.Math.Pow(xDist, 2) + System.Math.Pow(yDist, 2), 0.5);
            //Debug.WriteLine("Distance:" + xDist);

            if (distToCenter < 10)
            {
                return new Vector2(0, 0);
            }

            float universalGravity = 6.67E-11f;
            float totalForce = (universalGravity * massPlanet * mass) / (distToCenter * distToCenter);
            float angle = (float)System.Math.Atan2(yDist, xDist);

            //if (yDist < 0 && xDist < 0 || yDist > 0 && xDist < 0)
            //{
            //    angle += MathHelper.Pi;
            //}

            float xForce = -totalForce * (float)System.Math.Cos(angle);
            float yForce = totalForce * (float)System.Math.Sin(angle);

            return new Vector2(xForce, yForce);
        }

        public void Update(double frameTime)
        {
            Vector2 forces = computeForce();
            velocity.X += forces.X / mass;
            velocity.Y += forces.Y / mass;

            velocity.X = 500;

            position.X += velocity.X * (float)frameTime;
            position.Y += velocity.Y * (float)frameTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, null, Color.White, angle, new Vector2(texture.Width / 2, texture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

    }
}
