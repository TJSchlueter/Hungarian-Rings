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
 * This class is the UI portion of the program.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using System.IO;

namespace hungarian_rings
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Array upon which the puzzle is constructed.  It is used to re-arrange the puzzle by
        /// changing the color of the balls in specified locations.  The hr_node objects also
        /// contain integer variables indicating their relative location within the puzzle.
        /// </summary>
        hr_node[] Ball_list;

        /// <summary>
        /// Array of oval-shaped drawable objects, which represent the visible balls in the 
        /// Hungarian Ringspuzzle.  After initial construction, their color is modified based
        /// upon the values of the corresponding hr_node objects.
        /// </summary>
        OvalShape[] Ball_group;

        /// <summary>
        /// Provides support for the OvalShape objects used to represent the balls.
        /// </summary>
        ShapeContainer canvas;

        /// <summary>
        /// Puzzle_state object representing the puzzle as it appears on screen.
        /// </summary>
        Puzzle_state base_state;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public Form1()
        {
            // Initializes the basic GUI components
            InitializeComponent();

            // All else in this function creates the puzzle
            Ball_list = new hr_node[38];

            base_state = new Puzzle_state();

            canvas = new ShapeContainer();
            canvas.Parent = this;

            Ball_group = new OvalShape[38];

            /* The following four arrays define the locations of the balls in the puzzle
             * relative to the center of one of the two circles, depending on the ball's
             * location within the puzzle.
             * 
             **************************************************
             * 
             * X_coords and Y_coords are used to construct the left-hand circle, using
             * a total of 20 balls.
             * X_coords2 and Y_coords2 are used to construct the right-hand circle, using
             * a total of 18 balls, skipping over locations that overlap with the left-
             * hand circle.
             */
            int[] X_coords = { 58, 20, -20, -58, -90, -113, -125, -125, -113, -90,
                                 -58, -20, 20, 58, 90, 113, 126, 126, 113, 90};
            int[] Y_coords = { 113, 126, 126, 113, 90, 58, 20, -20, -58, -90, -113,
                                 -125, -125, -113, -90, -58, -20, 20, 58, 90 };
            int[] X_coords2 = {  -113, -125, -125, -113, -58, -20, 20, 58, 90, 113,
                                  126, 126, 113, 90, 58, 20, -20, -58};
            int[] Y_coords2 = { 58, 20, -20, -58, -113, -125, -125, -113, -90, -58,
                                  -20, 20, 58, 90, 113, 126, 126, 113};

            //loop to define the members of the left-hand circle
            for (int i = 0; i < 20; i++)
            {
                //Create drawable objects 0-19
                Ball_group[i] = new OvalShape();
                Ball_group[i].Parent = canvas;
                Ball_group[i].Size = new System.Drawing.Size(40, 40);
                Ball_group[i].Location = new System.Drawing.Point(X_coords[19 - i] + 200,
                                                                  Y_coords[19 - i] + 200);
                Ball_group[i].FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                Ball_group[i].BorderColor = System.Drawing.Color.White;
            }

            //loop to define the members of the right-hand circle
            for (int i = 0; i < 18; i++)
            {
                //Create drawable objects 20-37
                Ball_group[20 + i] = new OvalShape();
                Ball_group[20 + i].Parent = canvas;
                Ball_group[20 + i].Size = new System.Drawing.Size(40, 40);
                Ball_group[20 + i].Location = new System.Drawing.Point(X_coords2[17 - i] + 380,
                                                                       Y_coords2[17 - i] + 200);
                Ball_group[20 + i].FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                Ball_group[20 + i].BorderColor = System.Drawing.Color.White;
            }
            Update();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Updates the color of the drawn objects in the puzzle.
        /// </summary>
        private void Update()
        {
            hr_node[] Ball_list1 = new hr_node[38];
            Ball_list1 = base_state.get_Ball_List();

            for (int i = 0; i < 38; i++)
            {
                //set the node's new color
                if (Ball_list1[i].get_clr() == 1)
                { Ball_group[i].FillColor = System.Drawing.Color.Red; }
                else if (Ball_list1[i].get_clr() == 2)
                { Ball_group[i].FillColor = System.Drawing.Color.Yellow; }
                else if (Ball_list1[i].get_clr() == 3)
                { Ball_group[i].FillColor = System.Drawing.Color.Blue; }
                else if (Ball_list1[i].get_clr() == 4)
                { Ball_group[i].FillColor = System.Drawing.Color.Black; }
                else
                { Ball_group[i].FillColor = System.Drawing.Color.White; }
                int k = 1;
            }
            int j = 1;
        }

        /// <summary>
        /// Sets the current state of the puzzle to a different Puzzle_state object.
        /// </summary>
        /// <param name="node"></param>
        private void set_State(Puzzle_state node)
        {
            base_state = node;
            Update();
        }

        /// <summary>
        /// Rancomizes the distribution of the balls in the puzzle as described below.
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Randomize_balls(object sender, EventArgs e)
        {
            /*IDA_star IDA = new IDA_star();
            List<Puzzle_state> test_list = IDA.make_children(base_state);
            set_State(test_list[3]);/**/

            int number_of_moves = (int)Move_number.Value;
            Move_number.Value = 10;
            base_state.Randomize(number_of_moves);

            //Update the graphics displayed
            Update();
        }

        /// <summary>
        /// Button EventHandler to spin the left-hand ring clockwise.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B1(object sender, EventArgs e)
        {
            base_state.Spin_left_clockwise();

            Update();
        }

        /// <summary>
        /// Button EventHandler to spin the right-hand ring clockwise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B2(object sender, EventArgs e)
        {
            base_state.Spin_right_clockwise();

            Update();
        }

        /// <summary>
        /// Button EventHandler to spin the left-hand ring counter-clockwise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B3(object sender, EventArgs e)
        {
            base_state.Spin_left_counterclockwise();

            Update();
        }

        /// <summary>
        /// Button EventHandler to spin the right-hand ring counter-clockwise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B4(object sender, EventArgs e)
        {
            base_state.Spin_right_counterclockwise();

            Update();
        }

        /// <summary>
        /// Creates and runs the IDA* algorithm a set number of times with the puzzle randomized
        /// a number of moves between the values of Range_Min and Range_Max.
        /// Also the event handler for IDA_star_button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void run_IDA_star(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch keep_time = new System.Diagnostics.Stopwatch();
            StreamWriter file_write = new StreamWriter(@"Data_out.csv");
            file_write.WriteLine("k-value, steps needed, nodes expanded, time elapsed");

            Random rand1 = new Random();
            int k = rand1.Next((int)Range_Max.Value) + (int)Range_Min.Value;
            
            IDA_star IDA = new IDA_star();
            //Loop runs 5 randomizations between the values of Range_Min and Range_Max
            for (int i = 0; i < 5; i++)
            {
                k = rand1.Next((int)Range_Max.Value);
                if (k < (int)Range_Min.Value)
                { k = (int)Range_Min.Value; }

                base_state.Randomize(k);
                //base_state.Spin_left_clockwise();
                int m = IDA.run(base_state);
                int a = 1;
                if (m > 0) { set_State(IDA.get_root()); }
                else
                {
                    base_state = new Puzzle_state();
                    Update();
                }

                long time_check = IDA.get_time_elapsed();
                int b = m;
                file_write.WriteLine(k + ", " + m + ", " + IDA.get_nodes_expanded() + "," + time_check);
                IDA.reset();
            }
            file_write.Close();
            Update();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base_state = new Puzzle_state();
            Update();
        }

    }
}
