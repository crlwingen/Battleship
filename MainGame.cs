/*============================================================================*
* Title       : Battleship w/ AI
* Description : A Battleship game with an AI that uses Iterative Deepening
*				Search to decide where to hit on the player's board.
* Filename    : MainGame.cs
* Version     : v3.0
* Author      : Gensaya, Carl Jerwin F.
* Yr&Sec&Uni  : BSCS 3-3 PUP Main
* Subject     : Artificial Intelligence
*============================================================================*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace battleShipV3 {

	public class MainGame : Form {
	
		public MainGame() {
		
			DisplayMainGame();
		
		}
		
		/*============================================================================*
	     *  Function   : DisplayMainGame
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: GUI for game mode selection.
	     *=============================================================================*/
		public void DisplayMainGame() {
		
			this.Size = new Size(1000, 650);
			this.BackColor = Color.Black;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.ControlBox = false;
			
			Image imgRad = Image.FromFile("resources/start.gif");
			pbRadar = new PictureBox();
			pbRadar.AutoSize  = true;
			pbRadar.Image = imgRad;
			pbRadar.Location = new Point(50, 100);
			this.Controls.Add(pbRadar);
			
			lblMain[0] = new Label();
			lblMain[0].Size = new Size(720, 80);
			lblMain[0].Font = new Font("OCR A Extended", 55);
			lblMain[0].Location = new Point(210, 130);
			lblMain[0].Text = "BATTLESHIP: IDS";
			lblMain[0].ForeColor = Color.Green;
			this.Controls.Add(lblMain[0]);
			
			pbPlayVsAI = new PictureBox();
			pbPlayVsAI.Size = new Size(260, 80);
			pbPlayVsAI.Location = new Point(60, 300);
			pbPlayVsAI.Tag = "pva";
			pbPlayVsAI.Image = Image.FromFile("resources/pva.png");
			pbPlayVsAI.MouseEnter += new EventHandler(pbEnter);
			pbPlayVsAI.MouseLeave += new EventHandler(pbLeave);
			pbPlayVsAI.Click += new EventHandler(pbClick);
			this.Controls.Add(pbPlayVsAI);
			
			lblMain[1] = new Label();
			lblMain[1].Size = new Size(570, 30);
			lblMain[1].Location = new Point(350, 320);
			lblMain[1].Text = "Objective: Fire. Destroy. Conquer.";
			lblMain[1].Font = new Font("OCR A Extended", 19);
			lblMain[1].BackColor = Color.Green;
			lblMain[1].ForeColor = Color.Black;
			this.Controls.Add(lblMain[1]);
			lblMain[1].Hide();
			
			pbAIVsAI = new PictureBox();
			pbAIVsAI.Size = new Size(260, 80);
			pbAIVsAI.Location = new Point(60, 400);
			pbAIVsAI.Tag = "ava";
			pbAIVsAI.Image = Image.FromFile("resources/ava.png");
			pbAIVsAI.MouseEnter += new EventHandler(pbEnter);
			pbAIVsAI.MouseLeave += new EventHandler(pbLeave);
			pbAIVsAI.Click += new EventHandler(pbClick);
			this.Controls.Add(pbAIVsAI);
			
			lblMain[2] = new Label();
			lblMain[2].Size = new Size(570, 30);
			lblMain[2].Location = new Point(350, 430);
			lblMain[2].Text = "Watch 2 commanders battle at the sea.";
			lblMain[2].Font = new Font("OCR A Extended", 18);
			lblMain[2].BackColor = Color.Green;
			lblMain[2].ForeColor = Color.Black;
			this.Controls.Add(lblMain[2]);
			lblMain[2].Hide();

			pbReady = new PictureBox();
			pbReady.Size = new Size(71, 76);
			pbReady.Location = new Point(880, 540);
			pbReady.Tag = "ready";
			pbReady.Image = Image.FromFile("resources/ready.png");
			pbReady.MouseEnter += new EventHandler(pbEnter);
			pbReady.MouseLeave += new EventHandler(pbLeave);
			pbReady.Click += new EventHandler(pbClick);
			this.Controls.Add(pbReady);
			pbReady.Hide();
			
			back = new PictureBox();
			back.Size = new Size(70, 60);
			back.Image = Image.FromFile("resources/back.png");
			back.Location = new Point(30, 560);
			back.Tag = "back";
			back.MouseEnter += new EventHandler(pbEnter);
			back.MouseLeave += new EventHandler(pbLeave);
			back.Click += new EventHandler(pbClick);
			this.Controls.Add(back);
			
		}
		
		/*============================================================================*
	     *  Function   : pbEnter
	     *  Params     : object source - component that triggered the event
	     *               EventArgs e - event argument 
	     *  Returns    : Void
	     *  Description: Changes picture box when it is hovered.
	     *=============================================================================*/
		private void pbEnter(object source, EventArgs e) {
			PictureBox sender = (PictureBox)source;
			
			if(sender.Tag.ToString() == "pva") { pbPlayVsAI.Image = Image.FromFile("resources/pva2.png"); }
			else if(sender.Tag.ToString() == "ava") { pbAIVsAI.Image = Image.FromFile("resources/ava2.png"); }
			else if(sender.Tag.ToString() == "back") { back.Image = Image.FromFile("resources/back2.png"); }
			else if(sender.Tag.ToString() == "ready") { pbReady.Image = Image.FromFile("resources/ready2.png"); }
		
		}
		
		/*============================================================================*
	     *  Function   : DisplapbLeaveyMainGame
	     *  Params     : object source - component that triggered the event
	     *               EventArgs e - event argument 
	     *  Returns    : Void
	     *  Description: Resets picture box's appearance.
	     *=============================================================================*/
		private void pbLeave(object source, EventArgs e) {
			PictureBox sender = (PictureBox)source;
			
			if(sender.Tag.ToString() == "pva") { pbPlayVsAI.Image = Image.FromFile("resources/pva.png"); }
			else if(sender.Tag.ToString() == "ava") { pbAIVsAI.Image = Image.FromFile("resources/ava.png"); }
			else if(sender.Tag.ToString() == "back") { back.Image = Image.FromFile("resources/back.png"); }
			else if(sender.Tag.ToString() == "ready") { pbReady.Image = Image.FromFile("resources/ready.png"); }
					
		}
		
		/*============================================================================*
	     *  Function   : pbClick
	     *  Params     : object source - component that triggered the event
	     *               EventArgs e - event argument  
	     *  Returns    : Void
	     *  Description: Handles click events for the picture boxes.
	     *=============================================================================*/
		private void pbClick(object source, EventArgs e) {
			PictureBox sender = (PictureBox)source;
			StartPage stp = new StartPage();
			
			if(sender.Tag.ToString() == "pva") { pbReady.Show(); lblMain[1].Show(); lblMain[2].Hide(); } 
			else if(sender.Tag.ToString() == "ava") { pbReady.Hide(); lblMain[1].Hide(); lblMain[2].Show(); }
			else if(sender.Tag.ToString() == "back") { stp.Show(); this.Dispose(); }
			else if(sender.Tag.ToString() == "ready") { this.Dispose(); gmb.Show(); } 
		}
		
		/*==============================INITIALIZATION==============================*/
		private PictureBox pbPlayVsAI, pbAIVsAI, pbRadar, pbReady, back;
		private Label[] lblMain = new Label[10];
		private GameBoard gmb = new GameBoard();
	
	}

}