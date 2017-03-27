/*============================================================================*
* Title       : Battleship w/ AI
* Description : A Battleship game with an AI that uses Iterative Deepening
*				Search to decide where to hit on the player's board.
* Filename    : GameBoard.cs
* Version     : v3.0
* Author      : Gensaya, Carl Jerwin F.
* Yr&Sec&Uni  : BSCS 3-3 PUP Main
* Subject     : Artificial Intelligence
*============================================================================*/

using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace battleShipV3 {
	
	public class GameBoard : Form {
		
		public GameBoard() {
			
			displayGUI();
			
		} // End of public mainGame().
		
		/*============================================================================*
	     *  Function   : displayGUI
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Displays the game board GUI.
	     *=============================================================================*/
		public void displayGUI() {
			
			this.Size = new Size(1000, 650);
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.ControlBox = false;
			this.BackColor = Color.Black;
			
			initBoardComponents();
			
		} // End of public void displayGUI().
		
		/*============================================================================*
	     *  Function   : displayGUI
	     *  Params     : PictureBox[,] TEMPBOARD - board cell holder
	     *			     int marginLeft - puts margins on the left
	     *				 int marginTop - puts margin on the right
	     *               int intX - x coordinate of the board
	     *               int intY - y coordinate of the board
	     *  Returns    : Void
	     *  Description: Displays the game board GUI.
	     *=============================================================================*/
		public void drawBoard(PictureBox[,] TEMPBOARD, int marginLeft, int marginTop, int intX, int intY) { // 80, 40
			
			// DRAWS THE BOARD
			char chrTemp;
			coorXYlbl(intX, intY);
			for(int intOLoop = 0; intOLoop < 10; intOLoop++) {
				for(int intILoop = 0; intILoop < 10; intILoop++) {
					chrTemp = (char)('A' + intILoop);
					TEMPBOARD[intOLoop, intILoop] = new PictureBox();
					TEMPBOARD[intOLoop, intILoop].Size = new Size(35, 35);
					TEMPBOARD[intOLoop, intILoop].BackColor = Color.White;
					TEMPBOARD[intOLoop, intILoop].Location = new Point(marginLeft + (intOLoop * 36), marginTop + (intILoop * 36));
					TEMPBOARD[intOLoop, intILoop].Image = Image.FromFile("resources/box.png");
					TEMPBOARD[intOLoop, intILoop].Tag = chrTemp.ToString() +  intOLoop.ToString();;
					this.Controls.Add(TEMPBOARD[intOLoop, intILoop]);
				}
			}
			
		} // End of public void drawBoard(PictureBox[,] TEMPBOARD, int marginLeft, int marginTop).
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public void initBoardComponents() {
		
			lblShip = new Label();
			lblShip.Size = new Size(100, 25);
			lblShip.Location = new Point(25, 500);
			lblShip.Text = "Ships        :";
			lblShip.Font = new Font("OCR A Extended", 8);
			lblShip.ForeColor = Color.Green;
			this.Controls.Add(lblShip);
			
			cmbShips = new ComboBox();
			cmbShips.Items.Add("Destroyer (2 blocks)");
			cmbShips.Items.Add("Cruiser (3 blocks)");
			cmbShips.Items.Add("Submarine (3 blocks)");
			cmbShips.Items.Add("Battleship (4 blocks)");
			cmbShips.Items.Add("Aircraft Carrier (5 blocks)");
			cmbShips.SelectedIndex = 0;
			cmbShips.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbShips.Size = new Size(140, 20);
			cmbShips.Location = new Point(130, 500);
			this.Controls.Add(cmbShips);
			
			lblPos = new Label();
			lblPos.Size = new Size(100, 25);
			lblPos.Location = new Point(25, 530);
			lblPos.Text = "Position     :";
			lblPos.Font = new Font("OCR A Extended", 8);
			lblPos.ForeColor = Color.Green;
			this.Controls.Add(lblPos);
			
			char chrXLbl =  'A';
			cmbPosX = new ComboBox();
			for(int intLoop = 1; intLoop < 11; intLoop++)
				cmbPosX.Items.Add(chrXLbl++);
			cmbPosX.SelectedIndex = 0;
			cmbPosX.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbPosX.Size = new Size(40, 20);
			cmbPosX.Location = new Point(130, 530);
			this.Controls.Add(cmbPosX);
			
			cmbPosY = new ComboBox();
			for(int intLoop = 1; intLoop < 11; intLoop++)
				cmbPosY.Items.Add(intLoop.ToString());
			cmbPosY.SelectedIndex = 0;
			cmbPosY.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbPosY.Size = new Size(40, 20);
			cmbPosY.Location = new Point(180, 530);
			this.Controls.Add(cmbPosY);

			lblOrien = new Label();
			lblOrien.Size = new Size(100, 25);
			lblOrien.Location = new Point(25, 560);
			lblOrien.Text = "Orientation  :";
			lblOrien.Font = new Font("OCR A Extended", 8);
			lblOrien.ForeColor = Color.Green;
			this.Controls.Add(lblOrien);
			
			cmbOrien = new ComboBox();
			cmbOrien.Items.Add("Horizontal");
			cmbOrien.Items.Add("Vertical");
			cmbOrien.SelectedIndex = 0;
			cmbOrien.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbOrien.Size = new Size(100, 20);
			cmbOrien.Location = new Point(130, 560);
			this.Controls.Add(cmbOrien);
			
			btnSet = new Button();
			btnSet.Size = new Size(100, 50);
			btnSet.Location = new Point(300, 500);
			btnSet.Text = "Deploy Ship!";
			btnSet.ForeColor = Color.Green;
			btnSet.Font = new Font("OCR A Extended", 10);
			btnSet.FlatStyle = FlatStyle.Flat;
			btnSet.Tag = "btnSet";
			btnSet.Click += new EventHandler(this.btnClicked);
			this.Controls.Add(btnSet);
			
			btnStart = new Button();
			btnStart.Enabled = false;
			btnStart.Size = new Size(100, 40);
			btnStart.Location = new Point(300, 570);
			btnStart.Text = "START!";
			btnStart.Tag = "btnStart";
			btnStart.ForeColor = Color.Green;
			btnStart.Font = new Font("OCR A Extended", 10);
			btnStart.FlatStyle = FlatStyle.Flat;
			btnStart.Click += new EventHandler(this.btnClicked);
			this.Controls.Add(btnStart);
			
			lBox = new ListBox();
			lBox.Size = new Size(60, 250);
			lBox.Location = new Point(900, 120);
			
			lBox2 = new ListBox();
			lBox2.Size = new Size(60, 250);
			lBox2.Location = new Point(900, 380);
			
			pbBack = new PictureBox();
			pbBack.Size = new Size(70, 60);
			pbBack.Image = Image.FromFile("resources/back.png");
			pbBack.Location = new Point(30, 30);
			pbBack.Tag = "back";
			pbBack.MouseEnter += new EventHandler(pbEnter);
			pbBack.MouseLeave += new EventHandler(pbLeave);
			pbBack.Click += new EventHandler(pbClick);
			this.Controls.Add(pbBack);
			
			pbPlayerTurn = new PictureBox();
			pbPlayerTurn.Size = new Size(32, 32);
			pbPlayerTurn.Image = Image.FromFile("resources/turn.gif");
			pbPlayerTurn.Location = new Point(100, 520);
			this.Controls.Add(pbPlayerTurn);
			pbPlayerTurn.Hide();
			
			lblPTurn = new Label();
			lblPTurn.Size = new Size(230, 40);
			lblPTurn.Text = "Your turn.";
			lblPTurn.Location = new Point(140, 520);
			lblPTurn.Font = new Font("OCR A Extended", 23);
			lblPTurn.ForeColor = Color.Green;
			this.Controls.Add(lblPTurn);
			lblPTurn.Hide();
			
			lblPlayer = new Label();
			lblPlayer.Size = new Size(170, 30);
			lblPlayer.Location = new Point(170, 50);
			lblPlayer.Text = "PLAYER BOARD";
			lblPlayer.Font = new Font("OCR A Extended", 15);
			lblPlayer.ForeColor = Color.Blue;
			this.Controls.Add(lblPlayer);
			
			hitstaken1 = new Label();
			hitstaken1.Text = "Lives left: 17";
			hitstaken1.Size = new Size(150, 20);
			hitstaken1.Location = new Point(175, 25);
			hitstaken1.Font = new Font("OCR A Extended", 12);
			hitstaken1.BackColor = Color.White;
			hitstaken1.ForeColor = Color.Blue;
			this.Controls.Add(hitstaken1);
			
			lblAI = new Label();
			lblAI.Size = new Size(170, 30);
			lblAI.Location = new Point(590, 50);
			lblAI.Text = "AI BOARD";
			lblAI.Font = new Font("OCR A Extended", 15);
			lblAI.ForeColor = Color.Red;
			
			hitstaken2 = new Label();
			hitstaken2.Text = "Lives left: 17";
			hitstaken2.Size = new Size(150, 20);
			hitstaken2.Location = new Point(595, 25);
			hitstaken2.Font = new Font("OCR A Extended", 12);
			hitstaken2.BackColor = Color.White;
			hitstaken2.ForeColor = Color.Red;
			
			pbAITurn = new PictureBox();
			pbAITurn.Size = new Size(32, 32);
			pbAITurn.Image = Image.FromFile("resources/turn2.gif");
			pbAITurn.Location = new Point(770, 520);
			pbAITurn.Hide();
			
			lblATurn = new Label();
			lblATurn.Size = new Size(230, 40);
			lblATurn.Text = "AI turn.";
			lblATurn.Location = new Point(580, 520);
			lblATurn.Font = new Font("OCR A Extended", 23);
			lblATurn.ForeColor = Color.Green;
			lblATurn.Hide();
			
			btnHit = new Button();
			btnHit.Location = new Point(870, 30);
			btnHit.Size = new Size(100, 30);
			btnHit.Font = new Font("OCR A Extended", 10);
			btnHit.Text = "Hits: " + intHits.ToString();
			btnHit.FlatStyle = FlatStyle.Flat;
			btnHit.ForeColor = Color.Green;
			this.Controls.Add(btnHit);
			
			pbHit = new PictureBox();
			pbHit.Size = new Size(35, 35);
			pbHit.Image = Image.FromFile("resources/hit.png");
			pbHit.Location = new Point(550, 510);
			
			lblHit = new Label();
			lblHit.Size = new Size(120, 30);
			lblHit.Text = "Shot hit.";
			lblHit.Location = new Point(600, 515);
			lblHit.Font = new Font("OCR A Extended", 13);
			lblHit.ForeColor = Color.Green;	
			
			pbMiss = new PictureBox();
			pbMiss.Size = new Size(35, 35);
			pbMiss.Image = Image.FromFile("resources/miss.png");
			pbMiss.Location = new Point(550, 565);
			
			lblMiss = new Label();
			lblMiss.Size = new Size(140, 30);
			lblMiss.Text = "Shot missed.";
			lblMiss.Location = new Point(600, 570);
			lblMiss.Font = new Font("OCR A Extended", 13);
			lblMiss.ForeColor = Color.Green;
			
			firstHit = new Label();
			firstHit.Text = "The first to hit is YOU. Choose a box on the AI's board to start the war.";
			firstHit.Size = new Size(350, 30);
			firstHit.Location = new Point(70, 515);
			firstHit.Font = new Font("OCR A Extended", 9);
			firstHit.ForeColor = Color.Green;
			
			WINNER = new Label();
			WINNER.Size = new Size(300, 30);
			WINNER.Text = "GAME STAT: On-game";
			WINNER.BackColor = Color.White;
			WINNER.ForeColor = Color.Black;
			WINNER.Font = new Font("OCR A Extended", 16);
			WINNER.Location = new Point(70, 555);	
			
			drawBoard(BOARD1, 70, 120, 80, 40);
			
		} // End of public void initBoardComponents().
		

		public void coorXYlbl(int intX, int intY) {
			
			// DISPLAYS THE COORDINATES ON THE BOARDS.
			char chrYLbl =  'A';
			char chrTemp;
			
			for(int intOLoop = 0; intOLoop < 10; intOLoop++) {
				xCoor[intOLoop] = new Label();
				xCoor[intOLoop].Size = new Size(25, 20);
				xCoor[intOLoop].Location = new Point(intX + (intOLoop * 36), 100);
				xCoor[intOLoop].Text = (intOLoop + 1).ToString();
				xCoor[intOLoop].Font = new Font("OCR A Extended", 10);
				xCoor[intOLoop].ForeColor = Color.Green;
				this.Controls.Add(xCoor[intOLoop]);
				for(int intILoop = 0; intILoop < 10; intILoop++) {
					chrTemp = (char)('A' + intILoop);
					yCoor[intILoop] = new Label();
					yCoor[intILoop].Size = new Size(25, 20);
					yCoor[intILoop].Text = chrYLbl++.ToString();
					yCoor[intILoop].Font = new Font("OCR A Extended", 10);
					yCoor[intILoop].ForeColor = Color.Green;
					yCoor[intILoop].Location = new Point(intY, 133 +  (intILoop * 36));
					this.Controls.Add(yCoor[intILoop]);
				}
			}
			
		} // End of public void coorXYlbl(int intX, int intY).
		
		public void checkCollision(String shipOrien, int shipLength, String ship) {
			
			// PLAYER: CHECKS IF SHIP WILL OVERLAP / OUT OF BOUNDS.
			int intFlag = 0;
			char chrTemp;
			char chrPosX = Convert.ToChar(cmbPosX.SelectedItem.ToString());
			int intPosY = Convert.ToInt32(cmbPosY.SelectedItem.ToString());
			String shipSelect = cmbShips.SelectedItem.ToString();
			
			if(shipOrien == "Horizontal") {
				for(int intLoop = 0; intLoop < shipLength; intLoop++) {
					for(int intLoop2 = 0; intLoop2 < lShipPos1.Count; intLoop2++) {
						String posCheck = chrPosX.ToString() + (intPosY + intLoop);
						if(lShipPos1[intLoop2] == posCheck) {
							intFlag = 1; break;
						} 
					}
				} if(intFlag == 1) {
					ShipOverlap(cmbPosX.SelectedItem.ToString(), cmbPosY.SelectedItem.ToString());
				} if(shipLength == 2 && intPosY == 10) {
						OutOfBounds();
						intFlag = 2;
				} if(shipLength == 3 && intPosY == 10 || shipLength == 3 && intPosY == 9) {
						OutOfBounds();
						intFlag = 2;
				} if(shipLength == 4 && intPosY == 10 || shipLength == 4 && intPosY == 9 || shipLength == 4 && intPosY == 8) {
						OutOfBounds();
						intFlag = 2;
				} if(shipLength == 5 && intPosY == 10 || shipLength == 5 && intPosY == 9 || shipLength == 5 && intPosY == 8 || shipLength == 5 && intPosY == 7) {
						OutOfBounds();
						intFlag = 2;
				} if(intFlag == 0) {
					shipCount++;
			 		if(shipSelect == "Destroyer (2 blocks)") {
						lBox.Items.Add("Destroyer");
						shipName = "Destroyer";
					} else if(shipSelect == "Cruiser (3 blocks)") {
						lBox.Items.Add("Cruiser");
						shipName = "Cruiser";
					} else if(shipSelect == "Submarine (3 blocks)") {
						lBox.Items.Add("Submarine");
						shipName = "Submarine";
					} else if(shipSelect == "Battleship (4 blocks)") {
						lBox.Items.Add("Battleship");
						shipName = "Battleship";
					} else if(shipSelect == "Aircraft Carrier (5 blocks)") {
						lBox.Items.Add("Aircraft");
						shipName = "Aircraft";
					} lShipPos1.Add(shipName);
					for(int intLoop = 0; intLoop < shipLength; intLoop++) {	
						String temp = "resources/ship/" + ship + (intLoop + 1).ToString() + ".png";
						Bitmap btTemp = (Bitmap)Bitmap.FromFile(temp);
						btTemp.RotateFlip(RotateFlipType.Rotate270FlipY);
						BOARD1[Convert.ToInt32(cmbPosY.SelectedItem) - 1 + intLoop, Convert.ToInt32(cmbPosX.SelectedItem) - 65].Image = btTemp;
						lShipPos1.Add(chrPosX.ToString() + (intPosY + intLoop).ToString());
						lBox.Items.Add(chrPosX.ToString() + (intPosY + intLoop).ToString());
						try {
							cmbShips.Items.RemoveAt(cmbShips.SelectedIndex);
						} catch (Exception e) {
							Debug.WriteLine(e.ToString());
						}
					}
				}
			} else if(shipOrien == "Vertical") {
				for(int intLoop = 0; intLoop < shipLength; intLoop++) {
					chrTemp = (char)(chrPosX + intLoop);
					for(int intLoop2 = 0; intLoop2 < lShipPos1.Count; intLoop2++) {
						String posCheck = chrTemp.ToString() + intPosY;
						if(lShipPos1[intLoop2] == posCheck) {
							intFlag = 1; break;
						} 
					}
				} if(intFlag == 1) {
					ShipOverlap(cmbPosX.SelectedItem.ToString(), cmbPosY.SelectedItem.ToString());
				} else if(shipLength == 2 && chrPosX == 'J') {
					OutOfBounds();
					intFlag = 2;
				} else if(shipLength == 3 && chrPosX == 'J' || shipLength == 3 && chrPosX == 'I') {
					OutOfBounds();
					intFlag = 2;
				} else if(shipLength == 4 && chrPosX == 'J' || shipLength == 4 && chrPosX == 'I' || shipLength == 4 && chrPosX == 'H') {
					OutOfBounds();
					intFlag = 2;
				} else if(shipLength == 5 && chrPosX == 'J' || shipLength == 5 && chrPosX == 'I' || shipLength == 5 && chrPosX == 'H' || shipLength == 5 && chrPosX == 'G') {
					OutOfBounds();
					intFlag = 2;
				} if(intFlag == 0) {
					shipCount++;
					if(shipSelect == "Destroyer (2 blocks)") {
						lBox.Items.Add("Destroyer");
						shipName = "Destroyer";
					} else if(shipSelect == "Cruiser (3 blocks)") {
						lBox.Items.Add("Cruiser");
						shipName = "Cruiser";
					} else if(shipSelect == "Submarine (3 blocks)") {
						lBox.Items.Add("Submarine");
						shipName = "Submarine";
					} else if(shipSelect == "Battleship (4 blocks)") {
						lBox.Items.Add("Battleship");
						shipName = "Battleship";
					} else if(shipSelect == "Aircraft Carrier (5 blocks)") {
						lBox.Items.Add("Aircraft");
						shipName = "Aircraft";
					} lShipPos1.Add(shipName);
					for(int intLoop = 0; intLoop < shipLength; intLoop++) {	
						char chrTest = (char)(chrPosX + intLoop);
						String temp = "resources/ship/" + ship + (intLoop + 1).ToString() + ".png";
						BOARD1[Convert.ToInt32(cmbPosY.SelectedItem) - 1, Convert.ToInt32(cmbPosX.SelectedItem) - 65 + intLoop].Image = Image.FromFile(temp);
						lShipPos1.Add(chrTest.ToString() + (intPosY).ToString());
						lBox.Items.Add(chrTest.ToString() + (intPosY).ToString());
						try {
							cmbShips.Items.RemoveAt(cmbShips.SelectedIndex);
						} catch (Exception e) {
							Debug.WriteLine(e.ToString());
						}
					}
				}
			}
			
		} // End of public void checkCollision(String shipOrien, int shipLength, Color clr).
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public void addAiBoardEvents() {
			
			// ADD CLICK EVENT TO AI BOARD.
			for(int intOLoop = 0; intOLoop < 10; intOLoop++) {
				for(int intILoop = 0; intILoop < 10; intILoop++) {
					BOARD2[intOLoop, intILoop].Click += new EventHandler(AIBoardClick);
				}
			}
			
		} // End of public void addAiBoardEvents().
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public void playGame() {
			
			// START THE GAME.
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Tick += new EventHandler(timerTick);
			timer.Interval = 10000;
			
		} // public void playGame().
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public bool checkBoxHit(PictureBox sender) {
			
			// CHECKS IF BOX IS ALREADY HIT (FOR PLAYER).
			int intFlag = 0;
			for(int intLoop = 0; intLoop < boxesHit1.Count; intLoop++) {
				if(sender.Tag.ToString() == boxesHit1[intLoop]) {
					intFlag = 1;
				}
			} if(intFlag == 1) {
				return false;
			} else {
				char[] chrTag = new char[2];
				btnHit.Text = "Hits: " + (intHits++).ToString();
				chrTag = (sender.Tag.ToString()).ToCharArray();
				BOARD2[Convert.ToInt32(chrTag[1]) - 48, Convert.ToInt32(chrTag[0]) - 65].Image = Image.FromFile("resources/miss.png");
				BOARD2[Convert.ToInt32(chrTag[1]) - 48, Convert.ToInt32(chrTag[0]) - 65].Click -= new EventHandler(AIBoardClick);
				boxesHit1.Add(sender.Tag.ToString());
				checkPWin(lShipPos2, chrTag[0].ToString() + (Convert.ToInt32(chrTag[1]) - 47).ToString());
				sinkShip(lShipPos2, shipSunk1, boxesHit1, "AI", 1);
				return true;
			}
			
		} // End of public void checkBoxHit(PictureBox sender).
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public void OutOfBounds() {
			
			// DISPLAY OUT OF BOUNDS MESSAGE.
			frmOut = new Form();
			frmOut.Size = new Size(450, 230);
			frmOut.StartPosition = FormStartPosition.CenterScreen;
			frmOut.FormBorderStyle = FormBorderStyle.FixedSingle;
			frmOut.ControlBox = false;
			frmOut.BackColor = Color.Black;
			
			pbOut = new PictureBox();
			pbOut.Image = Image.FromFile("resources/out.gif");
			pbOut.Size = new Size(100, 200);
			pbOut.Location = new Point(40, 60);
			frmOut.Controls.Add(pbOut);
			
			lblBounds = new Label();
			lblBounds.Text = "The ship will be out of bounds.";
			lblBounds.Size = new Size(200, 50);
			lblBounds.Font = new Font("OCR A Extended", 14);
			lblBounds.Location = new Point(180, 80);
			lblBounds.ForeColor = Color.Green;
			frmOut.Controls.Add(lblBounds);
			
			btnOk = new Button();
			btnOk.FlatStyle = FlatStyle.Flat;
			btnOk.Size = new Size(60, 30);
			btnOk.Text = "OK";
			btnOk.Tag = "btnOk";
			btnOk.Font = new Font("OCR A Extended", 13);
			btnOk.ForeColor = Color.Green;
			btnOk.Location = new Point(370, 170);
			btnOk.Click += new EventHandler(btnClicked);
			frmOut.Controls.Add(btnOk);
			frmOut.Show();
		
		} // End of public void OutOfBounds().
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public void ShipSinking(String shipName, String side) {
			
			if(frmSink.IsDisposed == true)
				frmSink = new Form();
			frmSink.Size = new Size(450, 230);
			frmSink.StartPosition = FormStartPosition.CenterScreen;
			frmSink.FormBorderStyle = FormBorderStyle.FixedSingle;
			frmSink.ControlBox = false;
			frmSink.BackColor = Color.Black;
			
			SinkGIF = new PictureBox();
			SinkGIF.Size = new Size(128, 128);
			SinkGIF.Location = new Point(25, 60);
			SinkGIF.Image = Image.FromFile("resources/sinkship.gif");
			frmSink.Controls.Add(SinkGIF);
			
			btnOk = new Button();
			btnOk.FlatStyle = FlatStyle.Flat;
			btnOk.Size = new Size(60, 30);
			btnOk.Text = "OK";
			btnOk.Tag = "sinkOk";
			btnOk.Font = new Font("OCR A Extended", 13);
			btnOk.ForeColor = Color.Green;
			btnOk.Location = new Point(370, 170);
			btnOk.Click += new EventHandler(btnClicked);
			frmSink.Controls.Add(btnOk);
			
			lblSink = new Label();
			lblSink.Text = shipName + ": " + side + " has been sunk.";
			lblSink.Font = new Font("OCR A Extended", 13);
			lblSink.ForeColor = Color.Green;
			lblSink.BackColor = Color.Black;
			lblSink.Size = new Size(250, 40);
			lblSink.Location = new Point(190, 100);
			frmSink.Controls.Add(lblSink);
			frmSink.Show();
			
		} // End of public void ShipSinking(String shipName, String side).
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public void WinMessage(String message) {
			
			if(frmWin.IsDisposed == true)
				frmWin = new Form();
			frmWin.Size = new Size(450, 230);
			frmWin.StartPosition = FormStartPosition.CenterScreen;
			frmWin.FormBorderStyle = FormBorderStyle.FixedSingle;
			frmWin.ControlBox = false;
			frmWin.BackColor = Color.Black;
			
			saluteGIF = new PictureBox();
			saluteGIF.Size = new Size(150, 128);
			saluteGIF.Location = new Point(25, 60);
			saluteGIF.Image = Image.FromFile("resources/salute.gif");
			frmWin.Controls.Add(saluteGIF);
			
			btnOk = new Button();
			btnOk.FlatStyle = FlatStyle.Flat;
			btnOk.Size = new Size(60, 30);
			btnOk.Text = "OK";
			btnOk.Tag = "winOk";
			btnOk.Font = new Font("OCR A Extended", 13);
			btnOk.ForeColor = Color.Green;
			btnOk.Location = new Point(370, 170);
			btnOk.Click += new EventHandler(btnClicked);
			frmWin.Controls.Add(btnOk);
			
			lblWin = new Label();
			lblWin.Text = message;
			lblWin.Font = new Font("OCR A Extended", 13);
			lblWin.ForeColor = Color.Green;
			lblWin.Size = new Size(250, 60);
			lblWin.Location = new Point(190, 100);
			frmWin.Controls.Add(lblWin);
			frmWin.Show();
			
		} // End of public void ShipSinking(String shipName, String side).
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public void ShipOverlap(String coorX, String coorY) {
			
			if(frmOverlap.IsDisposed == true)
				frmOverlap = new Form();
			frmOverlap.Size = new Size(450, 230);
			frmOverlap.FormBorderStyle = FormBorderStyle.FixedSingle;
			frmOverlap.ControlBox = false;
			frmOverlap.BackColor = Color.Black;
			frmOverlap.StartPosition = FormStartPosition.CenterScreen;
			
			overlapGIF = new PictureBox();
			overlapGIF.Size = new Size(128, 128);
			overlapGIF.Image = Image.FromFile("resources/overlap.gif");
			overlapGIF.Location = new Point(40, 60);
			frmOverlap.Controls.Add(overlapGIF);
			
			lblOverlap = new Label();
			lblOverlap.Text = "Choosing "+ coorX + coorY + " will cause ship overlap.";
			lblOverlap.Font = new Font("OCR A Extended", 13);
			lblOverlap.ForeColor = Color.Green;
			lblOverlap.Size = new Size(250, 40);
			lblOverlap.Location = new Point(190, 80);
			frmOverlap.Controls.Add(lblOverlap);
			
			overOk = new Button();
			overOk.FlatStyle = FlatStyle.Flat;
			overOk.Size = new Size(60, 30);
			overOk.Text = "OK";
			overOk.Tag = "overOk";
			overOk.Font = new Font("OCR A Extended", 13);
			overOk.ForeColor = Color.Green;
			overOk.Location = new Point(370, 170);
			overOk.Click += new EventHandler(btnClicked);
			frmOverlap.Controls.Add(overOk);
			frmOverlap.Show();
		}
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public void btnClicked(object source, EventArgs e) {
			
			// ALL THE BUTTON CLICK EVENTS.
			Button sender = (Button)source;
			
			if(sender.Tag.ToString() == "btnSet") {
				String shipOrien  = cmbOrien.SelectedItem.ToString();
				String shipSelect = "";
				if(cmbShips.SelectedIndex > -1)  {
					shipSelect = cmbShips.SelectedItem.ToString();
					if(shipSelect == "Destroyer (2 blocks)")
						checkCollision(shipOrien, 2, "des");
					else if(shipSelect == "Cruiser (3 blocks)")
						checkCollision(shipOrien, 3, "cru");
					else if(shipSelect == "Submarine (3 blocks)")
						checkCollision(shipOrien, 3, "sub");
					else if(shipSelect == "Battleship (4 blocks)")
						checkCollision(shipOrien, 4, "bat");
					else if(shipSelect == "Aircraft Carrier (5 blocks)")
						checkCollision(shipOrien, 5, "air");
					if(shipCount == 5) {
						btnStart.Enabled = true;
						btnSet.Enabled = false;
						cmbShips.Enabled = false;
						cmbPosX.Enabled = false;
						cmbPosY.Enabled = false;
						cmbOrien.Enabled = false;
					} 
				} else 
					MessageBox.Show("Select a ship.");
				
			} else if(sender.Tag.ToString() == "btnStart") {	
				btnStart.Hide();
				btnSet.Hide();
				lblShip.Hide();
				cmbShips.Hide();
				lblPos.Hide();
				cmbPosX.Hide();
				cmbPosY.Hide();
				lblOrien.Hide();
				cmbOrien.Hide();
				drawBoard(BOARD2, 470, 120, 480, 840);
				this.Controls.Add(lblAI);
				this.Controls.Add(pbMiss);
				this.Controls.Add(lblMiss);
				this.Controls.Add(lblHit);
				this.Controls.Add(pbHit);
				this.Controls.Add(hitstaken2);
				this.Controls.Add(firstHit);
				this.Controls.Add(WINNER);
				randomizeAIBoard(BOARD2, 1);
				playGame();
			} else if(sender.Tag.ToString() == "btnOk") {
				frmOut.Dispose();
			} else if(sender.Tag.ToString() == "sinkOk")
				frmSink.Dispose();
			else if(sender.Tag.ToString() == "overOk")
				frmOverlap.Dispose();
			else if(sender.Tag.ToString() == "winOk")
				frmWin.Dispose();
		} // End of public void btnClicked(object source, EventArgs e).
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public void pbEnter(object source, EventArgs e) {
			
			// IF CURSOR ENTERS TO YES / NO AND BACK.	
			PictureBox sender = (PictureBox)source;
			
			if(sender.Tag.ToString() == "back") { pbBack.Image = Image.FromFile("resources/back2.png"); }
			else if(sender.Tag.ToString() == "yes") { pbYes.Image = Image.FromFile("resources/yes2.png"); }
			else if(sender.Tag.ToString() == "no") { pbNo.Image = Image.FromFile("resources/no2.png"); }
			
		} // End of public void pbEnter(object source, EventArgs e).
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public void pbLeave(object source, EventArgs e) {
			
			// IF CURSOR LEAVES TO YES / NO AND BACK.
			PictureBox sender = (PictureBox)source;
			
			if(sender.Tag.ToString() == "back") { pbBack.Image = Image.FromFile("resources/back.png"); }
			else if(sender.Tag.ToString() == "yes") { pbYes.Image = Image.FromFile("resources/yes.png"); }
			else if(sender.Tag.ToString() == "no") { pbNo.Image = Image.FromFile("resources/no.png"); }
			
		} // End of public void pbLeave(object source, EventArgs e).
		
		/*============================================================================*
	     *  Function   : initBoardComponents
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Handles other components of the GUI.
	     *=============================================================================*/
		public void pbClick(object source, EventArgs e) {
		
			// IF CURSOR CLICKS TO YES / NO AND BACK.
			PictureBox sender = (PictureBox)source;
			StartPage stp = new StartPage();
			Label lblBack = new Label();
			
			if(sender.Tag.ToString() == "back") {
				if(frmBack.IsDisposed == true)
					frmBack = new Form();
				pbBack.Enabled = false;
				lblBack.Size = new Size(400, 70);
				lblBack.Text = "Are you sure?";
				lblBack.Font = new Font("OCR A Extended", 30);
				lblBack.ForeColor = Color.Green;
				lblBack.Location = new Point(90, 40);
				frmBack.Controls.Add(lblBack);
				
				pbYes = new PictureBox();
				pbYes.Size = new Size(170, 100);
				pbYes.Location = new Point(50, 120);
				pbYes.Image = Image.FromFile("resources/yes.png");
				pbYes.Tag = "yes";
				pbYes.MouseEnter += new EventHandler(pbEnter);
				pbYes.MouseLeave += new EventHandler(pbLeave);
				pbYes.Click += new EventHandler(pbClick);
				frmBack.Controls.Add(pbYes);
				
				pbNo = new PictureBox();
				pbNo.Size = new Size(170, 100);
				pbNo.Location = new Point(240, 120);
				pbNo.Image = Image.FromFile("resources/no.png");
				pbNo.Tag = "no";
				pbNo.MouseEnter += new EventHandler(pbEnter);
				pbNo.MouseLeave += new EventHandler(pbLeave);
				pbNo.Click += new EventHandler(pbClick);
				frmBack.Controls.Add(pbNo);
				
				frmBack.Size = new Size(500, 350);
				frmBack.FormBorderStyle = FormBorderStyle.FixedSingle;
				frmBack.ControlBox = false;
				frmBack.BackColor = Color.Black;
				frmBack.StartPosition = FormStartPosition.CenterScreen;
				frmBack.Show();
			
			} else if(sender.Tag.ToString() == "yes") {
				this.Dispose();
				frmBack.Dispose();
				stp.Show();  
			} else if(sender.Tag.ToString() == "no") {
				frmBack.Dispose();
				pbBack.Enabled = true;
			}
		
		} // End of public void pbClick(object source, EventArgs e).
		
		/*============================================================================*
	     *  Function   : AIboardEnter
	     *  Params     : object source - component that triggered the event
	     *				 EventArgs e - event arguments
	     *  Returns    : Void
	     *  Description: If a cursor hovers through a cell, make it red. (UNUSED)
	     *=============================================================================*/
		public void AIboardEnter(object source, EventArgs e) {
			
			// IF CURSOR ENTERS AI BOARD BOX.
			PictureBox sender = (PictureBox)source;
			char[] chrTag = new char[2];
			chrTag = (sender.Tag.ToString()).ToCharArray();
			BOARD2[Convert.ToInt32(chrTag[1]) - 48, Convert.ToInt32(chrTag[0]) - 65].BackColor = Color.Red;
			
		} // End of public void AIboardEnter(object source, EventArgs e).
		
		/*============================================================================*
	     *  Function   : AIboardLeave
	     *  Params     : object source - component that triggered the event
	     *				 EventArgs e - event arguments
	     *  Returns    : Void
	     *  Description: If a cursor leaves a cell, reset the color. (UNUSED)
	     *=============================================================================*/
		public void AIboardLeave(object source, EventArgs e) {
			
			// IF CURSOR LEAVES AI BOARD BOX.
			PictureBox sender = (PictureBox)source;
			char[] chrTag = new char[2];
			chrTag = (sender.Tag.ToString()).ToCharArray();
			BOARD2[Convert.ToInt32(chrTag[1]) - 48, Convert.ToInt32(chrTag[0]) - 65].BackColor = Color.White;
			
		} // End of public void AIboardLeave(object source, EventArgs e).
		
		/*============================================================================*
	     *  Function   : AIBoardClick
	     *  Params     : object source - component that triggered the event
	     *				 EventArgs e - event arguments 
	     *  Returns    : Void
	     *  Description: If player clicks AI board, do this.
	     *=============================================================================*/
		public void AIBoardClick(object source, EventArgs e) {
			
			// IF MOUSE CLICK IS ON AI BOARD BOX.
			PictureBox sender = source as PictureBox;
			bool alreadyHit = checkBoxHit(sender);
			if(alreadyHit == true && winP != 17 || winA != 17) {
				initAiMove();
			}
			
		} // End of public void AIBoardClick(object source, EventArgs e).

		/*============================================================================*
	     *  Function   : DisableBoard
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: Disables the
	     *=============================================================================*/
		public void DisableBoard() {
			
			// DISABLES BOTH BOARDS.
			for(int intOLoop = 0; intOLoop < 10; intOLoop++) {
				for(int intILoop = 0; intILoop < 10; intILoop++) {
					BOARD2[intOLoop, intILoop].Enabled = false;
					BOARD1[intOLoop, intILoop].Enabled = false;
				}
			}
		
		} // End of public void DisableBoard().
		
		/*============================================================================*
	     *  Function   : timerTick
	     *  Params     : object source - component that triggered the event
	     *				 EventArgs e - event arguments  
	     *  Returns    : Void
	     *  Description: Handles turn passing.
	     *=============================================================================*/
		public void timerTick(object source, EventArgs e) {
			 
			 // THE SAVIOR.
		
		} // End of public void timerTick(object source, EventArgs e).
		
		/*============================================================================*
	     *  Function   : checkPWin
	     *  Params     : List<string> shipPos - coordinates of enemy ships 
	     *				 String turnHit - cell to be evaluated
	     *  Returns    : Void
	     *  Description: Checks if player already won.
	     *=============================================================================*/
		public void checkPWin(List<string> shipPos, String turnHit) {
			
			char[] chrTemp = turnHit.ToCharArray();
			
			if(turnHit.Length == 3) {
				strX = ((char)chrTemp[1] + chrTemp[2] - 87).ToString();
			} else {
				strX = chrTemp[1].ToString();
			}
			lBox2.Items.Add(chrTemp[0].ToString() + strX + " PL");
			for(int intLoop = 0; intLoop < shipPos.Count; intLoop++) {
				if(turnHit == shipPos[intLoop]) {
					winP++;
					BOARD2[Convert.ToInt32(strX) - 1, Convert.ToInt32(chrTemp[0]) - 65].Image = Image.FromFile("resources/hit.png");
					break;
				}
			}
			hitstaken2.Text = "Lives left: " + (17 - winP).ToString();
			if(winP == 17) {
				WinMessage("You won. Congratulations!");
				WINNER.Text = "WINNER: PLAYER";
				DisableBoard();
			}
		} // End of public void checkPWin(List<string> shipPos, String turnHit).
		
		/*============================================================================*
	     *  Function   : randomizeAIBoard
	     *  Params     : PictureBox[,] TEMPBOARD - board state
	     *				 int intBoard - board to be processed
	     *  Returns    : Void
	     *  Description: Randomizes ship position on the AI's board.
	     *=============================================================================*/
		public void randomizeAIBoard(PictureBox[,] TEMPBOARD, int intBoard) {
			
			// RANDOMIZE AI BOARD (FUNCTION NAME SPEAKS FOR ITSELF).
			addAiBoardEvents();
			int[] AIshipCount = {0, 0};
			while(AIshipCount[intBoard] < 5) {
				int randY = randomize.Next(1, 10);
				int randX = randomize.Next(1, 10);
				int randOrien = randomize.Next(1, 3);
				int[] shipSize = {2, 3, 3, 4, 5};
				int intFlag = 0;
				char chrTemp = (char) (randY + 'A');
				String posCheck = "";
				
				if(randOrien == 1) {
					for(int intLoop = 0; intLoop < shipSize[AIshipCount[intBoard]]; intLoop++) {
						for(int listLoop = 0; listLoop < lShipPos2.Count; listLoop++) {
							posCheck = chrTemp.ToString() + (randX + intLoop).ToString();
							if(lShipPos2[listLoop] == posCheck) {
								intFlag = 1;
							}
						}
					} if(shipSize[AIshipCount[intBoard]] == 2 &&  randX == 10) {
						intFlag = 2;
					} else if(shipSize[AIshipCount[intBoard]] == 3 &&  randX == 10 || shipSize[AIshipCount[intBoard]] == 3 &&  randX == 9) {
						intFlag = 2;
					} else if(shipSize[AIshipCount[intBoard]] == 4 &&  randX == 10 || shipSize[AIshipCount[intBoard]] == 4 &&  randX == 9 || shipSize[AIshipCount[intBoard]] == 4 &&  randX == 8){
						intFlag = 2;
					} else if(shipSize[AIshipCount[intBoard]] == 5 &&  randX == 10 || shipSize[AIshipCount[intBoard]] == 5 &&  randX == 9 || shipSize[AIshipCount[intBoard]] == 5 &&  randX == 8 || shipSize[AIshipCount[intBoard]] == 5 &&  randX == 7) {
						intFlag = 2;
					} else if(intFlag == 0) {
						if(AIshipCount[intBoard] == 0) {
							lBox.Items.Add("Destroyer");
							lShipPos2.Add("Destroyer");
						} else if(AIshipCount[intBoard] == 1) {
							lBox.Items.Add("Cruiser");
							lShipPos2.Add("Cruiser");
						} else if(AIshipCount[intBoard] == 2) {
							lBox.Items.Add("Submarine");
							lShipPos2.Add("Submarine");
						} else if(AIshipCount[intBoard] == 3) {
							lBox.Items.Add("Battleship");
							lShipPos2.Add("Battleship");
						} else if(AIshipCount[intBoard] == 4) {
							lBox.Items.Add("Aircraft");
							lShipPos2.Add("Aircraft");
						} for(int intLoop = 0; intLoop < shipSize[AIshipCount[intBoard]]; intLoop++) {
							char chrTest = (char)(chrTemp);
							posCheck = chrTest.ToString() + (randX + intLoop).ToString();
							lBox.Items.Add(posCheck);
							lShipPos2.Add(posCheck);
						} AIshipCount[intBoard]++;
					} 
				} else if(randOrien == 2) {
					for(int intLoop = 0; intLoop < shipSize[AIshipCount[intBoard]]; intLoop++) {
						for(int listLoop = 0; listLoop < lShipPos2.Count; listLoop++) {
							char chrTest = (char)(chrTemp + intLoop);
							posCheck = chrTest.ToString() + randX.ToString();
							if(lShipPos2[listLoop] == posCheck) {
								intFlag = 1;
							}
						}
					} if(shipSize[AIshipCount[intBoard]] == 2 &&  chrTemp == 'J') {
						intFlag = 2;
					} else if(shipSize[AIshipCount[intBoard]] == 3 &&  chrTemp == 'J' || shipSize[AIshipCount[intBoard]] == 3 &&  chrTemp == 'I') {
						intFlag = 2;
					} else if(shipSize[AIshipCount[intBoard]] == 4 &&  chrTemp == 'J' || shipSize[AIshipCount[intBoard]] == 4 &&  chrTemp == 'I' || shipSize[AIshipCount[intBoard]] == 4 &&  chrTemp == 'H'){
						intFlag = 2;
					} else if(shipSize[AIshipCount[intBoard]] == 5 &&  chrTemp == 'J' || shipSize[AIshipCount[intBoard]] == 5 &&  chrTemp == 'I' || shipSize[AIshipCount[intBoard]] == 5 &&  chrTemp == 'H' || shipSize[AIshipCount[intBoard]] == 5 &&  chrTemp == 'G') {
						intFlag = 2;
					} else if(intFlag == 0) {
						if(AIshipCount[intBoard] == 0) {
							lBox.Items.Add("Destroyer");
							lShipPos2.Add("Destroyer");
						} else if(AIshipCount[intBoard] == 1) {
							lBox.Items.Add("Cruiser");
							lShipPos2.Add("Cruiser");
						} else if(AIshipCount[intBoard] == 2) {
							lBox.Items.Add("Submarine");
							lShipPos2.Add("Submarine");
						} else if(AIshipCount[intBoard] == 3) {
							lBox.Items.Add("Battleship");
							lShipPos2.Add("Battleship");
						} else if(AIshipCount[intBoard] == 4) {
							lBox.Items.Add("Aircraft");
							lShipPos2.Add("Aircraft");
						} for(int intLoop = 0; intLoop < shipSize[AIshipCount[intBoard]]; intLoop++) {
							char chrTest = (char)(chrTemp + intLoop);
							posCheck = chrTest.ToString() + randX.ToString();
							lBox.Items.Add(posCheck);
							lShipPos2.Add(posCheck);
						} AIshipCount[intBoard]++;
					}
				}
			} 
			
		} // End of public void randomizeAIBoard().
		
		/*============================================================================*
	     *  Function   : initAiMove
	     *  Params     : None 
	     *  Returns    : Void
	     *  Description: This is the brain of the AI.
	     *=============================================================================*/
		public void initAiMove() {
			
			// START AI MOVE (STARTS W/ RANDOM SHOTS).
			String hold = "";
			String POPSTACK = "";
			int FLAG = 0, FIREANOTHER = 0;
			
			if(EXPLORE_STACK.Count > 0) {
				int HITCHECK = 0;
				FLAG = 0;
				
				do {
					FLAG = 0;
					POPSTACK = EXPLORE_STACK.Pop().ToString();
					char[] chrTemp = POPSTACK.ToCharArray();
					for(int intLoop = 0; intLoop < boxesHit2.Count; intLoop++) {
						if(POPSTACK == boxesHit2[intLoop]) {
							FLAG = 1;
							break;
						}
					} if(FLAG == 0) {
						for(int intLoop = 0; intLoop < lShipPos1.Count; intLoop++) {
							if(chrTemp[0].ToString() + Convert.ToInt32(chrTemp[1] - 47).ToString() == lShipPos1[intLoop]) {
								HITCHECK = 1;
							}
						} if(HITCHECK == 1) {
							BOARD1[Convert.ToInt32(chrTemp[1] - 48), Convert.ToInt32(chrTemp[0]) - 65].Image = Image.FromFile("resources/hit.png");
							HUNTSTRAIGHT(chrTemp[0].ToString() + Convert.ToInt32(chrTemp[1] - 48).ToString());
						} else if(HITCHECK == 0)
							BOARD1[Convert.ToInt32(chrTemp[1] - 48), Convert.ToInt32(chrTemp[0]) - 65].Image = Image.FromFile("resources/miss.png");
						btnHit.Text = "Hits: " + (++intHits).ToString();
						boxesHit2.Add(POPSTACK);
						sinkShip(lShipPos1, shipSunk2, boxesHit2, "Player", 0);
						checkAWin(lShipPos1);
						FIREANOTHER = 1;
					}
				} while(FLAG == 1 && EXPLORE_STACK.Count > 0);
			}
			
			if(HUNT_STACK.Count > 0 && EXPLORE_STACK.Count == 0 && FIREANOTHER == 0) {
				int HITCHECK = 0;
				FLAG = 0;
				do {
					FLAG = 0;
					POPSTACK = HUNT_STACK.Pop().ToString();
					char[] chrTemp = POPSTACK.ToCharArray();
					if(POPSTACK.Length <= 3) {
						if(POPSTACK.Length == 3) {
							POPSTACK = chrTemp[0].ToString() + "9";
						} else {
							POPSTACK = chrTemp[0].ToString() + (Convert.ToInt32(chrTemp[1].ToString()) - 1).ToString();
						}
						chrTemp = POPSTACK.ToCharArray();
						for(int intLoop = 0; intLoop < boxesHit2.Count; intLoop++) {
							if(POPSTACK == boxesHit2[intLoop]) {
								FLAG = 1;
								break;
							}
						} if(FLAG == 0) {
							for(int intLoop = 0; intLoop < lShipPos1.Count; intLoop++) {
								if(chrTemp[0].ToString() + (Convert.ToInt32(chrTemp[1].ToString()) + 1).ToString() == lShipPos1[intLoop]) {
									HITCHECK = 1;
								}
							} 
							if(HITCHECK == 1) {
								BOARD1[Convert.ToInt32(chrTemp[1] - 48), Convert.ToInt32(chrTemp[0]) - 65].Image = Image.FromFile("resources/hit.png");
							} else if(HITCHECK == 0)
								BOARD1[Convert.ToInt32(chrTemp[1] - 48), Convert.ToInt32(chrTemp[0]) - 65].Image = Image.FromFile("resources/miss.png");
							btnHit.Text = "Hits: " + (++intHits).ToString();
							boxesHit2.Add(POPSTACK);
							sinkShip(lShipPos1, shipSunk2, boxesHit2, "Player", 0);
							checkAWin(lShipPos1);
							FIREANOTHER = 1;
						}
					}
				} while(FLAG == 1 && HUNT_STACK.Count > 0);
			}
			

			if(EXPLORE_STACK.Count == 0 && HUNT_STACK.Count == 0 || EXPLORE_STACK.Count == 0 && FIREANOTHER == 0) {
				while(notOcc == false) {
					int intFlag = 0, HITCHECK = 0;
					int yRand = randomize.Next(0, 10);
					char randY = (char)('A' + yRand);
					hold = randY.ToString();
					randX = randomize.Next(0, 10);
					String strTemp = randY.ToString() + randX.ToString();
					pbPlayerTurn.Hide();
					lblPTurn.Hide();
					
					for(int intLoop = 0; intLoop < boxesHit2.Count; intLoop++) {
						if(boxesHit2[intLoop] == strTemp) {
							intFlag = 1;
							break;
						}
					} if(intFlag == 0) { 
						int intLoop;
						char[] chrTemp;
						
						if(strTemp.Length == 3) {
							chrTemp = strTemp.ToCharArray();
							strTemp = chrTemp[0].ToString() + Convert.ToString(9);
							chrTemp = strTemp.ToCharArray();
						} else {
							chrTemp = strTemp.ToCharArray();
						}
						
						for(intLoop = 0; intLoop < lShipPos1.Count; intLoop++) {
							if(chrTemp[0].ToString() + Convert.ToInt32(chrTemp[1] - 47).ToString() == lShipPos1[intLoop]) {
								HITCHECK = 1;
								break;
							}
						} if(HITCHECK == 1) {
							BOARD1[randX, Convert.ToInt32(randY) - 65].Image = Image.FromFile("resources/hit.png");
							HUNTPOINTS(chrTemp);
							HUNTSTRAIGHT(strTemp);
							
						} else if(HITCHECK == 0)
							BOARD1[randX, Convert.ToInt32(randY) - 65].Image = Image.FromFile("resources/miss.png");
						btnHit.Text = "Hits: " + (++intHits).ToString();
						boxesHit2.Add(strTemp);
						sinkShip(lShipPos1, shipSunk2, boxesHit2, "Player", 0);
						notOcc = true;
					}
				}
				notOcc = false;
				checkAWin(lShipPos1);
			}
			
			playGame();
			
		} // End of public void initAiMove().
		
		/*============================================================================*
	     *  Function   : HUNTPOINTS
	     *  Params     : char[] ROOT - root of the tree 
	     *  Returns    : Void
	     *  Description: If a cell is HIT, puts the cells around it on a stack to be
	     *               explored by the AI.
	     *=============================================================================*/
		public void HUNTPOINTS(char[] ROOT) { // Root is (Limit 0).
			
			// Search (Limit 1).
			if(ROOT[0] == 'A' && ROOT[1] == '0') {
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 47).ToString());
				EXPLORE_STACK.Push(((char)(ROOT[0] + 1)).ToString() + ROOT[1].ToString());
			} else if(ROOT[0] == 'A' && ROOT[1] == '9') {
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 49).ToString());
				EXPLORE_STACK.Push(((char)(ROOT[0] + 1)).ToString() + ROOT[1].ToString());
			} else if(ROOT[0] == 'J' && ROOT[1] == '0') {
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 47).ToString());
				EXPLORE_STACK.Push(((char)(ROOT[0] - 1)).ToString() + ROOT[1].ToString());
			} else if(ROOT[0] == 'J' && ROOT[1] == '9') {
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 49).ToString());
				EXPLORE_STACK.Push(((char)(ROOT[0] - 1)).ToString() + ROOT[1].ToString());
			} else if(ROOT[0] == 'A') {
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 47).ToString());
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 49).ToString());
				EXPLORE_STACK.Push(((char)(ROOT[0] + 1)).ToString() + ROOT[1].ToString());
			} else if(ROOT[1] == '9') {
				EXPLORE_STACK.Push(((char)(ROOT[0] - 1)).ToString() + ROOT[1].ToString());
				EXPLORE_STACK.Push(((char)(ROOT[0] + 1)).ToString() + ROOT[1].ToString());
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 49).ToString());
			} else if(ROOT[0] == 'J') {
				EXPLORE_STACK.Push(((char)(ROOT[0] - 1)).ToString() + ROOT[1].ToString());
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 47).ToString());
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 49).ToString());
			} else if(ROOT[1] == '0') {
				EXPLORE_STACK.Push(((char)(ROOT[0] - 1)).ToString() + ROOT[1].ToString());
				EXPLORE_STACK.Push(((char)(ROOT[0] + 1)).ToString() + ROOT[1].ToString());
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 47).ToString());
			} else {
				EXPLORE_STACK.Push(((char)(ROOT[0] - 1)).ToString() + ROOT[1].ToString());
				EXPLORE_STACK.Push(((char)(ROOT[0] + 1)).ToString() + ROOT[1].ToString());
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 49).ToString());
				EXPLORE_STACK.Push(ROOT[0] + (Convert.ToInt32(ROOT[1]) - 47).ToString());
			}
	
		} // End of public void stackHuntMode().
		
		/*============================================================================*
	     *  Function   : HUNTSTRAIGHT
	     *  Params     : String ROOT 
	     *  Returns    : Void
	     *  Description: Puts possible coordinate of a ship to be sinked.
	     *=============================================================================*/
		public void HUNTSTRAIGHT(String ROOT) { // Search (Limit 2) 
		
			String check = "";
			char[] chrTemp = ROOT.ToCharArray();
			ROOT = chrTemp[0].ToString() + (Convert.ToInt32(chrTemp[1].ToString()) + 1);
			int intFlag = 0;
			for(int intLoop = 0; intLoop < lShipPos1.Count; intLoop++) {
				if(lShipPos1[intLoop].Length > 3 && intFlag == 0) {
					check = lShipPos1[intLoop];
				} if(ROOT == lShipPos1[intLoop]) {
					intFlag = 1;
					break;
				}
			} if(check == "Destroyer") {
				int indexDes = lShipPos1.IndexOf("Destroyer") + 2;
				for(int intLoop = indexDes; intLoop > 0; intLoop--) {
					HUNT_STACK.Push(lShipPos1[intLoop]);
				}
			} else if(check == "Cruiser") {
				int indexDes = lShipPos1.IndexOf("Cruiser") + 3;
				for(int intLoop = indexDes; intLoop > 3; intLoop--) {
					HUNT_STACK.Push(lShipPos1[intLoop]);
				}
			} else if(check == "Submarine") {
				int indexDes = lShipPos1.IndexOf("Submarine") + 3;
				for(int intLoop = indexDes; intLoop > 7; intLoop--) {
					HUNT_STACK.Push(lShipPos1[intLoop]);
				}
			} else if(check == "Battleship") {
				int indexDes = lShipPos1.IndexOf("Battleship") + 1;
				for(int intLoop = 0; intLoop < 4; intLoop++) {
					HUNT_STACK.Push(lShipPos1[intLoop + indexDes]);
				}
			} else if(check == "Aircraft") {
				int indexDes = lShipPos1.IndexOf("Aircraft") + 1;
				for(int intLoop = 0 ; intLoop < 5; intLoop++) {
					HUNT_STACK.Push(lShipPos1[indexDes + intLoop]);
				}
			}	
			
		}
		
		/*============================================================================*
	     *  Function   : checkAWin
	     *  Params     : List<string> shipPos 
	     *  Returns    : Void
	     *  Description: Checks if AI won the game (updates Lives Left display).
	     *=============================================================================*/
		public void checkAWin(List<string> shipPos) {
			
			// CHECKS IF AI ALREADY WON.
			int check = 0;
			for(int intLoop = 0; intLoop < shipPos.Count; intLoop++) {
				for(int intILoop = 0; intILoop < boxesHit2.Count; intILoop++) {
					char[] chrTemp = boxesHit2[intILoop].ToCharArray();
					String strTemp = chrTemp[0].ToString() + (Convert.ToInt32(chrTemp[1].ToString()) + 1).ToString();
					if(shipPos[intLoop] == strTemp)
					check++;
				}
			} 
			hitstaken1.Text = "Lives left: " + (17 - check).ToString();
			if(check == 17) {
				WinMessage("AI won. Nice match.");
				DisableBoard();
				WINNER.Text = "WINNER: AI";
			}
			winA = check;
		
		} // End of public void checkAWin(List<string> shipPos).
		
		/*============================================================================*
	     *  Function   : sinkShip
	     *  Params     : List<string> shipPos - coordinates of the ship
	     *               List<String> shipSunk - coordinates of sunk ship
	     *				 List<String> boxesHit - coordinates of cells that is 
	     *                                       already hit
	     *               String side - edges of the board
	     *               int arrayIndex - (?)
	     *  Returns    : Void
	     *  Description: Checks if AI won the game (updates Lives Left display).
	     *=============================================================================*/
		public void sinkShip(List<String> shipPos, List<String> shipSunk, List<String> boxesHit, String side, int arrayIndex) {
			
			// DISPLAY SHIP SINK MESSAGE & UPDATES SHIP STATUS.
			char[] chrTemp;
			String strTemp = "";
			int indexDes = shipPos.IndexOf("Destroyer");
			int indexCru = shipPos.IndexOf("Cruiser");
			int indexSub = shipPos.IndexOf("Submarine");
			int indexBat = shipPos.IndexOf("Battleship");
			int indexAir = shipPos.IndexOf("Aircraft");
			
			if(checkShipSunk("Destroyer", shipSunk) == false && des[arrayIndex] != 2) {
				des[arrayIndex] = 0;
				for(int intLoop = 1; intLoop <= 2; intLoop++) {
					for(int intHit = 0; intHit < boxesHit.Count; intHit++) {
						chrTemp = boxesHit[intHit].ToCharArray();
						strTemp = chrTemp[0].ToString() + (Convert.ToInt32(chrTemp[1]) - 47).ToString();
						if(shipPos[indexDes + intLoop] == strTemp) {
							des[arrayIndex]++;
						} 
					}
				} if(des[arrayIndex] == 2) {
					shipSunk.Add("Destroyer");
					ShipSinking(side, "Destroyer");
				}
			} if(checkShipSunk("Cruiser", shipSunk) == false && cru[arrayIndex] != 3) {
				cru[arrayIndex] = 0;
				for(int intLoop = 1; intLoop <= 3; intLoop++) {
					for(int intHit = 0; intHit < boxesHit.Count; intHit++) {
						chrTemp = boxesHit[intHit].ToCharArray();
						strTemp = chrTemp[0].ToString() + (Convert.ToInt32(chrTemp[1]) - 47).ToString();
						if(shipPos[indexCru + intLoop] == strTemp) {
							cru[arrayIndex]++;
						} 
					}
				} if(cru[arrayIndex] == 3) {
					shipSunk.Add("Cruiser");
					ShipSinking(side, "Cruiser");
				}
			} if(checkShipSunk(side + ": Submarine", shipSunk) == false && sub[arrayIndex] != 3) {
				sub[arrayIndex] = 0;
				for(int intLoop = 1; intLoop <= 3; intLoop++) {
					for(int intHit = 0; intHit < boxesHit.Count; intHit++) {
						chrTemp = boxesHit[intHit].ToCharArray();
						strTemp = chrTemp[0].ToString() + (Convert.ToInt32(chrTemp[1]) - 47).ToString();
						if(shipPos[indexSub + intLoop] == strTemp) {
							sub[arrayIndex]++;
						} 
					}
				} if(sub[arrayIndex] == 3) {
					shipSunk.Add("Submarine");
					ShipSinking(side, "Submarine");
				}
			} if(checkShipSunk("Battleship", shipSunk) == false && bat[arrayIndex] != 4) {
				bat[arrayIndex] = 0;
				for(int intLoop = 1; intLoop <= 4; intLoop++) {
					for(int intHit = 0; intHit < boxesHit.Count; intHit++) {
						chrTemp = boxesHit[intHit].ToCharArray();
						strTemp = chrTemp[0].ToString() + (Convert.ToInt32(chrTemp[1]) - 47).ToString();
						if(shipPos[indexBat + intLoop] == strTemp) {
							bat[arrayIndex]++;
						} 
					} 
				} if(bat[arrayIndex] == 4) {
					shipSunk.Add("Battleship");
					ShipSinking(side, "Battleship");
				}
			} if(checkShipSunk("Aircraft", shipSunk) == false && air[arrayIndex] != 5) {
				air[arrayIndex] = 0;
				for(int intLoop = 1; intLoop <= 5; intLoop++) {
					for(int intHit = 0; intHit < boxesHit.Count; intHit++) {
						chrTemp = boxesHit[intHit].ToCharArray();
						strTemp = chrTemp[0].ToString() + (Convert.ToInt32(chrTemp[1]) - 47).ToString();
						if(shipPos[indexAir + intLoop] == strTemp) {
							air[arrayIndex]++;
						} 
					} 
				} if(air[arrayIndex] == 5) {
					shipSunk.Add("Aircraft");
					ShipSinking(side, "Aircraft");
				}
			}

		} // End of public void sinkShip(List<String> shipPos, List<String> shipSunk, List<String> boxesHit, String side, int arrayIndex).
		
		/*============================================================================*
	     *  Function   : checkShipSunk
	     *  Params     : String shipName - name of the ship
	     *               List<STring> sunkList - list of sunk ships 
	     *  Returns    : Bolean
	     *  Description: Checks if a ship is already sunk.
	     *=============================================================================*/
		public bool checkShipSunk(String shipName, List<String> sunkList) {
			
			// RETURNS IF SHIP IS ALREADY IN THE SINK LIST.
			bool check = false;
			int intFlag = 0;
			for(int intLoop = 0; intLoop < sunkList.Count; intLoop++) {
				if(shipName == sunkList[intLoop]) {
					intFlag = 1;
				} else {
					intFlag = 0;
				}
			} if(intFlag == 0)
				check = false;
			else if(intFlag == 1)
				check = true;
			return check;
		
		} // End of public bool checkShipSunk(String shipName, List<String> sunkList).

		/*==============================INITIALIZATION==============================*/
		private PictureBox[,] BOARD1 = new PictureBox[10, 10];		
		private PictureBox[,] BOARD2 = new PictureBox[10, 10];
		private PictureBox pbBack, pbYes, pbNo, pbPlayerTurn, pbAITurn, pbOut, SinkGIF, overlapGIF, saluteGIF, pbMiss, pbHit;
		private ComboBox cmbShips, cmbPosX, cmbPosY, cmbOrien;
		private Label lblShip, lblPos, lblOrien, lblPTurn, lblATurn, lblBounds, lblSink, lblOverlap, lblAI, lblPlayer, lblMiss, lblHit, hitstaken1, hitstaken2, firstHit, WINNER,  lblWin;
		private Label[] xCoor = new Label[10];
		private Label[] yCoor = new Label[10]; 
		private Button btnSet, btnStart, btnHit, btnOk, overOk;
		private List<String> lShipPos1 = new List<String>();
		private List<String> lShipPos2 = new List<String>();
		private List<String> boxesHit1 = new List<String>();
		private List<String> boxesHit2 = new List<String>();
		private List<String> shipSunk1 = new List<String>();
		private List<String> shipSunk2 = new List<String>();
		private Stack EXPLORE_STACK = new Stack();
		private Stack HUNT_STACK    = new Stack();
		private static Random randomize = new Random();
		private int shipCount = 0, intHits = 0, winA = 0, winP = 0, randX;
		private int[] des = new int[2];
		private int[] cru = new int[2];
		private int[] sub = new int[2];
		private int[] bat = new int[2];
		private int[] air = new int[2];
		private ListBox lBox, lBox2;
		private bool notOcc = false;
		private Form frmBack = new Form();
		private Form frmOut = new Form();
		private Form frmSink = new Form();
		private Form frmOverlap = new Form();
		private Form frmWin = new Form();
		private String strX = "", shipName = "";

	
	} // End of public class mainGame : Form.
	
} // End of namespace battleShipV3.