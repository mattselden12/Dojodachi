using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dojodachi.Models
{
    public class Dachi
    {
        public int Fullness {get;set;}
        public int Happiness {get;set;}
        public int Meals {get;set;}
        public int Energy {get;set;}

        public Dachi()
        {
            // Fullness=20;
            // Happiness=20;
            // Meals=3;
            // Energy=50;
        }

        public Dachi(int full, int happy, int meal, int energy)
        {
            Fullness = full;
            Happiness = happy;
            Meals = meal;
            Energy = energy;
        }

        public string Feed(){
            Random rand = new Random();
            int likenum = rand.Next(1,101);
            if(Meals>0)
            {
                Meals -= 1;
                if(likenum<=75)
                {
                    int fullincrease = rand.Next(5,11);
                    Fullness += fullincrease;
                    return("You fed your Dojodachi! Fullness +"+fullincrease+", Meals -1");
                }
                else
                {
                    return("Your Dojodachi didn't like eating! Meals -1");
                }
            }
            else
            {
                return("You need meals to feed your Dojodachi");
            }
        }

        public string Play(){
            Random rand = new Random();
            int likenum = rand.Next(1,101);
            if(Energy>=5)
            {
                Energy -= 5;
                if(likenum<=75)
                {
                    int happyincrease = rand.Next(5,11);
                    Happiness += happyincrease;
                    return("You played with your Dojodachi! Happiness +"+happyincrease+", Energy -5");
                }
                else
                {
                    return("Your Dojodachi didn't like playing! Energy -5");
                }
            }
            else
            {
                return("Your Dojodachi doesn't have enough energy to play");
            }
        }

        public string Work()
        {
            if(Energy>=5)
            {
                Energy -= 5;
                Random rand = new Random();
                Meals += rand.Next(1,4);
                return("Your Dojodachi worked. Meals +"+Meals+", Energy -5");
            }
            else
            {
                return("Your Dojodachi doesn't have enough energy to play");
            }
        }

        public string Sleep()
        {
            Energy += 15;
            Fullness -= 5;
            Happiness -= 5;
            return("Your Dojodachi slept. Energy +15, Fullness -5, Happiness -5");
        }
    }
}