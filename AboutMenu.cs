/*============================================================================*
* Title       : Battleship w/ AI
* Description : A Battleship game with an AI that uses Iterative Deepening
*				Search to decide where to hit on the player's board.
* Filename    : AboutMenu.cs
* Version     : v3.0
* Author      : Gensaya, Carl Jerwin F.
* Yr&Sec&Uni  : BSCS 3-3 PUP Main
* Subject     : Artificial Intelligence
*============================================================================*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace battleShipV3 {

	public class AboutMenu : Form {
	
		public AboutMenu() {
		
			DisplayAboutMenu();
			
		}

		/*============================================================================*
	     *  Function   : DisplayAboutMenu
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Shows the about menu that contains the description and 
	     				 resouces used inside the program.
	    *=============================================================================*/	
		public void DisplayAboutMenu() {
		
			this.Size = new Size(650, 500);
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.BackColor = Color.Black;
			this.ControlBox = false;
			
			pBox = new PictureBox();
			pBox.Size = new Size(640, 360);
			Image img = Image.FromFile("resources/giphy.gif");
			pBox.BackgroundImage = img;
			
			lblAbout[0] = new Label();
			lblAbout[0].Text = "BATTLESHIP GAME w/ AI: IDS";
			lblAbout[0].Font = new Font("OCR A Extended", 15);
			lblAbout[0].ForeColor = Color.Green;
			lblAbout[0].Size = new Size(500, 25);
			lblAbout[0].Location = new Point(147, 30);
			this.Controls.Add(lblAbout[0]);
			
			lblAbout[1] = new Label();
			lblAbout[1].Text = "By: Gensaya, CJ F. and Magturo, JP";
			lblAbout[1].Font = new Font("OCR A Extended", 10);
			lblAbout[1].ForeColor = Color.Green;
			lblAbout[1].Size = new Size(290, 20);
			lblAbout[1].Location = new Point(180, 60);
			this.Controls.Add(lblAbout[1]);
			
			lblAbout[2] = new Label();
			lblAbout[2].Text = "This game is coded using Notepad++ and C#. The game aims to demonstrate how an AI using an ITERATIVE DEEPENING SEARCH will behave in the game. The AI uses a stack to create a search tree using a HIT box as the root. Although this is not the best algorithm to be used in this game, it still showed how useful the algo used here can be depending on the situation.";
			lblAbout[2].Font = new Font("OCR A Extended", 12);
			lblAbout[2].ForeColor = Color.Green;
			lblAbout[2].Size = new Size(500, 150);
			lblAbout[2].Location = new Point(70, 120);
			this.Controls.Add(lblAbout[2]);
			
			lblAbout[3] = new Label();
			lblAbout[3].Text = "REFERENCES";
			lblAbout[3].Font = new Font("OCR A Extended", 13);
			lblAbout[3].ForeColor = Color.Green;
			lblAbout[3].Location = new Point(255, 280);
			lblAbout[3].Size = new Size(150, 30);
			this.Controls.Add(lblAbout[3]);
			
			references[0] = new PictureBox();
			references[0].Size = new Size(200, 25);
			references[0].Location = new Point(75, 310);
			references[0].Image = Image.FromFile("resources/datagen.png");
			this.Controls.Add(references[0]);	
			
			references[1] = new PictureBox();
			references[1].Size = new Size(200, 25);
			references[1].Location = new Point(75, 370);
			references[1].Image = Image.FromFile("resources/stackover.png");
			this.Controls.Add(references[1]);
			
			references[2] = new PictureBox();
			references[2].Size = new Size(200, 25);
			references[2].Location = new Point(350, 310);
			references[2].Image = Image.FromFile("resources/reddit.png");
			this.Controls.Add(references[2]);
			
			references[3] = new PictureBox();
			references[3].Size = new Size(78, 84);
			references[3].Location = new Point(410, 350);
			references[3].Image = Image.FromFile("resources/hasbro.png");
			this.Controls.Add(references[3]);
			
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
	     *  Description: Mouse enter event for the back button.
	     *=============================================================================*/
		private void backEnter(object source, EventArgs e) {
			
			back.Image = Image.FromFile("resources/back2.png");
			
		}
		
		/*============================================================================*
	     *  Function   : backLeave
	     *  Params     : object source - component that triggered the event
		 *               EventArgs e - event argument
	     *  Returns    : Void
	     *  Description: Resets back button image.
	     *=============================================================================*/
		private void backLeave(object source, EventArgs e) {
			
			back.Image = Image.FromFile("resources/back.png");
			
		}
		
		/*============================================================================*
	     *  Function   : backClick
	     *  Params     : object source - component that triggered the event
		 *               EventArgs e - event argument
	     *  Returns    : Void
	     *  Description: Back click event. Sends the user back to the main menu.
	     *=============================================================================*/
		private void backClick(object source, EventArgs e) {
			
			StartPage stp = new StartPage();
			this.Dispose();
			stp.Show();
			
		}

		/*==============================INITIALIZATION==============================*/
		private PictureBox pBox, back;
		private PictureBox[] references = new PictureBox[10];
		private Label[] lblAbout = new Label[10];
	
	}

}