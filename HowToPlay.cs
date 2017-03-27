/*============================================================================*
* Title       : Battleship w/ AI
* Description : A Battleship game with an AI that uses Iterative Deepening
*				Search to decide where to hit on the player's board.
* Filename    : HowToPlay.cs
* Version     : v3.0
* Author      : Gensaya, Carl Jerwin F.
* Yr&Sec&Uni  : BSCS 3-3 PUP Main
* Subject     : Artificial Intelligence
*============================================================================*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace battleShipV3 {

	public class HowToPlay : Form {

		public HowToPlay() {
		
			DisplayHowToPlay();
			
		}
		
		/*============================================================================*
	     *  Function   : DisplayHowToPlay
	     *  Params     : None
	     *  Returns    : Void
	     *  Description: GUI that shows instructions on how to play the game.
	     *=============================================================================*/
		public void DisplayHowToPlay() {
		
			this.Size = new Size(600, 500);
			this.StartPosition = FormStartPosition.CenterScreen; 
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.BackColor = Color.Black;
			this.ControlBox = false;
			
			howToPlayH[0] = new Label();
			howToPlayH[0].Size = new Size(250, 25);
			howToPlayH[0].Font = new Font("OCR A Extended", 10, FontStyle.Bold);
			howToPlayH[0].Location = new Point(20, 50);
			howToPlayH[0].ForeColor = Color.Green;
			howToPlayH[0].Text = "How to play? (Player VS AI)";
			this.Controls.Add(howToPlayH[0]);
			
			howToPlay[0] = new Label();
			howToPlay[0].Size = new Size(250, 30);
			howToPlay[0].Location = new Point(20, 80);
			howToPlay[0].ForeColor = Color.Green;
			howToPlay[0].Font = new Font("OCR A Extended", 10);
			howToPlay[0].Text = "| Game -> Player vs AI |";
			this.Controls.Add(howToPlay[0]);
			
			howToPlay[1] = new Label();
			howToPlay[1].Size = new Size(250, 115);
			howToPlay[1].Location = new Point(20, 110);
			howToPlay[1].ForeColor = Color.Green;
			howToPlay[1].Font = new Font("OCR A Extended", 10);
			howToPlay[1].Text = "Position all your ships on the board using the controls provided on the bottom of the first board. If all the ships are successfully placed on the board, the START! button will be enabled so you can begin the game.";
			this.Controls.Add(howToPlay[1]);
			
			howToPlay[2] = new Label();
			howToPlay[2].ForeColor = Color.Green;
			howToPlay[2].Size = new Size(250, 40);
			howToPlay[2].Location = new Point(20, 240);
			howToPlay[2].Font = new Font("OCR A Extended", 10);
			howToPlay[2].Text = "Start -> Pick a box on the enemy's board and sink all the ships!";
			this.Controls.Add(howToPlay[2]);
			
			howToPlay[3] = new Label();
			howToPlay[3].Size = new Size(250, 40);
			howToPlay[3].ForeColor = Color.Green;
			howToPlay[3].Location = new Point(20, 300);
			howToPlay[3].Font = new Font("OCR A Extended", 12);
			howToPlay[3].Text = "Good luck on the battlefield, Commander!";                          
			this.Controls.Add(howToPlay[3]);
			
			howToPlayH[1] = new Label();
			howToPlayH[1].Size = new Size(250, 25);
			howToPlayH[1].ForeColor = Color.Green;
			howToPlayH[1].Font = new Font("OCR A Extended", 10, FontStyle.Bold);
			howToPlayH[1].Location = new Point(330, 50);
			howToPlayH[1].Text = "How to play? (AI VS AI)";
			this.Controls.Add(howToPlayH[1]);
			
			howToPlay[4] = new Label();
			howToPlay[4].Size = new Size(250, 30);
			howToPlay[4].ForeColor = Color.Green;
			howToPlay[4].Location = new Point(330, 80);
			howToPlay[4].Font = new Font("OCR A Extended", 10);
			howToPlay[4].Text = "| Game -> AI vs AI |";
			this.Controls.Add(howToPlay[4]);	
			
			howToPlay[5] = new Label();
			howToPlay[5].ForeColor = Color.Green;
			howToPlay[5].Size = new Size(250, 60);
			howToPlay[5].Location = new Point(330, 110);
			howToPlay[5].Font = new Font("OCR A Extended", 10);
			howToPlay[5].Text = "Watch 2 AIs outsmart each other at the battle field.";
			this.Controls.Add(howToPlay[5]);
			
			Image img = Image.FromFile("resources/howto.gif");
			pbShipBattle = new PictureBox();
			pbShipBattle.Size = new Size(273, 153);
			pbShipBattle.Image = img;
			pbShipBattle.Location = new Point(305, 170);
			this.Controls.Add(pbShipBattle);
			
			howToPlayH[2] = new Label();
			howToPlayH[2].Size = new Size(280, 25);
			howToPlayH[2].ForeColor = Color.Green;
			howToPlayH[2].Font = new Font("OCR A Extended", 15, FontStyle.Bold);
			howToPlayH[2].Location = new Point(170, 5);
			howToPlayH[2].Text = "B A T T L E S H I P";
			this.Controls.Add(howToPlayH[2]);
		
					back = new PictureBox();
			back.Size = new Size(70, 60);
			back.Image = Image.FromFile("resources/back.png");
			back.Location = new Point(15, 420);
			back.MouseEnter += new EventHandler(backEnter);
			back.MouseLeave += new EventHandler(backLeave);
			back.Click += new EventHandler(backClick);
			this.Controls.Add(back);
		
		}
		
		/*============================================================================*
	     *  Function   : backEnter
	     *  Params     : object source - component that triggered the event
	     *               EventArgs e - event argument
	     *  Returns    : Void
	     *  Description: Changes back logo's appearance when hovered.
	     *=============================================================================*/
		private void backEnter(object source, EventArgs e) {
			
			back.Image = Image.FromFile("resources/back2.png");
			
		}
		
		/*============================================================================*
	     *  Function   : backLeave
	     *  Params     : object source - component that triggered the event
	     *               EventArgs e - event argument
	     *  Returns    : Void
	     *  Description: Resets back loogo's appearance when unhovered.
	     *=============================================================================*/
		private void backLeave(object source, EventArgs e) {
			
			back.Image = Image.FromFile("resources/back.png");
			
		}
		
		/*============================================================================*
	     *  Function   : backClick
	     *  Params     : object source - component that triggered the event
	     *               EventArgs e - event argument
	     *  Returns    : Void
	     *  Description: Takes the user back on the start page.
	     *=============================================================================*/
		private void backClick(object source, EventArgs e) {
			
			StartPage stp = new StartPage();
			this.Dispose();
			stp.Show();
			
		}

		/*==============================INITIALIZATION==============================*/
		private Label[] howToPlayH = new Label[10];
		private Label[] howToPlay = new Label[10];
		private PictureBox pbShipBattle, back;
	
	}

}