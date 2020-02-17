using Avalonia;
using Avalonia.Media;
using System;

namespace Physics
{
    public class Ball : EllipseGeometry
    {
        public Color Color { get; set; }
        public Vector Center { get; private set; }
        public double Radius { get; private set; }
        public Vector Velocity { get; private set; }
        public bool Updated { get; set; }

        private static double Friction = 0.01;

        public Ball(Vector center, double radius, Vector velocity, Color color) : base()
        {
            Center = center;
            Radius = radius;
            Velocity = velocity;
            Color = color;
            Updated = false;

            ComputeRect();
        }

        public void Gravitate(double frameTimeSeconds, Vector gravity)
        {
            Velocity = Velocity + (gravity * frameTimeSeconds);
        }

        public void ApplyFriction(double frameTimeSeconds)
        {
            Velocity = Velocity - (Velocity * Friction * frameTimeSeconds);
        }

        public void Move(double frameTimeSeconds)
        {
            Center += Velocity * frameTimeSeconds;
            ComputeRect();
        }

        public bool ImpactBounding(double frameTimeSeconds, Rect rect)
        {
            bool collided = false;
            Vector movement = Velocity * frameTimeSeconds;

            if (Rect.X + movement.X < rect.X)
            {
                Center = new Vector(rect.X + Radius, Center.Y);
                Velocity = new Vector(-Velocity.X, Velocity.Y);
                collided = true;
            }
            else if (Rect.X + Rect.Width + movement.X > rect.X + rect.Width)
            {
                Center = new Vector(rect.X + rect.Width - Radius, Center.Y);
                Velocity = new Vector(-Velocity.X, Velocity.Y);
                collided = true;
            }

            if (Rect.Y + movement.Y < rect.Y)
            {
                Center = new Vector(Center.X, rect.Y + Radius);
                Velocity = new Vector(Velocity.X, -Velocity.Y);
                collided = true;
            }
            else if (Rect.Y + Rect.Height + movement.Y > rect.Y + rect.Height)
            {
                Center = new Vector(Center.X, rect.Y + rect.Height - Radius);
                Velocity = new Vector(Velocity.X, -Velocity.Y);
                collided = true;
            }

            if (collided)
            {
                ComputeRect();
            }

            return collided;
        }

        public bool Impact(double frameTimeSeconds, Ball other)
        {
            // Thanks to https://www.gamasutra.com/view/feature/3015/pool_hall_lessons_fast_accurate_.php
            // for the detailed description of how to calculate rigid circle to circle elastic collisions.
            Vector stationary_reference_frame_velocity = (Velocity - other.Velocity) * frameTimeSeconds;
            Vector to_other = other.Center - Center;

            double sum_of_radii = Radius + other.Radius;

            if (stationary_reference_frame_velocity.Length < (to_other.Length - (sum_of_radii)))
            {
                // The velocity between the balls is lower than the distance between them,
                // so we cannot have collided.
                return false;
            }

            Vector velocity_normal = stationary_reference_frame_velocity.Normalize();
            double closest_point_on_velocity_to_other_length = Vector.Dot(velocity_normal, to_other);

            if (closest_point_on_velocity_to_other_length < 0)
            {
                // The other ball is behind this ball, relative to its velocity,
                // so we cannot have collided.
                return false;
            }

            double of_from_other_to_velocity_length_squared =
                to_other.SquaredLength -
                Mthx.Square(closest_point_on_velocity_to_other_length);

            double sum_of_radii_squared = Mthx.Square(sum_of_radii);

            if (of_from_other_to_velocity_length_squared >= sum_of_radii_squared)
            {
                // The distance from the other ball to the relative velocity line between these balls is
                // larger than the radius of both balls, so we cannot have collided.
                return false;
            }

            double of_from_closest_point_on_velocity_to_impact_length_squared =
                sum_of_radii_squared - of_from_other_to_velocity_length_squared;

            if (of_from_closest_point_on_velocity_to_impact_length_squared < 0)
            {
                // There is no triangle that describes the distance from the closest point on the
                // relative velocity vector to the point of impact, so we cannot have collided.
                return false;
            }

            double to_impact_length = closest_point_on_velocity_to_other_length -
                Math.Sqrt(of_from_closest_point_on_velocity_to_impact_length_squared);

            if (to_impact_length > stationary_reference_frame_velocity.Length)
            {
                // The impact is further away that we will move right now,
                // so we haven't collided yet.
                return false;
            }

            // We've impacted. Move both balls to the point of the impact.
            double proportional_time_of_impact =
                stationary_reference_frame_velocity.Length / ((Velocity.Length + other.Velocity.Length) * frameTimeSeconds);

            Vector to_timed_impact = Velocity.Normalize() * to_impact_length * proportional_time_of_impact * frameTimeSeconds;
            Vector other_to_timed_impact = other.Velocity.Normalize() * to_impact_length * proportional_time_of_impact * frameTimeSeconds;

            Center += to_timed_impact;
            other.Center += other_to_timed_impact;

            ComputeRect();
            other.ComputeRect();

            // Calculate an elastic collision that simulates the balls as perfect rigid bodies.
            Vector impact_normal = to_other.Normalize();

            double impact_magnitude_one = Vector.Dot(Velocity, impact_normal);
            double impact_magnitude_two = Vector.Dot(other.Velocity, impact_normal);
            double impact_magnitude = (2.0 * (impact_magnitude_one - impact_magnitude_two)) / sum_of_radii;

            Velocity = Velocity - impact_normal * impact_magnitude * other.Radius;
            other.Velocity = other.Velocity + impact_normal * impact_magnitude * Radius;

            return true;
        }

        private void ComputeRect()
        {
            Rect = new Rect(
                Center.X - Radius,
                Center.Y - Radius,
                Radius * 2,
                Radius * 2
            );
        }
    }
}
