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
 * Project purpose: Create a program to randomize and solve a Hungarian Rings puzzle
 * The puzzle consists of a total of 38 balls divided between two interlocked rings with four balls in the
 *  overlapping space of each ring; the rings intersect at two locations, each 1 ball in width, so two balls
 *  are shared by both rings.
 *  Ball colors:
 *      10 black
 *      10 red
 *      09 blue
 *      09 yellow
 *   The object of the puzzle is to re-arrange the balls into a random arrangement starting from complete, wherein
 *   all the balls of each color form a single, unbroken line.
 *   Start ball count at the lower overlap location, moving counter-clocwise along left-hand ring until ball
 *      #20 is reached.  From there, start counting ball #21 as being the ball counter-clockwise from ball #1
 *      along the right-hand ring.  ball #6 is located at the second overlap between balls 34 and 35.
 *      Balls 6 and 1 must be considered more than once when checking for puzzle correctness.
 *   Perhaps correctness could be checked by counting around each ring individually and recording the # of
 *      adjacent balls of each color within each ring.  Due to practical restraints, if all 4 colors appear 
 *      within either ring, then the puzzle cannot be correctly solved.
 *   Supposedly there are several potential solutions to the puzzle.
 * Possible moves:
 *  Rotate right-hand ring one ball clockwise
 *  Rotate right-hand ring one ball counter-clockwise
 *  Rotate left-hand ring one ball clockwise
 *  Rotate left-hand ring one ball counter-clockwise
 *  
 * 
 * References:
 * Windows Form Application Template from VS 2010
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace hungarian_rings
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
