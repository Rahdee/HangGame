using System;
using System.Collections.Generic;
using static System.Random;
using System.Text;
using HangmanRenderer.Renderer;


namespace Hangman.Core.Game
{
    // 1) Take a random word from a string array
    //  and store it into a string "WordToFind".

    //  2) Convert the "Word to find" string into a character array.
    //     

    //  3) The player provides the input letter (A, B, C.. etc.)

    //  4) When any input button is pressed, the letter is stored into a
    //  "CurrentInputLetter" string.

    //  5) Make another string "StringToDisplay" to display for
    //     the intermediate status.(For example: If the word is "APPLE",
    //      at some point the "StringToDisplay" could be "A_PL_")

    //   6) Iterate each character in the "WordToFind" string and check each letter
    //      in the "WordToFind" character array to determine whether any letter
    //      matches with "CurrentInputLetter".

    public class HangmanGame
    {

        private GallowsRenderer _renderer;
        private int index;
        private string _underscoreWords;
        private int gameLives = 6;
        private string _guessWord;
        private char[] Overwrite;
        private string newGuess;

        public string ReturnUnderscore()
        {
            return _underscoreWords;
        }

        public HangmanGame()
        {
            // SecretWords (20+)

            string[] SecretWords = { "harry potter", "lord of the rings", "the hobbit", "cat in the hat", "sherlock holmes", "narnia",
                                     "dirty dancing", "troy", "god of war", "age of empires", "assasins creed", "prince of persia", "the witcher",
                                     "rise", "pac man", "donkey kong","pokemon", "mario", "yu gi oh" };

            //This will randomize the secret word list

            index = new Random().Next(SecretWords.Length);
            _guessWord = SecretWords[index];

            //This changes the characters in the string to a dash

            for (int i = 0; i < _guessWord.Length; i++)
            {
                _underscoreWords += "-";
            }
            //Renders the gallows

            _renderer = new GallowsRenderer();

        }
        

        public void TheOverwrittenWord(char letterGuessed)
        {
             //turning the underscoreWords into an array

            Overwrite = _underscoreWords.ToCharArray();

            /*Forloops loops through each charcter of the word, 
             * An " overwrite " character overwrites the underscores in the string*/

            for (int i = 0; i < _guessWord.Length; i++)
            {
                if (_guessWord[i] == letterGuessed)
                {
                    Overwrite[i] = letterGuessed;
                }
            }
            //overwriting the underscores

            _underscoreWords = new string(Overwrite);
        }

        public void Run()
        {
            gameLives = 6;

            bool game = true;

            while(game)
            {
              _renderer.Render(5, 5, gameLives);

                 try
                 {
                        Console.SetCursorPosition(0, 13);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("Your current guess: ");
                        Console.WriteLine(ReturnUnderscore());

                        Console.SetCursorPosition(0, 15);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("What is your next guess: ");
                        newGuess = Console.ReadLine();
                        newGuess = newGuess.ToLower();
                        Console.SetCursorPosition(35, 18);
                        Console.WriteLine("                                                             ");
                    
                        TheOverwrittenWord(newGuess[0]);
                 }
                 catch (IndexOutOfRangeException EX)
                 {
                    Console.SetCursorPosition(35, 18);
                    Console.Write(EX.Message);
                 }

                 if (newGuess.Length > 1)
                 {
                        Console.SetCursorPosition(35, 18);
                        Console.Write("You can't enter more than 1 letter.");
                 }

                 for (int i = 0; i < _guessWord.Length; i++)
                 {
                    if (_guessWord.Contains(newGuess))
                    {

                    }
                    else if (!_guessWord.Contains(newGuess))
                    {
                        gameLives--;

                        break;
                    }

                 }

                Console.SetCursorPosition(0, 17);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ReturnUnderscore());

                if (_underscoreWords == _guessWord)
                {
                    Console.SetCursorPosition(0, 18);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"You Win! The word was {_guessWord}");
                    game = false;
                }
                if (gameLives == 0)
                {
                    _renderer.Render(5, 5, 0);
                    Console.SetCursorPosition(0, 18);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You Lose! The word was {_guessWord}");
                    game = false;

                }
            }   
        }
    }
}
