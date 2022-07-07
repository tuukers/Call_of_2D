using System;
using System.Numerics;
using System.Collections.Generic;
using System.Data;
using Callof2d.Game.Casting;
using Callof2d.Game.Services;


namespace Callof2d.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleBulletCollisionsAction : Action
    {

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        private ContactService contactService;
        bool collision = false;
        public HandleBulletCollisionsAction(ContactService contactService)
        {
            this.contactService = contactService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            Actor player = cast.GetFirstActor("player");
            List<Actor> zombies = cast.GetActors("zombie");
            List<Actor> bullets = cast.GetActors("bullets");
            List<Actor> walls = cast.GetActors("wall");
            
            foreach(Zombie zombie in zombies)
            {
                Vector2 zombiePosition=zombie.GetPosition();
                float zombiePosition_X=zombiePosition.X;
                float zombiePosition_Y=zombiePosition.Y;

                foreach(Actor bullet in bullets)
                {
                    Vector2 bulletPosition=bullet.GetPosition();
                    Bullet shot = (Bullet)bullet;
                    float bulletPosition_X=bulletPosition.X;
                    float bulletPosition_Y=bulletPosition.Y;

                    float x_difference= zombiePosition_X-bulletPosition_X;
                    float y_difference= zombiePosition_Y-bulletPosition_Y;
                    float x_difference_abs=Math.Abs(x_difference);
                    float y_difference_abs=Math.Abs(y_difference);

                    if(x_difference_abs<10 && y_difference_abs<10)
                    {
                        cast.RemoveActor("bullets",bullet);
                        zombie.TakeDamage(shot.GetBulletDamage());
                        if (zombie.GetHealth()<=0){
                            cast.RemoveActor("zombie",zombie);
                        }
                    }

                    foreach (Actor wallT in walls)
                    {
                        Wall wall= (Wall)wallT;
                        collision=contactService.WallCollision(bullet,wall);
                        if(collision)
                        {
                            cast.RemoveActor("bullets",bullet);
                        }
                    }
                }
            }

        }

        /// <summary>

        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>

        // private void Growth(Cast cast)
        // {
        //     Player player1 = (Player)cast.GetFirstActor("player1");
        //     Player player2 = (Player)cast.GetFirstActor("player2");
        //     //Score score = (Score)cast.GetFirstActor("score");
        //     //Food food = (Food)cast.GetFirstActor("food");
            
            
            
        //     int points = 1;
        //     player1.GrowTail(points,1);
        //     player2.GrowTail(points,2);
        //     //score.AddPoints(points);
        //     //food.Reset();
            
        // }

        // /// <summary>

        // /// Sets the game over flag if the snake collides with one of its segments.
        // /// </summary>
        // /// <param name="cast">The cast of actors.</param>
        // private void HandleSegmentCollisions(Cast cast)
        // {
        //     Player player1 = (Player)cast.GetFirstActor("player1");
        //     Player player2 = (Player)cast.GetFirstActor("player2");
        //     Actor head1 = player1.GetHead();
        //     Actor head2 = player2.GetHead();
        //     List<Actor> body1 = player1.GetBody();
        //     List<Actor> body2 = player2.GetBody();

        //     int x = Constants.MAX_X / 2;
        //     int y = Constants.MAX_Y / 2;
        //     Point position = new Point(x, y);
            
            
            
        //     if (isGameOver == false)
        //     {
        //         foreach (Actor segment in body1)
        //         {
        //             if (segment.GetPosition().Equals(head1.GetPosition()))
        //             {
        //                 isGameOver = true;
                        
        //                 player2Win = true;
        //             }
        //             if (segment.GetPosition().Equals(head2.GetPosition()))
        //             {
        //                 isGameOver = true;

        //                 player1Win = true;
        //             }
        //         }
        //         foreach (Actor segment in body2)
        //         {
        //             if (segment.GetPosition().Equals(head1.GetPosition()))
        //             {
        //                 isGameOver = true;
        //                 player2Win = true;
        //             }
        //             if (segment.GetPosition().Equals(head2.GetPosition()))
        //             {
        //                 isGameOver = true;
        //                 player1Win = true;
            
        //             }
        //         }
        //     }
            
        // }

        // private void HandleGameOver(Cast cast)
        // {
        //     if (isGameOver == true)
        //     {
        //         Player player2 = (Player)cast.GetFirstActor("player2");
        //         Player player1 = (Player)cast.GetFirstActor("player1");
        //         List<Actor> segments1 = player1.GetSegments();
        //         List<Actor> segments2 = player2.GetSegments();

        //         // create a "game over" message
        //         int x = Constants.MAX_X / 2- 3*Constants.CELL_SIZE;
        //         int y = Constants.MAX_Y / 2;
        //         Point position = new Point(x, y);

        //         // Actor message = new Actor();
        //         // message.SetText("Game Over!");
        //         // message.SetPosition(position);
        //         // cast.AddActor("messages", message);

        //         if (player1Win==true)
        //         {
        //             if (player2Win== true)
        //             {
        //                 Actor message = new Actor();
        //                 message.SetText("Tie Game!!!");
        //                 message.SetPosition(position);
        //                 message.SetColor(Constants.GREEN);
        //                 cast.AddActor("messages", message);
        //                 // Turn both players white
        //                 foreach (Actor segment in segments1)
        //                 {
        //                     segment.SetColor(Constants.WHITE);
        //                 }
        //                 foreach (Actor segment in segments2)
        //                 {
        //                     segment.SetColor(Constants.WHITE);
        //                 }
        //             }
        //             else
        //             {
        //                 Actor message = new Actor();
        //                 message.SetText("Player 1 Wins!!");
        //                 message.SetPosition(position);
        //                 message.SetColor(player1.GetPlayerColor(1));
        //                 cast.AddActor("messages", message);
        //                 // Make player 2 turn white
        //                 foreach (Actor segment in segments2)
        //                 {
        //                     segment.SetColor(Constants.WHITE);
        //                 }
        //             }
        //         }
        //         else if(player2Win == true)
        //         {
        //             Actor message = new Actor();
        //             message.SetText("Player 2 Wins!!");
        //             message.SetPosition(position);
        //             message.SetColor(player2.GetPlayerColor(2));
        //             cast.AddActor("messages", message);
        //             // Make player 1 turn white
        //             foreach (Actor segment in segments1)
        //             {
        //                 segment.SetColor(Constants.WHITE);
        //             }
        //         }
        //         else
        //         {
        //             Actor message = new Actor();
        //             message.SetText("Error no winner");
        //             message.SetPosition(position);
        //             cast.AddActor("messages", message);
        //         }
        //     }
        // }

    }
}