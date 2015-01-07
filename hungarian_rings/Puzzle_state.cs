/* Author: Timothy Jacob Schlueter
 * Created: September 4, 2014
* Modified: January 7, 2015
 * 
 * Program purpose START
 * This program was originally designed to implement an Iterative Deepening A* (IDA*) algorithm as a method to solve
 * a randomized representation of a Hungarian Rings puzzle.  This was an assignment from the fall 2014 semester
 * at the University of Kentucky for the course CS463 <Artificial Intelligence>.
 * This is an updated version of the program turned in for the assignment.  Intended changes include a modification
 * of the UI components used to launch the IDA* algorithm, as re-implementation of UI components that allow
 * direct changes to the puzzle, and some other features which are intended to begin a transition from a basic
 * search-based AI program to a working puzzle game.  IDA* implementation will remain intact, though it may be
 * re-purposed as a "bonus feature" in the event that this program should ever be launched as a playable game.
  * 
 * Known Limitations: The IDA* algorithm is useful in that it will find any existing solution to the puzzle at
 * a theoretically minimal number of moves.  However, this accuracy comes at the expense of very long search times
 * for as little as 15 moves taken to randomize the puzzle.  These long search times are the result of quickly
 * expanding numbers of nodes that must be analyzed 4 * 3^(level-1) per level to be precise.
 * Program purpose END
 * 
 * Puzzle_state describes a given state of the Hungarian Rings puzzle created in this program.
 * An great number of these objects are created by the A* search algorithm in order to find a solution to a
 * Puzzle_state object that has been randomized by a series of moves, according to this class's Randomize method.
 * 
 * Methods:
 * Spin_right_clockwise - rotate the balls in the right-hand ring one step clockwise.
 * Spin_right_counterclockwise - rotate the balls in the right-hand ring one step counterclockwise.
 * Spin_left_clockwise - rotate the balls in the left-hand ring one step clockwise.
 * Spin_left_counterclockwise - rotate the balls in the left-hand ring one step counterclockwise.
 * Randomize - use a random series of Spin_* methods to mix up the location of hr_node objects representing
 *      the balls of a Hungarian Rings puzzle into a solvable state.  True random placement of the balls could
 *      create an unsolvable state, so that is not an acceptable randomization method for this puzzle.
 *      Parameters: int number_of_moves - how many moves should be taken to randomize the puzzle
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hungarian_rings
{
    /// <summary>
    /// Object type that describes a current state in the Hungarian Rings puzzle.
    /// Provides the underlying structure for creating, drawing, and manipulating the puzzle.
    /// </summary>
    public class Puzzle_state
    {
        /// <summary>
        /// Array upon which the puzzle is constructed.  It is used to re-arrange the puzzle by
        /// changing the color of the balls in specified locations.  The hr_node objects also
        /// contain integer variables indicating their relative location within the puzzle.
        /// </summary>
        hr_node[] Ball_list;

        /// <summary>
        /// Records the previous move action taken to reach the current puzzle state.
        /// </summary>
        int last_move;

        /// <summary>
        /// Constructor 1
        /// </summary>
        public Puzzle_state()
        {
            last_move = -1;

            // Creates the array of hr_nodes
            Ball_list = new hr_node[38];

            //loop to define the members of the left-hand circle
            for (int i = 0; i < 20; i++)
            {
                //Set up the hr_node array, Ball_list for balls 0-19
                if (i == 0)
                { Ball_list[i] = new hr_node(3, 20, 37, 1, 19); }
                else if (i == 5)
                { Ball_list[i] = new hr_node(1, 34, 33, 6, 4); }
                else if (i < 11)
                { Ball_list[i] = new hr_node(1, i + 1, i - 1, 2); }
                else
                { Ball_list[i] = new hr_node(2, i + 1, i - 1, 2); }
            }

            //loop to define the members of the right-hand circle
            for (int i = 0; i < 18; i++)
            {
                //Set up the remainder of Ball_list (20-37)
                if (i == 0)
                { Ball_list[20 + i] = new hr_node(3, 21, 0, 1); }
                else if (i == 17)
                { Ball_list[20 + i] = new hr_node(3, 0, 19 + i, 1); } //ball 38
                else if (i == 13)
                { Ball_list[20 + i] = new hr_node(4, 5, 19 + i, 1); } //ball 34
                else if (i == 14)
                { Ball_list[20 + i] = new hr_node(3, 21 + i, 5, 1); } //ball 35
                else if (i < 4 || i > 13)
                { Ball_list[20 + i] = new hr_node(3, 21 + i, 19 + i, 1); }
                else
                { Ball_list[20 + i] = new hr_node(4, 21 + i, 19 + i, 1); }
            }
        }

        /// <summary>
        /// Constructor to create a new Puzzle_state object from an array of hr_node objects and an integer
        /// indicating the last move taken, which resulted in this object's creation.
        /// 
        /// TJS: NOTE: no references to this method found, consider for removal.
        /// </summary>
        /// <param name="in_list">
        /// Ball_List taken from a parent Puzzle_State
        /// </param>
        /// <param name="move_in">
        /// An integer indicating the previous move taken by the A* algorithm, which resulted in the creation of
        /// a new object.  Tracking of previous moves is necessary to avoid infinitelydeep search trees.
        /// </param>
        public Puzzle_state(hr_node[] in_list, int move_in)
        {
            last_move = move_in;

            Ball_list = new hr_node[38];
            Ball_list = (hr_node[])in_list.Clone();
        }

        /// <summary>
        /// Constructor to create an exact, deep copy of an existing Puzzle_state object.
        /// </summary>
        /// <param name="node">
        /// Puzzle_state object to be copied.
        /// </param>
        public Puzzle_state(Puzzle_state node)
        {
            last_move = node.get_last_move();

            hr_node[] in_list = new hr_node[38];
            in_list = node.get_Ball_List();
            Ball_list = new hr_node[38];
            for (int i = 0; i < in_list.Length; i++)
            {
                /*hr_node t = new hr_node(in_list[i].get_clr(), in_list[i].get_nnr(), in_list[i].get_pnr(),
                    in_list[i].get_nnl(), in_list[i].get_pnl());/**/
                hr_node t = new hr_node(in_list[i]);/**/
                Ball_list[i] = t;
            }
        }

        /// <summary>
        /// Spin balls in the right-hand circle clockwise.
        /// </summary>
        public void Spin_right_clockwise()
        {
            hr_node temp = new hr_node(Ball_list[0].get_clr(), 0, 0, 1);
            Ball_list[0].set_clr(Ball_list[20].get_clr());

            hr_node temp2 = new hr_node(Ball_list[5].get_clr(), 0, 0, 1);
            Ball_list[5].set_clr(Ball_list[34].get_clr());

            for (int i = 20; i < 38; i++)
            {
                if (i == 33)
                    Ball_list[i].set_clr(temp2.get_clr());
                else if (i == 37)
                    Ball_list[i].set_clr(temp.get_clr());
                else
                    Ball_list[i].set_clr(Ball_list[Ball_list[i].get_nnr()].get_clr());
            }
        }

        /// <summary>
        /// Spin balls in the right-hand circle counter-clockwise
        /// </summary>
        public void Spin_right_counterclockwise()
        {
            hr_node temp = new hr_node(Ball_list[0].get_clr(), 0, 0, 1);
            Ball_list[0].set_clr(Ball_list[37].get_clr());

            hr_node temp2 = new hr_node(Ball_list[5].get_clr(), 0, 0, 1);
            Ball_list[5].set_clr(Ball_list[33].get_clr());

            for (int i = 37; i > 19; i--)
            {
                int k = 0;
                if (i == 34)
                    Ball_list[i].set_clr(temp2.get_clr());
                else if (i == 20)
                    Ball_list[i].set_clr(temp.get_clr());
                else
                    Ball_list[i].set_clr(Ball_list[Ball_list[i].get_pnr()].get_clr());
            }
        }

        /// <summary>
        /// Spin balls in the left-hand circle clockwise
        /// </summary>
        public void Spin_left_clockwise()
        {
            hr_node temp = new hr_node(Ball_list[0].get_clr(), 0, 0, 1);

            for (int i = 0; i < 20; i++)
            {
                if (i == 19)
                    Ball_list[i].set_clr(temp.get_clr());
                else
                    Ball_list[i].set_clr(Ball_list[Ball_list[i].get_nnl()].get_clr());
            }
        }

        /// <summary>
        /// Spin balls in the left-hand circle counter-clockwise.
        /// </summary>
        public void Spin_left_counterclockwise()
        {
            hr_node temp = new hr_node(Ball_list[19].get_clr(), 0, 0, 1);

            for (int i = 19; i > -1; i--)
            {
                if (i == 0)
                    Ball_list[i].set_clr(temp.get_clr());
                else
                    Ball_list[i].set_clr(Ball_list[Ball_list[i].get_pnl()].get_clr());
            }
        }

        /// <summary>
        /// Randomizes the distribution of the balls in the puzzle as described below.
        /// 
        /// ***********************************************
        /// 
        /// Call a number of Spin_* methods a number of times equal to the current number in
        /// the NumericUpDown object Move_number.
        /// It can be a wide variety of combinations, but some restrictions will be applied:
        ///     The same action cannot be taken 10 times in a row.
        ///     An action directly opposite to the previous action cannot be taken.
        ///     The total number of actions cannot exceed 1000 (subject to change).
        /// </summary>
        /// <param name="number_of_moves">
        /// The number of moves to be taken during randomization.
        /// </param>
        public void Randomize(int number_of_moves)
        {
            Random rand1 = new Random();

            int[] last_nine_moves = new int[9];
            for (int i = 0; i < 9; i++) { last_nine_moves[i] = -1; }

            /* Compare the current move selection with the previous nine to
             * ensure that nine identical actions are not taken and that the
             * current move is not the opposite of the previous move.
             * This ensures that the puzzle will not accidentally be solved by the randomizer
             * 
             ***************************************************************
             *
             * Integer variable test has four standard values and one error value
             * that can be assigned to it, each indicating a different response action:
             *  0: Spin the left circle clockwise
             *  1: Spin the right circle clockwise
             *  2: Spin the left circle counter-clockwise
             *  3: Spin the right circle counter-clockwise
             *  -1: invalid error value; the response is to take no action, as there is no
             *      apprpriate move associated with this number.
             *  OTHER: no other values should be assigned to test, but if they are, no action will be taken.
             */
            for (int i = 0; i < number_of_moves; i++)
            {
                int test = -1;
                bool ten_sames = true;
                bool undo = true;
                // Generates potential moves until one is found such that there are not ten consecutive moves of the
                // same type, and which does not undo the previous move taken.
                while (ten_sames || undo)
                {
                    undo = false;
                    test = rand1.Next(4);

                    //Check to ensure the previous nine moves are not the same as the currently proposed one
                    for (int j = 0; j < 9; j++)
                    {
                        if (test != last_nine_moves[j])
                            ten_sames = false;
                    }

                    //Check to ensure the proposed move is not undoing the previous one
                    switch (last_nine_moves[0])
                    {
                        case 0:
                            if (test == 2) { undo = true; } break;
                        case 1:
                            if (test == 3) { undo = true; } break;
                        case 2:
                            if (test == 0) { undo = true; } break;
                        case 3:
                            if (test == 1) { undo = true; } break;
                    }
                }

                //refresh the list of the previous nine moves, adding the newest and pitching the oldest
                for (int j = 8; j > 0; j--)
                {
                    last_nine_moves[j] = last_nine_moves[j - 1];
                }
                last_nine_moves[0] = test;

                // Call the Spin_* method associated with test
                if (test == 0)
                {
                    Spin_left_clockwise();
                }
                else if (test == 1)
                {
                    Spin_right_clockwise();
                }
                else if (test == 2)
                {
                    Spin_left_counterclockwise();
                }
                else if (test == 3)
                {
                    Spin_right_counterclockwise();
                }
            }
        }

        /// <summary>
        /// Gets the hr_node array Ball_List.
        /// </summary>
        /// <returns> The reference to the Ball_List array.
        /// </returns>
        public hr_node[] get_Ball_List()
        {
            return Ball_list;
        }

        /// <summary>
        /// Gets the value of last_move.
        /// </summary>
        /// <returns> Returns the value of last_move.
        /// </returns>
        public int get_last_move()
        {
            return last_move;
        }

        /// <summary>
        /// Sets the value of last_move to the value of the input variable.
        /// </summary>
        /// <param name="move_in"> Integer representing the last move taken on the array.
        /// </param>
        public void set_last_move(int move_in)
        {
            last_move = move_in;
        }
    }
}
