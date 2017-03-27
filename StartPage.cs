





	
		
		
		
		
		
		
		
		
		
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
					abt = new AboutMenu();
					htp = new HowToPlay();
					man = new MainGame();
				abt.Show(); 
				Application.Exit();
				htp.Show(); 
				if(abt.IsDisposed == true) 
				if(htp.IsDisposed == true) 
				if(man.IsDisposed == true)
				man.Show();
				this.Hide();
				this.Hide();
				this.Hide();
			Application.Run(new StartPage());
			back = new PictureBox();
			back.Click += new EventHandler(pbClick);
			back.Image = Image.FromFile("resources/back.png");
			back.Location = new Point(30, 560);
			back.MouseEnter += new EventHandler(pbEnter);
			back.MouseLeave += new EventHandler(pbLeave);
			back.Size = new Size(70, 60);
			back.Tag = "back";
			DisplayStartPage();
			else if(sender.Tag.ToString() == "about") { pbAbout.Image = Image.FromFile("resources/aboutgame.png"); }
			else if(sender.Tag.ToString() == "about") { pbAbout.Image = Image.FromFile("resources/aboutgame2.png"); }
			else if(sender.Tag.ToString() == "back") { back.Image = Image.FromFile("resources/back.png"); }
			else if(sender.Tag.ToString() == "back") { back.Image = Image.FromFile("resources/back2.png"); }
			else if(sender.Tag.ToString() == "howto") { pbHowTo.Image = Image.FromFile("resources/howtogame.png"); }
			else if(sender.Tag.ToString() == "howto") { pbHowTo.Image = Image.FromFile("resources/howtogame2.png"); }
			if(sender.Tag.ToString() == "start") {
			if(sender.Tag.ToString() == "start") { pbStart.Image = Image.FromFile("resources/startgame.png"); }
			if(sender.Tag.ToString() == "start") { pbStart.Image = Image.FromFile("resources/startgame2.png"); }
			Image imgRad = Image.FromFile("resources/start.gif");
			lblStart[0] = new Label();
			lblStart[0].Font = new Font("OCR A Extended", 50);
			lblStart[0].ForeColor = Color.Green;
			lblStart[0].Location = new Point(210, 130);
			lblStart[0].Size = new Size(750, 80);
			lblStart[0].Text = "BATTLESHIP: IDS";
			pbAbout = new PictureBox();
			pbAbout.Click += new EventHandler(pbClick);
			pbAbout.ForeColor = Color.Green;
			pbAbout.Image = Image.FromFile("resources/aboutgame.png");
			pbAbout.Location = new Point(300, 430);
			pbAbout.MouseEnter += new EventHandler(pbEnter);
			pbAbout.MouseLeave += new EventHandler(pbLeave);
			pbAbout.Size = new Size(380, 80);
			pbAbout.Tag = "about";
			pbHowTo = new PictureBox();
			pbHowTo.Click += new EventHandler(pbClick);
			pbHowTo.ForeColor = Color.Green;
			pbHowTo.Image = Image.FromFile("resources/howtogame.png");
			pbHowTo.Location = new Point(300, 350);
			pbHowTo.MouseEnter += new EventHandler(pbEnter);
			pbHowTo.MouseLeave += new EventHandler(pbLeave);
			pbHowTo.Size = new Size(380, 80);
			pbHowTo.Tag = "howto";
			pbRadar = new PictureBox();
			pbRadar.AutoSize  = true;
			pbRadar.Image = imgRad;
			pbRadar.Location = new Point(50, 100);
			pbStart = new PictureBox();
			pbStart.Click += new EventHandler(pbClick);
			pbStart.ForeColor = Color.Green;
			pbStart.Image = Image.FromFile("resources/startgame.png");
			pbStart.Location = new Point(300, 270);
			pbStart.MouseEnter += new EventHandler(pbEnter);
			pbStart.MouseLeave += new EventHandler(pbLeave);
			pbStart.Size = new Size(380, 80);
			pbStart.Tag = "start";
			PictureBox sender = (PictureBox)source;
			PictureBox sender = (PictureBox)source;
			PictureBox sender = (PictureBox)source;
			this.BackColor = Color.Black;
			this.ControlBox = false;
			this.Controls.Add(back);
			this.Controls.Add(lblStart[0]);
			this.Controls.Add(pbAbout);	
			this.Controls.Add(pbHowTo);
			this.Controls.Add(pbRadar);
			this.Controls.Add(pbStart);
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.Size = new Size(1000, 650);
			this.StartPosition = FormStartPosition.CenterScreen;
			}
			} else if(sender.Tag.ToString() == "about") {  
			} else if(sender.Tag.ToString() == "back") {
			} else if(sender.Tag.ToString() == "howto") { 
		/*============================================================================*
		/*============================================================================*
		/*============================================================================*
		/*============================================================================*
		/*============================================================================*
		/*==============================INITIALIZATION==============================*/
		private AboutMenu abt = new AboutMenu();
		private HowToPlay htp = new HowToPlay();
		private Label[] lblStart = new Label[10];
		private MainGame man = new MainGame();
		private PictureBox pbRadar, pbStart, pbAbout, pbHowTo, back;
		private void pbClick(object source, EventArgs e) {
		private void pbEnter(object source, EventArgs e) {
		private void pbLeave(object source, EventArgs e) {
		public StartPage() {
		public static void Main(String[] args) {
		public void DisplayStartPage() {
		}
		}
		}
		}
		}
		}
	     * 				 EventArgs e - event argument 
	     * 				 EventArgs e - event argument 
	     * 				 EventArgs e - event argument  
	     *  Description: Changes picture box's image content when hovered.
	     *  Description: Entry point of the program.
	     *  Description: Handles picture boxes' click events.
	     *  Description: Resets picture box's appearance when unhovered.
	     *  Description: Shows welcome page of the program.
	     *  Function   : DisplayStartPage
	     *  Function   : Main
	     *  Function   : pbClick
	     *  Function   : pbEnter
	     *  Function   : pbLeave
	     *  Params     : None 
	     *  Params     : object source - component that triggered the event
	     *  Params     : object source - component that triggered the event
	     *  Params     : object source - component that triggered the event
	     *  Params     : String[] args 
	     *  Returns    : Void
	     *  Returns    : Void
	     *  Returns    : Void
	     *  Returns    : Void
	     *  Returns    : Void
	     *=============================================================================*/
	     *=============================================================================*/
	     *=============================================================================*/
	     *=============================================================================*/
	     *=============================================================================*/
	public class StartPage : Form {
	}
*				Search to decide where to hit on the player's board.
* Author      : Gensaya, Carl Jerwin F.
* Description : A Battleship game with an AI that uses Iterative Deepening
* Filename    : StartPage.cs
* Subject     : Artificial Intelligence
* Title       : Battleship w/ AI
* Version     : v3.0
* Yr&Sec&Uni  : BSCS 3-3 PUP Main
*============================================================================*/
/*============================================================================*
namespace battleShipV3 {
using System.Drawing;
using System.Windows.Forms;
using System;
}