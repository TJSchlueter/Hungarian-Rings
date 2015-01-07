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
 * hr_node is the class created to contain relevant information for the each of the 38 nodes
 *  in the Hungarian Rings puzzle.
 * Member variables:
 *  color - value carrying one of 4 possible values representing the ball's color
 *  next_node_right - the index of the next node, 1 move counter-clockwise, around the right-hand ring
 *      this value is not always defined, so a default will be created
 *  prev_node_right - the index of the previous node, 1 move clockwise around, around the right-hand ring
 *      this value is not always defined, so a default will be created
 *  next_node_left - the index of the next node, 1 move counter-clockwise, around the left-hand ring
 *      this value is not always defined, so a default will be created
 *  prev_node_left - the index of the previous node, 1 move clockwise around, around the left-hand ring
 *      this value is not always defined, so a default will be created
 * Member functions:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hungarian_rings
{
    /// <summary>
    /// Class created to represent any one of the 38 nodes in the Hungarian Rings puzzle.
    /// </summary>
    /// <param name="color">Integer indicating which color the ball should be drawn as.
    /// This value should be re-assigned every time a node is affected by a move operation even
    /// if the value ends up being the same as it was before.
    /// Functions and variables intended to modify this variable should contain the abbreviation
    /// "clr" in order to indicate their purpose.
    /// </param>
    /// <param name="next_node_right">Integer indicating the index of the next node, 1 space counter-
    /// clockwise, around the right-hand ring.
    /// Functions and variables intended to access this variable use an abbreviation
    /// of "nnr" somewhere within their function or variable name.
    /// Once created, this value should not change, therefore a set function is not implemented.
    /// </param>
    /// <param name="prev_node_right">Integer indicating the index of the previous node, 1 space
    /// clockwise, around the right-hand ring.
    /// Functions and variables intended to access this variable use an abbreviation
    /// of "pnr" somewhere within their function or variable name.
    /// Once created, this value should not change, therefore a set function is not implemented.
    /// </param>
    /// <param name="next_node_left">Integer indicating the index of the next node, 1 space counter-
    /// clockwise, around the left-hand ring.
    /// Functions and variables intended to access this variable use an abbreviation
    /// of "nnl" somewhere within their function or variable name.
    /// Once created, this value should not change, therefore a set function is not implemented.
    /// </param>
    /// <param name="prev_node_left">Integer indicating the index of the previous node, 1 space
    /// clockwise, around the left-hand ring.
    /// Functions and variables intended to access this variable use an abbreviation
    /// of "pnl" somewhere within their function or variable name.
    /// Once created, this value should not change, therefore a set function is not implemented.
    /// </param>
    /// <param name="error_detect">Bool indicating that an invalid value has been assigned to
    /// one of the member variables when true, and indicating that all is well when false.
    /// This variable is intended for use primarily in testing as normal runs should only provide
    /// values within the expected bounds of the other member functions
    /// </param>
    
    public class hr_node
    {
        /// <summary>
        /// Variable containing a value indicating the color of the ball displayed in the node.
        /// </summary>
        int color;
        /// <summary>
        /// Varaibles containing the index of nodes adjacent to this one.
        /// </summary>
        int next_node_right, prev_node_right, next_node_left, prev_node_left;
        /// <summary>
        /// Variable that, when true, indicates that a problem has occurred.
        /// </summary>
        bool error_detect;

        /// <summary>
        /// Constructor used to create hr_node objects lying at an the intersection between the two rings.
        /// </summary>
        /// <param name="clr_in">The color to be assigned to the node at creation time.</param>
        /// <param name="nnr_in">The index of the next node along the right-hand ring.</param> 
        /// <param name="pnr_in">The index of the previous node along the right-hand ring.</param>
        /// <param name="nnl_in">The index of the next node along the left-hand ring.</param>
        /// <param name="pnl_in">The index of the previous node along the left-hand ring.</param>
        public hr_node(int clr_in, int nnr_in, int pnr_in, int nnl_in, int pnl_in)
        {
            color = clr_in;
            next_node_right = nnr_in;
            prev_node_right = pnr_in;
            next_node_left = nnl_in;
            prev_node_left = pnl_in;
            error_detect = false;
        }

        /// <summary>
        /// Constructor used to create hr_node objects lying exclusively on within one of the two rings.
        /// </summary>
        /// <param name="clr_in">The color to be assigned to the node at creation time.</param>
        /// <param name="nr_in">The index of the next node along the right-hand ring.</param>
        /// <param name="pr_in">The index of the previous node along the right-hand ring.</param>
        /// <param name="r_or_l">Integer value indicating which of the two rings the node lies within.</param>
        public hr_node(int clr_in, int nn_in, int pn_in, int r_or_l)
        {
            color = clr_in;
            if (r_or_l == 1)
            {
                next_node_right = nn_in;
                prev_node_right = pn_in;
                next_node_left = -1;
                prev_node_left = -1;
                error_detect = false;
            }
            else if (r_or_l == 2)
            {
                next_node_right = -1;
                prev_node_right = -1;
                next_node_left = nn_in;
                prev_node_left = pn_in;
                error_detect = false;
            }
            else
            {
                next_node_right = -1;
                prev_node_right = -1;
                next_node_left = -1;
                prev_node_left = -1;
                error_detect = true;
            }
        }

        /// <summary>
        /// Constructor to create a new objecty based upon a previously existing one.
        /// </summary>
        /// <param name="node_in">
        /// The hr_node object that will server as the base for the newly created node.
        /// </param>
        public hr_node(hr_node node_in)
        {
            color = node_in.get_clr();
            next_node_right = node_in.get_nnr();
            prev_node_right = node_in.get_pnr();
            next_node_left = node_in.get_nnl();
            prev_node_left = node_in.get_pnl();
            error_detect = false;
        }

        /// <summary>
        /// Default constructor for hr_node objects.  It should be unnecessary, but is included in the event
        /// that it should at some point become necessary.  Additionally, the values inserted into the member
        /// variables are what any member variables should be set to by default.  -1 should not be used for 
        /// anything other than default values and testing purposes.
        /// </summary>
        public hr_node()
        {
            color = -1;
            next_node_right = -1;
            prev_node_right = -1;
            next_node_left = -1;
            prev_node_left = -1;
            error_detect = false;
        }

        /// <summary>
        /// Gets the value of the next_node_right member variable.
        /// </summary>
        /// <returns>Integer value from next_node_right.</returns>
        public int get_nnr()
        {
            return next_node_right;
        }

        /// <summary>
        /// Gets the value of the prev_node_right member variable.
        /// </summary>
        /// <returns>Integer value from prev_node_right.</returns>
        public int get_pnr()
        {
            return prev_node_right;
        }

        /// <summary>
        /// Gets the value of the next_node_left member variable.
        /// </summary>
        /// <returns>Integer value from next_node_left.</returns>
        public int get_nnl()
        {
            return next_node_left;
        }

        /// <summary>
        /// Gets the value of the prev_node_left member variable.
        /// </summary>
        /// <returns>Integer value from prev_node_left.</returns>
        public int get_pnl()
        {
            return prev_node_left;
        }

        /// <summary>
        /// Gets the value of the color member variable.
        /// </summary>
        /// <returns>Integer value from color.</returns>
        public int get_clr()
        {
            return color;
        }

        /// <summary>
        /// Assigns a new value to the color variable.  This should normally be a value
        /// obtained through a get_clr() function call on a different node.
        /// </summary>
        /// <param name="clr_in">The value to be assigned to the color variable.</param>
        public void set_clr(int clr_in)
        {
            color = clr_in;
        }
    }
}
