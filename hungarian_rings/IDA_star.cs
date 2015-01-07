/* Author: Timothy Jacob Schlueter
 * Created: September 2014
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
 * Class to provide the overhead for and operate a Iterative Deepening A* algorithm to find
 * a solution to any given randomization of the Hunarian Rings puzzle.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace hungarian_rings
{
    /// <summary>
    /// Class to provide the overhead for and operate a Iterative Deepening A* algorithm to find
    /// a solution to any given randomization of the Hunarian Rings puzzle.
    /// </summary>
    class IDA_star
    {
        Puzzle_state main_root;
        bool main_set;
        long nodes_expanded;
        System.Diagnostics.Stopwatch keep_time;

        /// <summary>
        /// Constructor
        /// </summary>
        public IDA_star()
        {
            keep_time = new System.Diagnostics.Stopwatch();
            main_root = new Puzzle_state();
            main_set = false;
            nodes_expanded = 0;
        }

        /// <summary>
        /// Runs the IDA* Algorithm
        /// </summary>
        /// <param name="root"> Puzzle State object that represents the initial state of the
        /// Hungarian Rings puzzle.
        /// </param>
        /// <returns> int t such that t is the number of steps taken to reach a solution
        /// </returns>
        public int run(Puzzle_state root)
        {
            main_root = root;
            int bound = Expected_Steps(root);
            int t = 0;


            keep_time.Start();
            while (t >= 0)
            {
                nodes_expanded = 0;
                t = Search(root, 0, bound);

                // The below are potential return conditions
                if (keep_time.ElapsedMilliseconds > 1000000)
                {
                    keep_time.Stop();
                    return -1;
                }
                if (t == int.MaxValue)
                {
                    // No solution found return condition
                    keep_time.Stop();
                    return -1;
                }
                if (t < 0)
                {
                    keep_time.Stop();
                    long time_check = keep_time.ElapsedMilliseconds;
                    t *= -1;
                    // Solution found in t steps
                    return t;
                }
                bound = t;
            }
            //This is a default return condition; should be unreachable
            //Check for errors if this value is ever returned
            return -2;
        }

        /// <summary>
        /// Caclculate how many color segments are currently present within the puzzle.
        /// In a solved puzzle, this method should indicate that there are 4 such segments.
        /// Any location of the four segments is acceptable, so long as they are genuinely
        /// continuous color segments.
        /// </summary>
        /// <param name="node"> The state of the Hungarian Rings puzzle for which we want to
        /// know the number of color segments.
        /// </param>
        /// <returns> An integer equal to the number of color segments in the puzzle
        /// </returns>
        private int Check(Puzzle_state node)
        {
            int count = 0;
            hr_node[] Ball_List = node.get_Ball_List();
            for (int i = 0; i < 20; i++)
            {
                if (i == 0 || i == 5)
                {
                    if (Ball_List[i].get_clr() != Ball_List[Ball_List[i].get_pnl()].get_clr() &&
                        ((Ball_List[i].get_clr() != Ball_List[Ball_List[i].get_nnr()].get_clr() &&
                        Ball_List[i].get_clr() != Ball_List[Ball_List[i].get_pnr()].get_clr()) ||
                        Ball_List[i].get_clr() == Ball_List[Ball_List[i].get_nnl()].get_clr()))
                    {
                        count++;
                    }
                }
                else
                {
                    if (Ball_List[i].get_clr() != Ball_List[Ball_List[i].get_pnl()].get_clr())
                    {
                        count++;
                    }
                }
            }
            for (int i = 0; i < 20; i++)
            {
                if (i == 0)
                {
                    if (Ball_List[0].get_clr() != Ball_List[Ball_List[0].get_pnr()].get_clr() &&
                        ((Ball_List[0].get_clr() != Ball_List[Ball_List[0].get_nnl()].get_clr() &&
                        Ball_List[0].get_clr() != Ball_List[Ball_List[0].get_pnl()].get_clr()) ||
                        Ball_List[0].get_clr() == Ball_List[Ball_List[0].get_nnr()].get_clr()))
                    {
                        count++;
                    }
                }
                else if (i == 15)
                {
                    int b = Ball_List[i].get_nnr();
                    bool b1, b2, b3;
                    if (Ball_List[5].get_clr() != Ball_List[Ball_List[5].get_nnl()].get_clr()) b1 = true;
                    if (Ball_List[5].get_clr() != Ball_List[Ball_List[5].get_pnl()].get_clr()) b2 = true;
                    if (Ball_List[5].get_clr() == Ball_List[Ball_List[5].get_nnl()].get_clr()) b3 = true;

                    int c = 1;

                    if (Ball_List[5].get_clr() != Ball_List[Ball_List[5].get_pnr()].get_clr() &&
                        ((Ball_List[5].get_clr() != Ball_List[Ball_List[5].get_nnl()].get_clr() &&
                        Ball_List[5].get_clr() != Ball_List[Ball_List[5].get_pnl()].get_clr()) ||
                        Ball_List[5].get_clr() == Ball_List[Ball_List[5].get_nnr()].get_clr()))
                    {
                        count++;
                    }
                }
                else if (i < 15)
                {
                    if (Ball_List[i + 19].get_clr() != Ball_List[Ball_List[i + 19].get_pnr()].get_clr())
                    {
                        count++;
                    }
                }
                else
                {
                    if (Ball_List[i + 18].get_clr() != Ball_List[Ball_List[i + 18].get_pnr()].get_clr())
                    {
                        count++;
                    }
                }
            }

            int j = 0;
            return count;
        }

        /// <summary>
        /// Calculates the minimum number of steps necessary to move from the current
        /// puzzle state to a state that qualifies as a solution to the puzzle.
        /// </summary>
        /// <param name="node"> The puzzle state for which we are calculating the minimum 
        /// number of steps to a solution.
        /// </param>
        /// <returns> Integer value indicating the minimum nuber of steps needed to
        /// reach a solution from the current state.
        /// </returns>
        private int Expected_Steps(Puzzle_state node)
        {
            int num_segments = Check(node);
            int min_steps = num_segments - 4;
            min_steps /= 4;
            return min_steps;
        }

        /// <summary>
        /// Populates children of the current node without creating a child that reverses the last
        /// move action taken in the puzzle.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<Puzzle_state> make_children(Puzzle_state node)
        {
            List<Puzzle_state> child_list = new List<Puzzle_state>();
            int last_move = node.get_last_move();
            /*last_move = -1;/**/
            int blah = 1;

            if (last_move != 2)
            {
                Puzzle_state new_node = new Puzzle_state(node);
                new_node.Spin_left_clockwise();
                new_node.set_last_move(0);
                child_list.Add(new_node);
            }
            if (last_move != 3)
            {
                Puzzle_state new_node = new Puzzle_state(node);
                new_node.Spin_right_clockwise();
                new_node.set_last_move(1);
                child_list.Add(new_node);
            }
            if (last_move != 0)
            {
                Puzzle_state new_node = new Puzzle_state(node);
                new_node.Spin_left_counterclockwise();
                new_node.set_last_move(2);
                child_list.Add(new_node);
            }
            if (last_move != 1)
            {
                Puzzle_state new_node = new Puzzle_state(node);
                new_node.Spin_right_counterclockwise();
                new_node.set_last_move(3);
                child_list.Add(new_node);
            }/**/

            return child_list;
        }

        /// <summary>
        /// Checks a given node to see if it is a solution to the puzzle, and if not, expands the
        /// node and 
        /// </summary>
        /// <param name="node"> The current Puzzle_state variable being checked to see if it is
        /// a solution.
        /// </param>
        /// <param name="g"> A measure of the number of steps necessary to reach the current point.
        /// </param>
        /// <param name="bound"> The expected number of steps to reach a solution.
        /// </param>
        /// <returns> The number of steps required to reach the solution, convereted to a negative
        /// number in to indicate that it is a solution.
        /// OR
        /// A new minimum number of steps required to reach a solution.
        /// </returns>
        private int Search(Puzzle_state node, int g, int bound)
        {
            int total_cost = Expected_Steps(node) + g;
            nodes_expanded++;
            if (total_cost > bound)
            { return total_cost; }
            if (Check(node) == 4)
            { return (-1 * g); }
            int minimum = int.MaxValue;
            int blah = make_children(node).ToArray().Length;
            int blah2 = 1;
            foreach (Puzzle_state new_node in make_children(node))
            {
                int test = Search(new_node, g + 1, bound);
                if (test < 0)
                {
                    if (!main_set)
                    {
                        main_root = new_node;
                        main_set = true;
                    }
                    return test; 
                }
                if (test < minimum)
                { minimum = test; }
            }
            return minimum;
        }

        /// <summary>
        /// Gets the node currently set as the tree's root.
        /// </summary>
        /// <returns> main_root, the current node set as the tree's root.  In practice, this
        /// could be either the root, or a solution to the puzzle.
        /// </returns>
        public Puzzle_state get_root()
        {
            return main_root;
        }

        /// <summary>
        /// Returns the number of nodes expanded on a given iteration of the search tree.
        /// </summary>
        /// <returns> The value of nodes_expanded.
        /// </returns>
        public long get_nodes_expanded()
        {
            return nodes_expanded;
        }

        /// <summary>
        /// Gets the elapsed runtime of the IDA* algorithm.
        /// </summary>
        /// <returns> Time elapsed in milliseconds.
        /// </returns>
        public long get_time_elapsed()
        {
            return keep_time.ElapsedMilliseconds;
        }

        /// <summary>
        /// Resets the IDA* object to pre-run conditions.
        /// </summary>
        public void reset()
        {
            keep_time.Reset();
            main_set = false;
            nodes_expanded = 0;
        }
    }
}
