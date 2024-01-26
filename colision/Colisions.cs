// using System.Collections.Generic;

// public class Collisions
// {
//     private static Collisions current;
//     public static Collisions Current => current;
//     public List<Collidable> Collidables { get; private set; }

//     private Collisions()
//     {
//         Collidables = new List<Collidable>();
//         current = this;
//     }

//     public void AddCollidable(Collidable collidable)
//     => Collidables.Add(collidable);

//     public void RemoveCollidable(Collidable collidable)
//         => Collidables.Remove(collidable);
//      public bool CheckCollisions(Collidable obj)
//     {
//         for (int j = 0; j < Collidables.Count; j++)
//         {
//             Collidable other = Collidables[j];

//             if (other == obj)
//                 continue;

//             if (CollisionDetected(obj, other) )//&& other.isHittable
//                 return true;
//         }
//         return false;
//     }
//         private bool CollisionDetected(Collidable obj1, Collidable obj2) =>
//         obj2.Hitbox.IntersectsWith(obj1.Hitbox);
    
//      public static void New() => current = new Collisions();
// }