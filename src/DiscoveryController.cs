
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The battle phase is handled by the DiscoveryController.
/// </summary>
static class DiscoveryController
{

	/// <summary>
	/// Handles input during the discovery phase of the game.
	/// </summary>
	/// <remarks>
	/// Escape opens the game menu. Clicking the mouse will
	/// attack a location.
	/// </remarks>
	public static void HandleDiscoveryInput()
	{
		//opens the game menu.
		if (SwinGame.KeyTyped(KeyCode.VK_ESCAPE)) {
			AddNewState(GameState.ViewingGameMenu);
		}

		//launches an attack at mouse location.
		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			DoAttack();
		}
	}

	/// <summary>
	/// Attack the location that the mouse if over.
	/// </summary>
	private static void DoAttack()
	{
		Point2D mouse = default(Point2D);

		//get where the coordinates where the mouse is located.
		mouse = SwinGame.MousePosition();

		//Calculate the row/col clicked
		int row = 0;
		int col = 0;
		row = Convert.ToInt32(Math.Floor((mouse.Y - FIELD_TOP) / (CELL_HEIGHT + CELL_GAP)));
		col = Convert.ToInt32(Math.Floor((mouse.X - FIELD_LEFT) / (CELL_WIDTH + CELL_GAP)));

		//if row and col are in the confins of the grid, attack.
		if (row >= 0 & row < HumanPlayer.EnemyGrid.Height) {
			if (col >= 0 & col < HumanPlayer.EnemyGrid.Width) {
				Attack(row, col);
			}
		}
	}

	/// <summary>
	/// Draws the game during the attack phase.
	/// </summary>s
	public static void DrawDiscovery()
	{
		const int SCORES_LEFT = 172;
		const int SHOTS_TOP = 157;
		const int HITS_TOP = 206;
		const int SPLASH_TOP = 256;

		//decides whether to draw enemy ships on the grid or not.
		if ((SwinGame.KeyDown(KeyCode.VK_LSHIFT) | SwinGame.KeyDown(KeyCode.VK_RSHIFT)) & SwinGame.KeyDown(KeyCode.VK_C)) {
			DrawField(HumanPlayer.EnemyGrid, ComputerPlayer, true);
		} else {
			DrawField(HumanPlayer.EnemyGrid, ComputerPlayer, false);
		}

		//draws a small players grid in the bottom left of the screen.
		DrawSmallField(HumanPlayer.PlayerGrid, HumanPlayer);
		DrawMessage();

		//draws all the variables onto the screen.
		SwinGame.DrawText(HumanPlayer.Shots.ToString(), Color.White, GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
		SwinGame.DrawText(HumanPlayer.Hits.ToString(), Color.White, GameFont("Menu"), SCORES_LEFT, HITS_TOP);
		SwinGame.DrawText(HumanPlayer.Missed.ToString(), Color.White, GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
