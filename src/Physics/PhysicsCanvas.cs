using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Rendering;
using System;
using System.Collections.Generic;

namespace Physics
{
    public class PhysicsCanvas : Canvas, IRenderLoopTask
    {
        private List<Ball> Balls { get; set; } = new List<Ball>();
        public bool NeedsUpdate { get; private set; } = true;
        private TimeSpan? LastUpdated { get; set; } = null;
        private static Vector Gravity = new Vector(0, 9.81);
        private const double timeFactor = 5;

        public void AddBall(Ball ball)
        {
            Balls.Add(ball);
            InvalidateVisual();
        }

        public void Clear()
        {
            Balls.Clear();
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            foreach (Ball ball in Balls)
            {
                context.DrawGeometry(new SolidColorBrush(ball.Color), null, ball);
            }
        }

        public void Render()
        {
            InvalidateVisual();
        }

        public void Update(TimeSpan time)
        {
            double frameTimeSeconds = (LastUpdated == null ? 0 : (time - LastUpdated)?.TotalSeconds ?? 0) * timeFactor;
            LastUpdated = time;

            for (int i = 0; i < Balls.Count; ++i)
            {
                Balls[i].Gravitate(frameTimeSeconds, Gravity);
                Balls[i].Updated = Balls[i].ImpactBounding(frameTimeSeconds, Bounds);

                for (int other = i + 1; other < Balls.Count; ++other)
                {
                    Balls[i].Updated = Balls[i].Impact(frameTimeSeconds, Balls[other]) | Balls[i].Updated;
                }

                if (!Balls[i].Updated)
                {
                    Balls[i].Move(frameTimeSeconds);
                }

                Balls[i].ApplyFriction(frameTimeSeconds);
            }
        }
    }
}
